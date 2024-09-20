using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataForEggCracking", menuName = "ScriptableObjects/EggCrackingScriptableObject", order = 1)]
public class EggCrackingScriptableObject : ScriptableObject
{
    [SerializeField] public Sprite[] eggCrackingType;
}
