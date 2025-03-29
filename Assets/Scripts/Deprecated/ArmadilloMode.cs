using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Armadillo))]public class ArmadilloMode : Interaction
{
    ArmadilloData data;
    [SerializeField] float moveSpeed, jumpPower, rotateSpeed;
    Armadillo armadillo;
    MovementController movements;
    public override void Update()
    {
        base.Update();
        if (counter < cooldown)
        {
            counter += Time.deltaTime;
        }
    }
    public override void Start()
    {
        base.Start();
        movements = GameManager.Instance.M_PlayerMovements;
        armadillo = GetComponent<Armadillo>();
        movements.spin.moveSpeed = moveSpeed;
        movements.spin.jumpPower = jumpPower;
        movements.spin.rotateSpeed = rotateSpeed;
        movements.spin.rotateObject = transform;
        remove = false;
        requireType = false;
    }
    public override void Interact()
    {
        base.Interact();
        if (counter < cooldown) return;
        counter = 0.0f;
        movements.CollisionReload();
        if (movements.current!=movements.spin)
        {
            armadillo.spinning = true;
            movements.Switch(movements.spin);
        }
        else
        {
            armadillo.spinning = false;
            movements.Switch(movements.basic);
        }
    }
}
