using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichStackingUI : GameplayUI
{
    [SerializeField] private SpriteRenderer[] sandwichRenderers;
    private int sandwichSection;
    [SerializeField] private float ingredientXMin;
    [SerializeField] private float ingredientXMax;
    private float ingredientXLocation;
    public override void RecievePeek(int percent, int target, int leeway)
    {
        ingredientXLocation = ingredientXMin + ((ingredientXMax - ingredientXMin) * percent / 100);
        sandwichRenderers[sandwichSection].transform.position = new Vector3(ingredientXLocation, sandwichRenderers[sandwichSection].transform.position.y, 0);
    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        

        if (sandwichSection < sandwichRenderers.Length)
        {
            sandwichSection++;
        }
        sandwichRenderers[sandwichSection].gameObject.SetActive(true);
    }
}
