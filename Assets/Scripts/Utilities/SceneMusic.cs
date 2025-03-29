using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] AudioSource source;
    private void Start()
    {
        GlobalManager.Instance.onSceneChange.AddListener(FadeOut);
        GlobalManager.Instance.onVolumeChange.AddListener(VolumeSync);
        source.volume = GlobalManager.Instance.volume;
    }
    public void FadeOut()
    {
        StartCoroutine(FadingOut());
    }
    IEnumerator FadingOut()
    {
        float a = GlobalManager.Instance.volume;
        for(int i = 0; i < 10; i++)
        {
            source.volume -= a;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void MusicPlay()
    {
        source.volume = GlobalManager.Instance.volume;
        source.Play();
    }
    public void MusicPlay(AudioClip changingClip)
    {
        source.clip = changingClip;
        source.volume = GlobalManager.Instance.volume;
        source.Play();
    }
    public void MusicStop()
    {
        source.Stop();
    }
    public void VolumeSync()
    {
        source.volume = GlobalManager.Instance.volume;
    }
}
