using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Animator anim; 
    NewCutsceneManager manager;
    bool moveTime = false, endTime = false, cutsceneComponent = true;
    IEnumerator running;
    private void Start()
    {
        if (GetComponent<NewCutsceneManager>() == null)
        {
            cutsceneComponent = false;
        }
        else manager = GetComponent<NewCutsceneManager>();
    }
    public void MoveCamera(CameraElements element)
    {
        if (running != null) StopCoroutine(running);
        running = RunCameraMovement(element);
        StartCoroutine(running);
    }
    IEnumerator RunCameraMovement(CameraElements element)
    {
        if(element.trackObject != null)
        {
            element.movePos = new Vector3(element.trackObject.position.x, element.trackObject.position.y, element.movePos.z);
        }
        element.movePos = new Vector3(element.movePos.x, element.movePos.y, (element.movePos.z == 0) ? -10.0f : element.movePos.z);
        if (element.instant)
        {
            if (element.blackening)
            {
                anim.SetTrigger("Blackening");
                moveTime = false;
                while (true)
                {
                    if (moveTime)
                    {
                        if (element.movingCam == null)
                        {
                            Camera.main.transform.position = element.movePos;
                        }
                        else
                        {
                            element.movingCam.transform.position = element.movePos;
                        }
                        break;
                    }
                    yield return null;
                }
                endTime = false;
                while (true)
                {
                    if (endTime)
                    {
                        if (element.endEvent) element.Event.Invoke();
                        if (element.progressCutscene && cutsceneComponent) manager.CutsceneProgress();
                        break;
                    }
                    yield return null;
                }
            }
            else
            {
                if (element.movingCam == null)
                {
                    Camera.main.transform.position = element.movePos;
                }
                else
                {
                    element.movingCam.transform.position = element.movePos;
                }
                if (element.endEvent) element.Event.Invoke();
                if (element.progressCutscene && cutsceneComponent) manager.CutsceneProgress();
            }
        }
        else
        {
            while (true)
            {
                if (element.movingCam == null)
                {
                    if (Vector3.Distance(Camera.main.transform.position, element.movePos) <= 0.15f)
                    {
                        Camera.main.transform.position = element.movePos;
                        break;
                    }
                    else Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, element.movePos, element.moveSpeed * Time.deltaTime);
                }
                else
                {
                    if (Vector3.Distance(element.movingCam.transform.position, element.movePos) <= 0.15f)
                    {
                        element.movingCam.transform.position = element.movePos;
                        break;
                    }
                    else element.movingCam.transform.position = Vector3.Lerp(element.movingCam.transform.position, element.movePos, element.moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
            if (element.endEvent) element.Event.Invoke();
            if (element.progressCutscene && cutsceneComponent)
            {
                manager.CutsceneProgress();
            }
        }
    }
    public void MoveTime()
    {
        moveTime = true;
    }
    public void EndTime()
    {
        endTime = true;
    }
}
[System.Serializable] public struct CameraElements
{
    [Tooltip("이동 위치. track object가 있으면 무시.")] public Vector3 movePos;
    [Tooltip("이 물체의 위치로 이동.")] public Transform trackObject;
    [Tooltip("이동할 카메라.")] public Camera movingCam;
    [Space(5)][Tooltip("즉시 그 위치로 갈지 결정. blackening을 키면 어두워졌다가 함.")] public bool instant;
    public bool blackening;
    public float moveSpeed;
    [HideInInspector] public bool progressCutscene;
    [HideInInspector] public bool endEvent;
    [HideInInspector] public UnityEvent Event;
}
