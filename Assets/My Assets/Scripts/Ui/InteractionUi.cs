using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUi : MonoBehaviour
{
    [field: SerializeField] private Text desc { get; set; }

    public void SetText(StringBuilder sb)
    {
        desc.text = sb.ToString();
    }
}
