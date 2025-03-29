using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTurret : Turret
{
    [SerializeField] SpriteRenderer beamWarner;
    [SerializeField] GameObject beamWarnObject;
    public override void Update()
    {
        base.Update();
        if (lockedOn)
        {
            beamWarnObject.SetActive(true);
            beamWarnObject.transform.localScale = new Vector2(beamWarner.transform.localScale.x, Vector2.Distance(beamWarnObject.transform.position, player.position));
            beamWarner.color = new Color(beamWarner.color.r, beamWarner.color.g, beamWarner.color.b, Mathf.Min(counter / fireRate, 1));
            if (counter >= fireRate)
            {
                GameManager.Instance.GetDamage(damage, immortalTime, ignoreImmortal, true);
                counter = 0.0f;
            }
        }
        else beamWarnObject.SetActive(false);
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") counter = 0.0f;
        base.OnTriggerExit2D(collision);
    }
}
