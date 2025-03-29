using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monkey Data", menuName = "Scriptables/Character Data/Monkey", order = 0)]
public class MonkeyData : CommonData
{
    [SerializeField] float m_midairSpeed, m_midairMaxSpeed, m_treeRotateSpeed;
    public float midairSpeed { get { return m_midairSpeed; } }
    public float midairMaxSpeed { get { return m_midairMaxSpeed; } }
    public float treeRotateSpeed { get { return m_treeRotateSpeed; } }


}
