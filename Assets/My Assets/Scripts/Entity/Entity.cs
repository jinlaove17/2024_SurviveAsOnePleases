using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public float hp { get; protected set; }
    public bool isDamaged { get; protected set; }
    private List<Material> materials { get; set; }
    public Collider collider { get; protected set; }
    public AudioSource audioSource { get; private set; }

    public event Action onDeath; // 사망시 발동할 이벤트

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
        collider = GetComponent<Collider>();
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
        collider.enabled = true;
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
        // onDeath 이벤트에 등록된 메서드가 있다면 모두 실행
        if (onDeath != null)
        {
            onDeath();
        }
    }

    protected void Inactive(float duration)
    {
        StartCoroutine(InactiveRoutine(duration));
    }

    private IEnumerator InactiveRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        gameObject.SetActive(false);
    }
}
