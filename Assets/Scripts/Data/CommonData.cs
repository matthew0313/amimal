using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class CommonData : ScriptableObject
{
    [SerializeField] float m_moveSpeed;
    public float moveSpeed { get { return m_moveSpeed; } }

    [SerializeField] float m_jumpPower;
    public float jumpPower { get { return m_jumpPower; } }
}
