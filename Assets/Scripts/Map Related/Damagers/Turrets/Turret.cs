using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : DamageSource
{
    [SerializeField] protected float fireRate, bulletSpeed, range;
    [SerializeField] protected Transform rotatePart, firePoint;
    [SerializeField] protected GameObject bullet;
    protected Transform player;
    protected TurretTarget target;
    protected bool lockedOn = false;
    protected float counter = 0.0f;
    protected Vector2 dir;
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player == null)
            {
                player = collision.transform;
                target = player.GetComponent<TurretTarget>();
            }
            target.TargetAdd(this);
            lockedOn = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target.TargetRemove(this);
            lockedOn = false;
        }
    }
    public virtual void Update()
    {
        if (lockedOn && counter < fireRate) counter += Time.deltaTime;
        if(lockedOn)
        {
            dir = player.position - rotatePart.position;
            dir.Normalize();
            rotatePart.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
    }
}
