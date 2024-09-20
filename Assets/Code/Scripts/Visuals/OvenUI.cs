using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenUI : GameplayUI
{

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }



    public override void RecievePeek(int percent, int target, int leeway)
    {
        if (percent < (target - leeway))
        {
            child_SpriteRenderer.gameObject.SetActive(true);
        }

    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
    }
}
