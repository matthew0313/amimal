using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree2 : Interaction
{
    [SerializeField] float anchorDist = 2.0f;
    MovementController controller;
    Rigidbody2D rb;
    public override void Start()
    {
        base.Start();
        requireType = true;
        requiredType = AnimalType.monkey;
        remove = true;
        controller = GameManager.Instance.M_PlayerMovements;
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Interact()
    {
        base.Interact();
        controller.joint.connectedAnchor = transform.position;
        controller.joint.anchor = new Vector2(0.0f, anchorDist);
        if (controller.current != controller.onTree)
        {
            controller.Switch(controller.onTree);
        }
        else
        {
            controller.onTree.ReConnect();
        }
    }
}
