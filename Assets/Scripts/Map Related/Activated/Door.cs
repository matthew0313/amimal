using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] public class Door : Activated
{
    [SerializeField] Animator anim;
    [SerializeField] bool moveCam;
    [SerializeField] CameraElements cam;
    [SerializeField] float endDelayTime = 3.0f, endImmortalTime = 2.0f;
    AudioSource sound;
    private void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.playOnAwake = false;
        sound.clip = GlobalManager.Instance.sounds.doorSound.clip;
        sound.volume = GlobalManager.Instance.sounds.doorSound.volume * GlobalManager.Instance.volume;
        GlobalManager.Instance.onVolumeChange.AddListener(VolumeChange);
    }
    private void VolumeChange()
    {
        sound.volume = GlobalManager.Instance.sounds.doorSound.volume * GlobalManager.Instance.volume;
    }
    public override void OnActivation()
    {
        base.OnActivation();
        if (moveCam)
        {
            GameManager.Instance.freeCam = true;
            GameManager.Instance.forcedImmortal = true;
            GameManager.Instance.M_PlayerMovements.canMove = false;
            GameManager.Instance.M_PlayerInteraction.canInteract = false;
            cam.endEvent = true;
            cam.Event.AddListener(MoveEnded);
            GameManager.Instance.M_CameraMovement.MoveCamera(cam);
        }
        else
        {
            sound.Play();
            anim.Play("open");
        }
    }
    public void MoveEnded()
    {
        sound.Play();
        anim.Play("open");
        StartCoroutine(EndWait());
    }
    IEnumerator EndWait()
    {
        yield return new WaitForSeconds(endDelayTime);
        GameManager.Instance.forcedImmortal = false;
        GameManager.Instance.Immortal(endImmortalTime);
        GameManager.Instance.freeCam = false;
        GameManager.Instance.M_PlayerMovements.canMove = true;
        GameManager.Instance.M_PlayerInteraction.canInteract = true;
    }
}
