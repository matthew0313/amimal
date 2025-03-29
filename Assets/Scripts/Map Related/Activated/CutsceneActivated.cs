using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActivated : Activated
{
    [SerializeField] int cutsceneNum;
    public override void OnActivation()
    {
        base.OnActivation();
        GameManager.Instance.M_NewCutsceneManager.StartCutscene(cutsceneNum);
    }
}
