using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OwlFly : Interaction
{
    [SerializeField] float glideFallSpeed, glideMoveSpeed, glideJumpPower, glideJumpCooldown, maxGlideSpeed;
    [SerializeField] int glideJumpCount;
    [SerializeField] public Glide glide;
    MovementController movements;
    float tmp = 0.0f;
    bool gliding = false;
    public void OnSwitch()
    {
        if (movements == null)
        {
            Debug.Log("Works");
            movements = GameManager.Instance.M_PlayerMovements;
        }
        if (!movements.grounded) glide.jumpCounter = glide.glideJumpCount;
    }
    public override void Update()
    {
        base.Update();
        if (movements.grounded) glide.jumpCounter = 0;
    }
    public override void Start()
    {
        base.Start();
        remove = false;
        requireType = false;
        glide.glideFallSpeed = glideFallSpeed;
        glide.glideMoveSpeed = glideMoveSpeed;
        glide.glideJumpPower = glideJumpPower;
        glide.glideJumpCooldown = glideJumpCooldown;
        glide.maxGlideSpeed = maxGlideSpeed;
        glide.glideJumpCount = glideJumpCount;
        GameManager.Instance.M_NewCutsceneManager.atCutsceneStart.AddListener(CutsceneUnglide);
    }
    public override void Interact()
    {
        Debug.Log("Workssssssss");
        base.Interact();
        if (counter < cooldown) return;
        counter = 0.0f;
        if (true)
        {
            movements.antiAirMove = false;
            movements.rb.velocityX = Mathf.Clamp(movements.rb.velocityX, -maxGlideSpeed, maxGlideSpeed);
            movements.Switch(glide);
        }
        else
        {
            movements.antiAirMove = true;
        }
    }
    void CutsceneUnglide()
    {
    }
}
