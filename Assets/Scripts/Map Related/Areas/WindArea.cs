using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] public class WindArea : Area
{
    [SerializeField] Direction dir;
    [SerializeField] float pushSpeed;
    [SerializeField] float particleSpeed;
    [SerializeField] float particleSpawnRate;
    [SerializeField] GameObject particle;
    float particleRange, particleSpawnRange;
    bool vertical = false;
    MovementController controller;
    public List<WindParticle> particles = new List<WindParticle>();
    public override void Start()
    {
        base.Start();
        vertical = (dir == Direction.Up || dir == Direction.Down);
        particleRange = (vertical) ? collider.size.y : collider.size.x;
        particleSpawnRange = (vertical) ? collider.size.x * 0.5f : collider.size.y * 0.5f;
        StartCoroutine(particleGen());
    }
    public override void AreaEffect()
    {
        base.AreaEffect();
        if (controller == null) controller = GameManager.Instance.M_PlayerMovements;
        if (controller.current == controller.glide)
        {
            if (vertical)
            {
                Debug.Log("Works");
                if(dir == Direction.Down)
                {
                    controller.glide.pulledDown = true;
                }
                controller.rb.velocityY += pushSpeed * Time.deltaTime * ((dir == Direction.Down) ? -1 : 1);
            }
            else
            {
                controller.rb.velocityX += pushSpeed * Time.deltaTime * ((dir == Direction.Left) ? -1 : 1);
            }
        }
    }
    public override void AreaLeaveEffect()
    {
        base.AreaLeaveEffect();
        if (dir == Direction.Down) controller.glide.pulledDown = false;
    }
    IEnumerator particleGen()
    {
        while (true)
        {
            yield return new WaitForSeconds(particleSpawnRate);
            if(particles.Count == 0)
            {
                WindParticle a = Instantiate(particle, transform).GetComponent<WindParticle>();
                particles.Add(a);
                a.dir = dir;
                a.boundArea = this;
                a.speed = particleSpeed;
                a.dist = particleRange;
                if (vertical)
                {
                    a.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                }
                a.GetComponent<SpriteRenderer>().flipX = (dir == Direction.Left || dir == Direction.Up);
            }
            particles[0].gameObject.SetActive(true);
            if (vertical)
            {
                particles[0].Travel(new Vector2(transform.position.x + Random.Range(-particleSpawnRange, particleSpawnRange), transform.position.y + collider.size.y * 0.5f * ((dir==Direction.Up) ? -1 : 1)));
            }
            else
            {
                particles[0].Travel(new Vector2(transform.position.x + collider.size.x * 0.5f * ((dir == Direction.Right) ? -1 : 1), transform.position.y + Random.Range(-particleSpawnRange, particleSpawnRange)));
            }
            particles.RemoveAt(0);
        }
    }
}
