using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrilledCheeseUI : GameplayUI
{
    public GrilledCheeseScriptableObject grilledCheeseSpriteData;
    private float timeFlipStart;
    [SerializeField] private float flipTime;
    private int currentSandwichBody;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        currentSandwichBody = 0;
    }

    void Update()
    {
        if (timeFlipStart + flipTime < Time.time) //unflip the egg
        {
            m_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichBody[currentSandwichBody];
            m_SpriteRenderer.gameObject.SetActive(true);
            child_SpriteRenderer.gameObject.SetActive(false);
        }
    }

    public override void RecievePeek(int percent, int target, int leeway)
    {
        timeFlipStart = Time.time;
        if (percent < 20)
        {
            m_SpriteRenderer.gameObject.SetActive(false);
            child_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichFlipped[0];
            child_SpriteRenderer.gameObject.SetActive(true);
        }
        else if (percent < (target - leeway))
        {
            m_SpriteRenderer.gameObject.SetActive(false);
            child_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichFlipped[1];
            child_SpriteRenderer.gameObject.SetActive(true);
        }
        else if (percent >= (target - leeway) && percent <= (target + leeway))
        {
            m_SpriteRenderer.gameObject.SetActive(false);
            child_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichFlipped[2];
            child_SpriteRenderer.gameObject.SetActive(true);
        }
        else
        {
            m_SpriteRenderer.gameObject.SetActive(false);
            child_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichFlipped[3];
            child_SpriteRenderer.gameObject.SetActive(true);
        }

    }

    public override void RecieveProgress(int percent, int target, int leeway)
    {
        if (percent < 20)
        {
            m_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichBody[0];
            currentSandwichBody = 0;
        }
        else if (percent < (target - leeway))
        {
            m_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichBody[1];
            currentSandwichBody = 1;
        }
        else if (percent >= (target - leeway) && percent <= (target + leeway))
        {
            m_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichBody[2];
            currentSandwichBody = 2;
        }
        else
        {
            m_SpriteRenderer.sprite = grilledCheeseSpriteData.sandwichBody[3];
            currentSandwichBody = 3;
        }
    }
}
