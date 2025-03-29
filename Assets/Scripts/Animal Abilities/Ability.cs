using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))] public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Animator bodyAnimator;
    [SerializeField] protected SpriteRenderer bodyRenderer;
    protected Movement movement;
    protected MovementController controller;
    protected CommonData commonData;
    [HideInInspector] public UnityEvent onChange;
    private void Awake()
    {
        bodyAnimator = GetComponent<Animator>();
        bodyRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void OnChange()
    {
        onChange.Invoke();
        bodyRenderer.color = new Color(1, 1, 1, 1);
        movement.moveSpeed = commonData.moveSpeed;
        movement.jumpPower = commonData.jumpPower;
        controller.Switch(movement, bodyAnimator, bodyRenderer);
    }
    public virtual void OnInteract()
    {

    }
    public virtual void OnExit()
    {

    }
    public void CanDisableJump()
    {
        GameManager.Instance.M_PlayerMovements.canDisableJump = true;
    }
}
