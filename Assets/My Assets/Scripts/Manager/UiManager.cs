using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    [field: SerializeField] private Text ammoText { get; set; }
    [field: SerializeField] private Slider playerHpBar { get; set; }
    private float recentHp { get; set; }
    private Coroutine hpAniRoutine { get; set; }

    public void SetPlayerHpBar(float maxHp, float hp)
    {
        if (hpAniRoutine != null)
        {
            StopCoroutine(hpAniRoutine);
            hpAniRoutine = null;
        }

        hpAniRoutine = StartCoroutine(HpAniRoutine(maxHp, hp));
    }

    private IEnumerator HpAniRoutine(float maxHp, float targetHp)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float smoothness = 3.0f * Time.deltaTime;

        while ((int)targetHp != (int)recentHp)
        {
            recentHp = Mathf.Lerp(recentHp, targetHp, smoothness);
            playerHpBar.value = recentHp / maxHp;
            yield return waitForEndOfFrame;
        }

        recentHp = targetHp;
        playerHpBar.value = recentHp / maxHp;
    }

    public void SetAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo.ToString() + " / " + remainAmmo.ToString();
    }
}
