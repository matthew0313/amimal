using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilloBreaker : Interaction
{
    Armadillo armadillo;
    [SerializeField] HorizontalDir breakDir;
    [SerializeField] Animator breakAnimation;
    public override void Start()
    {
        base.Start();
        armadillo = GameManager.Instance.animals[3] as Armadillo;
        requireType = true;
        requiredType = AnimalType.armadillo;
        if (breakAnimation == null && GetComponent<Animator>() != null) breakAnimation = GetComponent<Animator>();
    }
    public override void OnTriggerStay2D(Collider2D other)
    {
        if (armadillo.spinning == false) return;
        base.OnTriggerStay2D(other);
    }
    public override void Interact()
    {
        base.Interact();
        armadillo.StartBreak(breakDir, this);
    }
    public void Broken(HorizontalDir dir)
    {
        if (dir == HorizontalDir.Left) breakAnimation.Play("breakLeft");
        else breakAnimation.Play("break");
        interaction.Remove(this);
        interactable = false;
    }
}
