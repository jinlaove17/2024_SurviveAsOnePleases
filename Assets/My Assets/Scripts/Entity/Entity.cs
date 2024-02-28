using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float hp { get; protected set; }
    public bool isDamaged { get; protected set; }
    private List<Material> materials { get; set; }
    public AudioSource audioSource { get; private set; }

    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

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
        audioSource = GetComponent<AudioSource>();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] mats = renderer.materials;

            foreach (Material material in mats)
            {
                materials.Add(material);
            }
        }
    }

    protected virtual void OnEnable()
    {
        isDamaged = false;
    }

    public virtual void OnDamage(Entity from, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if ((isDamaged) || (isDead))
        {
            return;
        }

        hp = Mathf.Max(hp - damage, 0.0f);

        if (isDead)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageRoutine(0.08f));
        }
    }

    private IEnumerator DamageRoutine(float duration)
    {
        isDamaged = true;

        foreach (Material material in materials)
        {
            material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(duration);

        foreach (Material material in materials)
        {
            material.color = Color.white;
        }

        isDamaged = false;
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
