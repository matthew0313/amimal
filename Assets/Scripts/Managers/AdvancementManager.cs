using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancementManager : MonoBehaviour
{
    public List<Advancement[]> data = new List<Advancement[]>();
    private void Awake()
    {
        LoadData();
    }
    public void LoadData()
    {
        data.Clear();
        for (int i = 0; i < GlobalManager.Instance.stageCount; i++)
        {
            object[] obj = Resources.LoadAll("Data/Advancements/" + i);
            Advancement[] a = new Advancement[obj.Length];
            for (int k = 0; k < obj.Length; k++)
            {
                a[k].dataFile = obj[k] as AdvancementData;
                a[k].completed = PlayerPrefs.GetInt(i + "-" + k, 0) > 0;
            }
            data.Add(a);
        }
    }
    public Advancement[] Get(int stage)
    {
        return data[stage];
    }
    public void Complete(int stage, int num)
    {
        PlayerPrefs.SetInt(stage + "-" + num, 1);
        data[stage][num].completed = true;
    }
}
[System.Serializable] public struct Advancement
{
    public AdvancementData dataFile;
    public bool completed;
}

