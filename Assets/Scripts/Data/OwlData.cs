using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Owl Data", menuName = "Scriptables/Character Data/Owl", order = 0)]
public class OwlData : CommonData
{
    [SerializeField] float m_glideFallSpeed, m_glideMoveSpeed, m_glideJumpPower, m_glideJumpCooldown, m_maxGlideSpeed, m_changeCooldown;
    [SerializeField] int m_glideJumpCount;

    public float glideFallSpeed { get { return m_glideFallSpeed; } }
    public float glideMoveSpeed { get { return m_glideMoveSpeed; } }
    public float glideJumpPower { get { return m_glideJumpPower; } }
    public float glideJumpCooldown { get {  return m_glideJumpCooldown; } }
    public float maxGlideSpeed { get { return m_maxGlideSpeed; } }
    public int glideJumpCount { get { return m_glideJumpCount; } }
    public float changeCooldown { get { return m_changeCooldown; } }
}
