using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : DamageSource
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.GetDamage(damage, immortalTime, ignoreImmortal, true);
        }
    }
}
