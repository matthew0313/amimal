using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Ability
{
    [SerializeField] MonkeyData data;
    public override void OnChange()
    {
        if (controller == null)
        {
            controller = GameManager.Instance.M_PlayerMovements;
            movement = controller.basic;
            commonData = data;
        }
        OnTree a = controller.onTree;
        a.midairSpeed = data.midairSpeed;
        a.midairMaxSpeed = data.midairMaxSpeed;
        a.treeRotateSpeed = data.treeRotateSpeed;
        base.OnChange();
    }
}
