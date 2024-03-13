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
        Use(from);
    }

    protected abstract void Use(Entity entity);
}
