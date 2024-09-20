using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkUI : GameplayUI
{
    private int milkSection = 0;
    private float milkHeight;
    private float containerHeight = 4f;
    public RectTransform[] milkRenderers;
    public RectTransform[] progressRenderers;

    void Start()
    {
        milkHeight = 0.01f;
        milkSection = 0;
        
    }

    public override void RecievePeek(int percent, int target, int leeway)
    {
        milkHeight = percent * 0.01f * containerHeight;
        milkRenderers[milkSection].transform.localScale = new Vector3(1, milkHeight, 1);
        progressRenderers[milkSection].transform.localScale = new Vector3(1, milkHeight/4, 1);

    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        if (milkSection < 2)
        {
            milkSection++;
        }

    }
}