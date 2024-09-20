using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggUI : GameplayUI
{
    [SerializeField] private EggScriptableObject eggSpriteData;
    private float timeFlipStart;
    [SerializeField] private float flipTime;
    private int currentEggBody;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        currentEggBody = 0;
    }

    void Update()
    {
        if (timeFlipStart + flipTime < Time.time) //unflip the egg
        {
            child_SpriteRenderer.sprite = eggSpriteData.eggEdge[currentEggBody];
        }
    }

    public override void RecievePeek(int percent, int target, int leeway)
    {
        timeFlipStart = Time.time;
        if (percent < (target - leeway))
        {
            child_SpriteRenderer.sprite = eggSpriteData.eggEdgeFlipped[0];
        }
        else if (percent >= (target - leeway) && percent <= (target + leeway))
        {
            child_SpriteRenderer.sprite = eggSpriteData.eggEdgeFlipped[1];
        }
        else if (percent > (target + leeway) && percent < 90)
        {
            child_SpriteRenderer.sprite = eggSpriteData.eggEdgeFlipped[2];
        }
        else {
            child_SpriteRenderer.sprite = eggSpriteData.eggEdgeFlipped[3];
        }
        
    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        m_SpriteRenderer.transform.localScale = new Vector3 (-1, 1, 1); //flip the egg
        if (percent < (target - leeway))
        {
            m_SpriteRenderer.sprite = eggSpriteData.eggBody[0];
            currentEggBody = 0;
        }
        else if (percent >= (target - leeway) && percent <= (target + leeway))
        {
            m_SpriteRenderer.sprite = eggSpriteData.eggBody[1];
            currentEggBody = 1;
        }
        else if (percent > (target + leeway) && percent < 90)
        {
            m_SpriteRenderer.sprite = eggSpriteData.eggBody[2];
            currentEggBody = 2;
        }
        else
        {
            m_SpriteRenderer.sprite = eggSpriteData.eggBody[3];
            currentEggBody = 3;
        }
    }
}
