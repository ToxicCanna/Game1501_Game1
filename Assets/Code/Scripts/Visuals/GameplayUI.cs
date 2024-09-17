using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class GameplayUI : MonoBehaviour
{
    protected SpriteRenderer m_SpriteRenderer;
    [SerializeField] protected SpriteRenderer child_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void RecievePeek(int percent, int target, int leeway);
    public abstract void RecieveProgress(int percent, int target, int leeway);
}
