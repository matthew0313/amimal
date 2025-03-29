using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : Movement
{
    public Glide(MovementController c) : base(c)
    {

    }

    public float glideFallSpeed, glideMoveSpeed, glideJumpPower, glideJumpCooldown, maxGlideSpeed;
    public int glideJumpCount;

    public int jumpCounter = 0;
    float timer = 0.0f;

    public bool pulledDown = false;
    public override void OnSwitch()
    {
        base.OnSwitch();
        anim.SetBool("Gliding", true);
        anim.Play("glide");
        controller.antiAirMove = false;
        rb.gravityScale = controller.riseGravity;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        #region GroundScan
        Vector3 offSet = new Vector3(0.0f, -0.01f, 0.0f);
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + offSet, collider.bounds.size, 0.0f, Vector2.down, 0.02f, whatToScan);
        if (hit.collider != null)
        {
            controller.Switch(controller.basic);
        }
        #endregion
        #region Timers
        if (timer < glideJumpCooldown) timer += Time.deltaTime;
        #endregion
        if (!pulledDown && rb.velocityY < -glideFallSpeed)
        {
            rb.velocityY = -glideFallSpeed;
            Debug.Log("POS");
        }
        Mathf.Clamp(rb.velocityX, -maxGlideSpeed, maxGlideSpeed);
    }
    public override void FixedMoveRight()
    {
        base.FixedMoveRight();
        renderer.flipX = false;
        rb.velocityX += glideMoveSpeed * Time.deltaTime;
        if (rb.velocityX > maxGlideSpeed)
        {
            rb.velocityX = maxGlideSpeed;
        }
    }
    public override void FixedMoveLeft()
    {
        base.FixedMoveLeft();
        renderer.flipX = true;
        rb.velocityX -= glideMoveSpeed * Time.deltaTime;
        if (rb.velocityX < -maxGlideSpeed) rb.velocityX = -maxGlideSpeed;
    }
    public override void Jump()
    {
        base.Jump();
        if (jumpCounter < glideJumpCount && timer >= glideJumpCooldown)
        {
            timer = 0.0f;
            jumpCounter++;
            rb.velocityY = glideJumpPower;
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        anim.SetBool("Gliding", false);
        controller.antiAirMove = true;
    }
}
