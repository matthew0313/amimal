using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armadillo Data", menuName = "Scriptables/Character Data/Armadillo", order = 0)]
public class ArmadilloData : CommonData
{
    [SerializeField][Tooltip("벽을 파괴하고 있을 때의 회전 속도")] float m_breakSpinSpeed;
    [SerializeField][Tooltip("스핀 도중 이동 속도")] float m_spinMoveSpeed;
    [SerializeField][Tooltip("스핀 도중 점프력")] float m_spinJumpPower;
    [SerializeField][Tooltip("스핀 도중 회전속도")] float m_spinRotateSpeed;
    [SerializeField]
    [Tooltip("스핀/일반 전환 쿨타임")] float m_changeCooldown;
    public float breakSpinSpeed { get { return m_breakSpinSpeed; } }
    public float spinMoveSpeed { get { return m_spinMoveSpeed; } }
    public float spinJumpPower { get {  return m_spinJumpPower; } }
    public float spinRotateSpeed { get { return m_spinRotateSpeed; } }
    public float changeCooldown { get {  return m_changeCooldown; } }

}
