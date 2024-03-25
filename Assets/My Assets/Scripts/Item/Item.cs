using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    [field: SerializeField] public ItemData itemData { get; private set; }

    public void Interact(Entity from)
    {
        Animator playerAnimator = from.GetComponent<Animator>();

        playerAnimator.SetTrigger("Pick Up");
        UiManager.instance.inventory.AcquireItem(this);
        gameObject.SetActive(false);
    }

    protected abstract void Use(Entity entity);
}
