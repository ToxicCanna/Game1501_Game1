using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeIcingUI : GameplayUI
{
    [SerializeField] private SpriteRenderer[] icingRenderers; //unlike butter, icing will have 2 renderer for each section, so 8 total in this case
    private int icingSection; //middlebottom,left, middletop, right are 0, 1, 2, 3 //perfect is at around 0.1 for scale of the icing
    private float icingHeight;
    private float overflowHeight = 0.2f;

    void Start()
    {
        icingHeight = 0.0f;
        icingSection = 0;
    }

    public override void RecievePeek(int percent, int target, int leeway)
    {
        //let's make this spread the butter. 
        //90% is at 1.7. so 100% is at 1.9 rounded first calc the value. 
        icingHeight = percent * 0.01f * overflowHeight;
        if (icingHeight > 0.1f) //overflows
        {
            icingRenderers[icingSection * 2].transform.localScale = new Vector3(0.1f, 0.1f, 1);
            icingRenderers[icingSection * 2 + 1].transform.localScale = new Vector3(0.1f, icingHeight - 0.1f, 1);
        }
        else
        {
            icingRenderers[icingSection*2].transform.localScale = new Vector3(0.1f, icingHeight, 1);
        }
        

    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        if (icingRenderers[icingSection * 2 + 1].transform.localScale.x < 0.15f)
        {
            icingRenderers[icingSection * 2 + 1].transform.localScale = new Vector3(0.1f, 0f, 1);
        }
        
        
        if (icingSection < 3)
        {
            icingSection++;
        }

    }
}
