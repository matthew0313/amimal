using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Advancement Data", menuName = "Scriptables/Advancement Data", order = 0)]
public class AdvancementData : ScriptableObject
{
    [TextArea][SerializeField] string m_description;
    [SerializeField] Criteria m_criteria;
    [SerializeField] TimeLimit m_timeLimit;
    [SerializeField] NoAbility m_noAbility;
    [SerializeField] HpAbove m_hpAbove;
    public string description { get { return m_description; } }
    public Criteria criteria { get { return m_criteria; }  }
    public TimeLimit timeLimit { get {  return m_timeLimit; } }
    public NoAbility noAbility { get {  return m_noAbility; } }
    public HpAbove hpAbove { get {  return m_hpAbove; } }
}
[System.Serializable] public enum Criteria
{
    timeLimit,
    noAbility,
    hpAbove
}
[System.Serializable] public struct TimeLimit
{
    public float timeLimit;
}
[System.Serializable] public struct NoAbility
{
    public AnimalType restrictedType;
}
[System.Serializable] public struct HpAbove
{
    public int hpRequired;
    public bool noDamage;
}
