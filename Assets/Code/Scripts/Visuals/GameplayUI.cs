using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class GameplayUI : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer m_SpriteRenderer;
    [SerializeField] protected SpriteRenderer child_SpriteRenderer;
    [SerializeField] protected SpriteRenderer effectRenderer;
    [SerializeField] protected TextMeshProUGUI m_MedalText; //placeholder, wanted a medal image but alas
    private string medalString;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        medalString = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void RecievePeek(int percent, int target, int leeway);
    public abstract void RecieveProgress(int percent, int target, int leeway);
    public void UpdateMedalText(Medal medal)
    {
        if (m_MedalText != null)
        {
            switch (medal)
            {
                case Medal.NONE:
                    medalString = "No Medal";
                    break;
                case Medal.BRONZE:
                    medalString = "Bronze Medal";
                    break;
                case Medal.SILVER:
                    medalString = "Silver Medal";
                    break;
                case Medal.GOLD:
                    medalString = "Gold Medal";
                    break;
            }
            m_MedalText.text = medalString;
        }
    }

}
