using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [HideInInspector] public bool requireType, remove;
    [HideInInspector] public AnimalType requiredType;
    protected PlayerInteraction interaction;
    public bool interactable = true;
    public int priority;
    public Sprite uniqueButton;
    public float cooldown = 0.0f;
    [HideInInspector] public float counter = 0.0f;
    public virtual void Update()
    {
        if (!remove)
        {
            if (counter < cooldown) counter += Time.deltaTime;
        }
    }
    public virtual void Start()
    {
        transform.tag = "InteractArea";
        counter = cooldown;
        interaction = GameManager.Instance.M_PlayerInteraction;
    }
    public virtual void Interact()
    {
        if (!remove) counter = 0.0f;
    } 
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && interaction.interactions.Contains(this) == false)
        {
            interaction.Add(this);
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && interaction.interactions.Contains(this))
        {
            interaction.Remove(this);
        }
    }
}
[System.Serializable]
public enum Direction
{
    Right, Left, Up, Down
}
[System.Serializable]
public enum HorizontalDir
{
    Right, Left
}