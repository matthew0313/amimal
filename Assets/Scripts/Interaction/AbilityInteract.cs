using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInteract : Interaction
{
    [HideInInspector] public Ability ability;
    public override void Start()
    {
        requireType = false;
        remove = false;
    }
    public override void Interact()
    {
        if (counter < cooldown) return;
        base.Interact();
        ability.OnInteract();
    }
}
