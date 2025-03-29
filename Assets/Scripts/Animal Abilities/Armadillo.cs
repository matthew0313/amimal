using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityInteract))]public class Armadillo : Ability
{
    [SerializeField] ArmadilloData data;
    [SerializeField] GameObject timingGUI;
    [SerializeField] AbilityInteract interactor;
    public bool spinning = false;
    public bool changing = false;
    bool inArea = false, succeed = false;
    ArmadilloBreaker currentBreaking;
    HorizontalDir currentDir;
    PlayerInteraction interaction;
    IEnumerator rotating;
    public override void OnChange()
    {
        if (controller == null)
        {
            controller = GameManager.Instance.M_PlayerMovements;
            movement = controller.basic;
            commonData = data;
        }
        base.OnChange();
        Spin a = controller.spin;
        a.moveSpeed = data.spinMoveSpeed;
        a.jumpPower = data.spinJumpPower;
        a.rotateSpeed = data.spinRotateSpeed;
        a.rotateObject = transform;
        if (interactor == null)
        {
            interaction = GameManager.Instance.M_PlayerInteraction;
            interactor = GetComponent<AbilityInteract>();
            interactor.ability = this;
            interactor.cooldown = data.changeCooldown;
        }
        interactor.counter = interactor.cooldown;
        interaction.Add(interactor);
    }
    public override void OnExit()
    {
        base.OnExit();
        spinning = false;
        interaction.Remove(interactor);
    }
    public override void OnInteract()
    {
        base.OnInteract();
        if(controller.current == controller.basic)
        {
            bodyAnimator.Play("modeChange");
            spinning = true;
            GameManager.Instance.M_PlayerCharacter.canChange = false;
            controller.canJump = false;
        }
        else
        {
            bodyAnimator.Play("modeChangeBack");
            controller.Switch(controller.basic);
            spinning = false;
            GameManager.Instance.M_PlayerCharacter.canChange = false;
            controller.canJump = false;
        }
    }
    public void Area(bool inOut)
    {
        inArea = inOut;
    }
    public void BreakTime()
    {
        if (succeed)
        {
            currentBreaking.Broken(currentDir);
        }
    }
    public void StartBreak(HorizontalDir dir, ArmadilloBreaker broken)
    {
        StartCoroutine(RunBreak(dir, broken));
    }
    public void Finish()
    {
        GameManager.Instance.M_PlayerInteraction.canInteract = true;
        GameManager.Instance.M_PlayerInteraction.canAddInteract = true;
        GameManager.Instance.M_PlayerMovements.updating = true;
        GameManager.Instance.M_PlayerMovements.canMove = true;
        GameManager.Instance.M_PlayerCharacter.canChange = true;
    }
    IEnumerator RunBreak(HorizontalDir dir, ArmadilloBreaker broken)
    {
        
        currentBreaking = broken;
        currentDir = dir;
        GameManager.Instance.M_PlayerInteraction.canInteract = false;
        GameManager.Instance.M_PlayerInteraction.canAddInteract = false;
        GameManager.Instance.M_PlayerMovements.canMove = false;
        GameManager.Instance.M_PlayerMovements.updating = false;
        GameManager.Instance.M_PlayerCharacter.canChange = false;
        bodyAnimator.SetBool("Moving", true);
        bodyAnimator.SetBool("Grounded", true);
        if (dir == HorizontalDir.Left)
        {
            bodyRenderer.flipX = true;
        }
        else bodyRenderer.flipX = false;
        inArea = false;
        timingGUI.SetActive(true);
        GameManager.Instance.anim.SetBool("ArmadilloTiming", true);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                succeed = inArea;
                timingGUI.SetActive(false);
                if (dir == HorizontalDir.Left)
                {
                    bodyAnimator.Play("breakJumpLeft");
                }
                else bodyAnimator.Play("breakJump");
                GameManager.Instance.anim.SetBool("ArmadilloTiming", false);
                break;
            }
            yield return null;
        }
    }
    public void Switch()
    {
        if (spinning)
        {
            controller.Switch(controller.spin);
        }
        GameManager.Instance.M_PlayerCharacter.canChange = true;
        controller.canJump = true;
    }
}
