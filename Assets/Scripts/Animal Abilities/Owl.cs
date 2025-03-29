using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityInteract))]public class Owl : Ability
{
    [SerializeField] OwlData data;
    [SerializeField] AbilityInteract interactor;
    [SerializeField] GameObject darkArea, nightVision;
    [SerializeField] Basic basicMoveset;
    PlayerInteraction interaction;
    public override void OnChange()
    {
        if (controller == null)
        {
            controller = GameManager.Instance.M_PlayerMovements;
            movement = controller.basic;
            commonData = data;
        }
        if (interactor == null)
        {
            interaction = GameManager.Instance.M_PlayerInteraction;
            interactor = GetComponent<AbilityInteract>();
            interactor.ability = this;
            interactor.cooldown = data.changeCooldown;
        }
        interactor.counter = interactor.cooldown;
        base.OnChange();
        Glide a = controller.glide;
        a.glideJumpPower = data.glideJumpPower;
        a.glideFallSpeed = data.glideFallSpeed;
        a.glideMoveSpeed = data.glideMoveSpeed;
        a.maxGlideSpeed = data.maxGlideSpeed;
        a.glideJumpCooldown = data.glideJumpCooldown;
        a.glideJumpCount = data.glideJumpCount;
        if (!GameManager.Instance.M_PlayerMovements.grounded) a.jumpCounter = a.glideJumpCount;
        darkArea.SetActive(false);
        nightVision.SetActive(true);
    }
    public override void OnInteract()
    {
        base.OnInteract();
        if(controller.current == controller.basic) {
            controller.Switch(controller.glide);
        }
        else
        {
            controller.Switch(controller.basic);
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        darkArea.SetActive(true);
        nightVision.SetActive(false);
        interaction.Remove(interactor);
    }
    private void LateUpdate()
    {
        if(GameManager.Instance.M_PlayerMovements.grounded == false)
        {
            interaction.Add(interactor);
        }   
        else
        {
            controller.glide.jumpCounter = 0;
            interaction.Remove(interactor);
        }
    }
}
