using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    private void Start()
    {
        slider.value = GlobalManager.Instance.volume;
    }
    public void VolumeChange()
    {
        GlobalManager.Instance.SetVolume(slider.value);
    }
}
