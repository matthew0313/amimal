using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform parent;
    public BackgroundElements[] background;
    public void OnUpdate()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].obj.position = new Vector2(Camera.main.transform.position.x * background[i].moveScale, background[i].obj.position.y);
        }
        parent.position = new Vector2(0.0f, Camera.main.transform.position.y);
    }
}
[System.Serializable] public struct BackgroundElements
{
    public Transform obj;
    public float moveScale;
}