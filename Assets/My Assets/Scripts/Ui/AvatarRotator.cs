using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AvatarRotator : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [field: SerializeField] private PlayerInput playerInput { get; set; }
    [field: SerializeField] private Transform avatarCamTransform { get; set; }
    [field: SerializeField] private Transform playerTransform { get; set; }
    [field: SerializeField] private float smoothness { get; set; }
    private bool isCaptured { get; set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        isCaptured = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCaptured)
        {
            float angleY = smoothness * playerInput.rotate.x * Time.deltaTime;

            avatarCamTransform.RotateAround(playerTransform.position, Vector3.up, angleY);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCaptured = false;
    }
}
