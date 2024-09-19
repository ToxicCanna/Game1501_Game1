using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackingEggUI : GameplayUI
{
    [SerializeField] private Slider slider;
    [SerializeField] private EggCrackingScriptableObject eggCrackingData;
    //private float sliderValue;

    public override void RecievePeek(int percent, int target, int leeway)
    {
        //slider moves here
        float sliderValue = Mathf.Clamp01(percent / 100f);
        slider.value = sliderValue;
    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        //this is on spacebar release. change the hand sprite here
        if (percent < (target - leeway))
        {
            child_SpriteRenderer.sprite = eggCrackingData.eggCrackingType[0];
        }
        else if (percent >= (target - leeway) && percent <= (target + leeway))
        {
            child_SpriteRenderer.sprite = eggCrackingData.eggCrackingType[1];
        }
        else
        {
            child_SpriteRenderer.sprite = eggCrackingData.eggCrackingType[2];
        }
    }

}
