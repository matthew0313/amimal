using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneCommands : MonoBehaviour
{
    [SerializeField] UnityEvent[] act;
    GameObject player { get { return GameManager.Instance.player; } }
    public void Activate(int actNum)
    {
        act[actNum].Invoke();
    }
    public void SetPlayerPosX(float x)
    {
        player.transform.position = new Vector2(x, player.transform.position.y);
    }
    public void SetPlayerPosY(float y)
    {
        player.transform.position = new Vector2(player.transform.position.x, y);
    }
    public void SetPlayerPosTransform(Transform pos)
    {
        player.transform.position = pos.position;
    }
    public void FlipPlayer(bool flipOrNot)
    {
        GameManager.Instance.M_PlayerMovements.renderer.flipX = flipOrNot;
    }
    public void SceneChangeFinish()
    {
        GlobalManager.Instance.sceneChanging = false;
    }
    public void ChangeScene()
    {
        GlobalManager.Instance.ExecuteChange();
    }
    public void ArmadilloArea(int inout)
    {
        (GameManager.Instance.animals[3] as Armadillo).Area(inout > 0);
    }
    public void HideObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void ShowObject(GameObject obj)
    {
        obj.SetActive(true);
    }
}
