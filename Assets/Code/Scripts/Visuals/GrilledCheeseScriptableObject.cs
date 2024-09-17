using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataForGrilledCheese", menuName = "ScriptableObjects/GrilledCheeseScriptableObject", order = 1)]
public class GrilledCheeseScriptableObject : ScriptableObject
{
    [SerializeField] public Sprite[] sandwichBody;
    [SerializeField] public Sprite[] sandwichFlipped;
}
