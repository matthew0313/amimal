using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region movement
    [Header("Movements")] public sound jumpSound;
    #endregion
    #region objects
    #region Activator&Activated
    [Header("Activated&Activators")]public sound buttonPress;
    public sound doorSound;
    #endregion
    #region Turrets
    [Header("Turrets")] public sound basicTurretFire;
    public sound laserTurretFire;
    public sound laserTurretCharge;
    #endregion
    #endregion
}
[System.Serializable] public struct sound
{
    public AudioClip clip;
    [Range(0, 1)] public float volume;
}

