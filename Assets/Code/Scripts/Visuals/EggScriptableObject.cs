using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataForEgg", menuName = "ScriptableObjects/EggScriptableObject", order = 1)]
public class EggScriptableObject : ScriptableObject
{
    [SerializeField] public Sprite[] eggBody;
    [SerializeField] public Sprite[] eggEdge;
    [SerializeField] public Sprite[] eggEdgeFlipped;
}
