using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActScene : MonoBehaviour
{
    [SerializeField] private int leeway; //leeway for this level
    
    //Structure basic game logic here. 

    public void RecordScore()
    { 
        //tells ScoreManager to add a score. 
    }

    public void SetTargets()
    { 
        //tells ScoreManager to set targets
    }
}
