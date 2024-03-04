using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUi : StatusUi
{
    [field: SerializeField] private Text ammoText { get; set; }

    public void SetAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo.ToString() + " / " + remainAmmo.ToString();
    }
}
