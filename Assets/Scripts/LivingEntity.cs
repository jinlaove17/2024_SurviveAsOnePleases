using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float maxHp { get; protected set; }
    public float hp { get; protected set; }
    public bool isHit { get; protected set; }

    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

    private List<Material> materials;

    public bool isDead
    {
        get
        {
            return hp <= 0.01f;
        }
    }

    protected virtual void Awake()
    {
        materials = new List<Material>();

        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            Material[] mats = meshRenderer.materials;

            foreach (Material material in mats)
            {
                materials.Add(material);
            }
        }

        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            Material[] mats = skinnedMeshRenderer.materials;

            foreach (Material material in mats)
            {
                materials.Add(material);
            }
        }
    }

    protected virtual void OnEnable()
    {
        hp = maxHp;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if ((isHit) || (isDead))
        {
            return;
        }

        //hp -= damage;

        if (isDead)
        {
            Die();
        }
        else
        {
            StartCoroutine(HitRoutine(0.08f));
        }
    }

    private IEnumerator HitRoutine(float duration)
    {
        isHit = true;

        foreach (Material material in materials)
        {
            material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(duration);

        foreach (Material material in materials)
        {
            material.color = Color.white;
        }

        isHit = false;
    }

    public virtual void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ��� ����
        if (onDeath != null)
        {
            onDeath();
        }
    }
}
