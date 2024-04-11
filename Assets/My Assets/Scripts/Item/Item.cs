using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class Item : MonoBehaviour, IInteractable
{
    [field: SerializeField] public ItemData itemData { get; private set; }

    private void OnTriggerExit(Collider other)
    {
        UiManager.instance.inventoryUi.DeleteAroundItem(this);
    }

    public void Interact(Entity from)
    {
        PlayerHealth player = (PlayerHealth)from;

        player.PickUpItem();
        UiManager.instance.inventoryUi.DeleteAroundItem(this);
        UiManager.instance.inventoryUi.AcquireItem(this);
        gameObject.SetActive(false);
    }

    protected abstract void Use(Entity entity);
}
