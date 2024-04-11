using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerHealth : Entity
{
    [field: SerializeField] private float maxHp { get; set; }

    private Animator playerAnimator { get; set; }
    private PlayerInput playerInput { get; set; }
    private PlayerAttack playerAttack { get; set; }
    private bool isWeaponEquipped { get; set; }
    public bool isInteract { get; private set; }
    private IInteractable interactableObj { get; set; }

    protected override void Awake()
    {
        base.Awake();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hp = maxHp;
        UiManager.instance.playerStatusUi.hpBar.UpdateHpBar(maxHp, hp);
        interactableObj = null;
    }

    private void Update()
    {
        if (isInteract)
        {
            return;
        }

        if (((playerInput.input & ACTION.INTERACT) > 0) && (interactableObj != null))
        {
            isInteract = true;
            interactableObj.Interact(this);
            interactableObj = null;
            UiManager.instance.interactionUi.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }

        if (other.tag == "Item")
        {
            if (interactableObj == null)
            {
                // �浹�� �������κ��� Item ������Ʈ �������� �õ�
                Item item = other.GetComponent<Item>();

                if (item != null)
                {
                    interactableObj = item;

                    StringBuilder sb = new StringBuilder();

                    sb.AppendFormat($"{item.itemData.name} ȹ���ϱ�");
                    UiManager.instance.interactionUi.SetText(sb);
                    UiManager.instance.interactionUi.gameObject.SetActive(true);
                    UiManager.instance.inventoryUi.InsertAroundItem(item);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactableObj = null;
        UiManager.instance.interactionUi.gameObject.SetActive(false);
    }

    public override void OnDamage(Entity from, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if ((isDamaged) || (isDead))
        {
            return;
        }

        base.OnDamage(from, damage, hitPoint, hitNormal);
        UiManager.instance.playerStatusUi.hpBar.UpdateHpBar(maxHp, hp);
    }

    public void PickUpItem()
    {
        isWeaponEquipped = playerAttack.weapon.gameObject.activeSelf;

        if (isWeaponEquipped)
        {
            playerAttack.weapon.gameObject.SetActive(false);
        }

        playerAnimator.SetTrigger("Pick Up");
    }

    public void OnPickUpExit()
    {
        isInteract = false;

        if (isWeaponEquipped)
        {
            playerAttack.weapon.gameObject.SetActive(true);
        }
    }

    public override void Recovery(float healPer)
    {
        // �̹� ����� ��� ü���� ȸ���� �� ����
        if (isDead)
        {
            return;
        }

        base.Recovery(healPer);

        // ȸ������ hp�� �ִ� hp�� �ʰ����� �ʵ��� ����
        hp = Mathf.Min(hp, maxHp);
    }
}
