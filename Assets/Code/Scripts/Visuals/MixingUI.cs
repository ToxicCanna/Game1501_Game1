using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MixingUI : GameplayUI
{
    [SerializeField] private TextMeshProUGUI m_Text;
    private bool shouldPlayerHold;
    private bool stickOnRight;
    private bool secondRound;
    [SerializeField] private MixingScriptableObject mixScriptableObject;

    void Start()
    {
        shouldPlayerHold = false;
        stickOnRight = true;
        secondRound = false;
    }

    public override void RecievePeek(int percent, int target, int leeway)
    {
        //changes the tmp text
        if (!shouldPlayerHold)
        {
            m_Text.text = "Hold the Button";
            shouldPlayerHold = true;
        }
        else
        {
            m_Text.text = "Release the Button";
            shouldPlayerHold = false;
        }
        
    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        //Change spoon location here 155f, 205f
        if (stickOnRight)
        {
            child_SpriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 205f);
            stickOnRight = false;
        }
        else
        {
            child_SpriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 155f);
            stickOnRight = true;
            secondRound = true;
        }
        if (secondRound)
        {
            m_SpriteRenderer.sprite = mixScriptableObject.mixingBowlBody[1];
        }

    }
}
