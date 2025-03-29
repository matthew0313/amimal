using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTree : Movement
{
    public float treeRotateSpeed;
    public float midairSpeed;
    public float midairMaxSpeed;
    const float gravityScale = 3.0f;
    Vector2 finalLaunch = new Vector2(5.0f, 3.0f);
    public OnTree(MovementController c) : base(c)
    {
        
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (controller.joint.enabled == false)
        {
            RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size * 1.1f, 0.0f, Vector2.down, 0.0f, whatToScan);
            if(hit.collider != null)
            {
                controller.Switch(controller.basic);
            }
            rb.velocityX = Mathf.Clamp(rb.velocityX, -midairMaxSpeed, midairMaxSpeed);
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        anim.Play("idle");
        rb.freezeRotation = true;
        rb.velocity = Vector2.zero;
        controller.transform.rotation = Quaternion.Euler(Vector3.forward * 0.0f);
        GameManager.Instance.M_PlayerCharacter.canChange = true;
    }
    public override void OnSwitch()
    {
        base.OnSwitch();
        anim.Play("onTree");
        controller.antiAirMove = false;
        controller.joint.enabled = true;
        Physics2D.IgnoreLayerCollision(3, 7, true);
        rb.freezeRotation = false;
        rb.gravityScale = gravityScale;
        GameManager.Instance.M_PlayerCharacter.canChange = false;
    }
    public override void MoveRight()
    {
        base.MoveRight();
        if (controller.joint.enabled)
        {
            rb.AddForce(Vector2.right * treeRotateSpeed * Time.deltaTime * 0.01f);
            renderer.flipX = false;
        }
        else
        {
            rb.velocityX = Mathf.Min(rb.velocityX + midairSpeed * Time.deltaTime, midairMaxSpeed);
        }
    }
    public override void MoveLeft()
    {
        base.MoveLeft();
        if (controller.joint.enabled)
        {
            rb.AddForce(Vector2.left * treeRotateSpeed * Time.deltaTime * 0.01f);
            renderer.flipX = true;
        }
        else
        {
            rb.velocityX = Mathf.Max(rb.velocityX - midairSpeed * Time.deltaTime, -midairMaxSpeed);
        }
    }
    public override void Jump()
    {
        base.Jump();
        if (controller.joint.enabled)
        {
            controller.joint.enabled = false;
            anim.Play("idle");
            Physics2D.IgnoreLayerCollision(3, 7, false);
            rb.AddForce(new Vector2(finalLaunch.x * ((rb.velocityX > 0) ? 1.0f : -1.0f), finalLaunch.y) * 0.01f);
        }
    }
    public void ReConnect()
    {
        controller.joint.enabled = true;
        anim.Play("onTree");
        Physics2D.IgnoreLayerCollision(3, 7, true);
    }
}
