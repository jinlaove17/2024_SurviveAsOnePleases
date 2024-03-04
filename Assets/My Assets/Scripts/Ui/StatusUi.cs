using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUi : MonoBehaviour
{
    [field: SerializeField] public string entityName { get; set; }
    [field: SerializeField] public HpBarUi hpBar { get; private set; }
}
