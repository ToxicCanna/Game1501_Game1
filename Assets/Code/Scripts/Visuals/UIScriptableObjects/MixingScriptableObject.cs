using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataForMixing", menuName = "ScriptableObjects/MixingScriptableObject", order = 4)]
public class MixingScriptableObject : ScriptableObject
{
    [SerializeField] public Sprite[] mixingBowlBody;
}
