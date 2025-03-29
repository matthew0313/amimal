using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : DamageSource
{
    public float range;
    float counter = 0.0f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        counter = 0.0f;
    }
    private void Update()
    {
        counter += Vector2.Distance(Vector2.zero, new Vector2(rb.velocityX, rb.velocityY))*Time.deltaTime;
        if (counter >= range)
        {
            Destroy(gameObject); Debug.Log(counter);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.GetDamage(damage, immortalTime, ignoreImmortal, true);
        }
        if ((!collision.gameObject.CompareTag("Turret") && !collision.gameObject.CompareTag("Area") && !collision.gameObject.CompareTag("InteractArea")) && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "UI")
        {
            Destroy(gameObject);
        }
    }
}
