using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle : MonoBehaviour
{
    public WindArea boundArea;
    public float dist, speed;
    public Direction dir;
    bool vertical = false;
    public void Travel(Vector2 startPos)
    {
        vertical = (dir == Direction.Up || dir == Direction.Down);
        transform.position = startPos;
        StartCoroutine(Travelling());
    }
    IEnumerator Travelling()
    {
        float time = 0.0f;
        while (time * speed < dist)
        {
            float a = Time.deltaTime;
            transform.Translate(Vector2.right * speed * a * ((dir == Direction.Left || dir == Direction.Up) ? -1 : 1));
            time += a;
            yield return null;
        }
        boundArea.particles.Add(this);
        gameObject.SetActive(false);
    }
}
