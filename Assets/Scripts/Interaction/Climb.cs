using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : Interaction
{
    [SerializeField] HorizontalDir climbFrom;
    [SerializeField] float upspeed;
    public Rigidbody2D Player;
    [SerializeField] bool climbing = false;
    MovementController controller;
    public override void Start()
    {
        base.Start();
        requireType = true;
        requiredType = AnimalType.monkey;
        Player = GameManager.Instance.M_PlayerMovements.rb;
        controller = GameManager.Instance.M_PlayerMovements;
    }
    public override void Update()
    {
        base.Update();
        if (climbing)
        {
            Player.velocityY = upspeed;
        }
    }

    void UnClimb()
    {
        climbing = false;
        controller.anim.SetBool("Climbing", false);
        if (climbFrom == HorizontalDir.Left) controller.moveLeft.RemoveListener(UnClimb);
        else controller.moveRight.RemoveListener(UnClimb);
    }
    // 1스테이지 중간에 옆으로 걸쳐진 나무 타는걸 실패하면 옆에 서 있는 큰나무 탈 수 있게끔 해놓음.
    public override void Interact()
    {

        base.Interact();
        if (!climbing)
        {
            climbing = true;
            controller.anim.SetBool("Climbing", true);
            controller.anim.Play("climb");
            if (climbFrom == HorizontalDir.Left) controller.moveLeft.AddListener(UnClimb);
            else controller.moveRight.AddListener(UnClimb);
        }
        else
        {
            climbing = false;
            controller.anim.SetBool("Climbing", false);
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        climbing = false;
        controller.anim.SetBool("Climbing", false);
        base.OnTriggerExit2D(other);
    }
}
