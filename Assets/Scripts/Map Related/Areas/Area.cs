using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]public class Area : MonoBehaviour
{
    protected BoxCollider2D collider;
    public virtual void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AreaEffect();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AreaLeaveEffect();
        }
    }
    public virtual void AreaEffect()
    {

    }
    public virtual void AreaLeaveEffect()
    {

    }
}
