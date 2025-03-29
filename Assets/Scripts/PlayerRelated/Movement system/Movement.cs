using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(MovementController))] public class Movement
{
    public float moveSpeed, jumpPower;

    protected MovementController controller;
    protected Animator anim;
    protected SpriteRenderer renderer;
    [SerializeField] protected BoxCollider2D collider;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected LayerMask whatToScan;

    public Movement(MovementController c)
    {
        controller = c;
        collider = controller.collider;
        rb = controller.rb;
        whatToScan = controller.whatToScan;
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnFixedUpdate()
    {

    }
    public virtual void OnSwitch()
    {
        anim = controller.anim;
        renderer = controller.renderer;
    }

    //기본 오른쪽 이동. Override 할 시 base.MoveRight()를 사용하지 않으면 변경 가능
    public virtual void MoveRight()
    {

    }

    //기본 왼쪽 이동. Override 할 시 base.MoveLeft()를 사용하지 않으면 변경 가능
    public virtual void MoveLeft() 
    {

    }
    public virtual void FixedMoveRight()
    {

    }
    public virtual void FixedMoveLeft()
    {

    }
    public virtual void Jump()
    {
        
    }
    public virtual void OnExit()
    {

    }
}
