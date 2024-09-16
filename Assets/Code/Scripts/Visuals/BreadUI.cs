using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadUI : GameplayUI
{
    [SerializeField] private SpriteRenderer[] butterRenderers;
    private int butterSection; //left, middle, right are 0, 1, 2 //perfect is at around 1.7 for scale of the butter
    private float butterHeight;
    private float breadHeight = 1.9f;

    void Start()
    {
        butterHeight = 0.01f;
        butterSection = 0;
    }

    public override void RecievePeek(int percent, int target, int leeway)
    {
        //let's make this spread the butter. 
        //90% is at 1.7. so 100% is at 1.9 rounded first calc the value. 
        butterHeight = percent * 0.01f * breadHeight; 
        butterRenderers[butterSection].transform.localScale = new Vector3(1, butterHeight, 1);

    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        if (butterSection < 2)
        {
            butterSection++;
        }

    }
}