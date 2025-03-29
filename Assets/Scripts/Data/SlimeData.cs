using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slime Data", menuName = "Scriptables/Character Data/Slime", order = 0)]
public class SlimeData : CommonData
{
    [SerializeField] float m_doubleJumpPower, m_dashPower, m_termDJ, m_termD;
    public float doubleJumpPower { get { return m_doubleJumpPower; } }
    public float dashPower { get { return m_dashPower; } }
    public float termDJ { get {  return m_termDJ; } }
    public float termD { get { return m_termD; } }
}
