using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUi : MonoBehaviour
{
    [field: SerializeField] private Slider delayedSlider { get; set; }
    [field: SerializeField] private Slider slider { get; set; }
    [field: SerializeField] private Coroutine aniRoutine { get; set; }

    public void UpdateHpBar(float maxHp, float hp)
    {
        if (aniRoutine != null)
        {
            StopCoroutine(aniRoutine);
            aniRoutine = null;
        }

        float hpPer = hp / maxHp;

        if (hpPer >= slider.value)
        {
            aniRoutine = StartCoroutine(HpIncAniRoutine(hpPer));
        }
        else
        {
            aniRoutine = StartCoroutine(HpDecAniRoutine(hpPer));
        }
    }

    public void UpdateHpBar(float initValue, float maxHp, float hp)
    {
        slider.value = delayedSlider.value = initValue;
        UpdateHpBar(maxHp, hp);
    }

    private IEnumerator HpIncAniRoutine(float hpPer)
    {
        yield return null;
    }

    private IEnumerator HpDecAniRoutine(float hpPer)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float smoothness = 3.0f * Time.deltaTime;

        while (slider.value - hpPer > 0.01f)
        {
            slider.value = Mathf.Lerp(slider.value, hpPer, 2.0f * smoothness);
            yield return waitForEndOfFrame;
            delayedSlider.value = Mathf.Lerp(delayedSlider.value, slider.value, smoothness);
        }

        slider.value = hpPer;

        while (delayedSlider.value - slider.value > 0.01f)
        {
            delayedSlider.value = Mathf.Lerp(delayedSlider.value, slider.value, smoothness);
            yield return waitForEndOfFrame;
        }

        delayedSlider.value = slider.value;
    }
}
