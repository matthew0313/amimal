using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject[] _hidestage;
    [SerializeField] StageDesc[] stageDesc;
    [SerializeField] Animator anim;
    [SerializeField] Text stageNameText, stageDescText, recordText;
    [SerializeField] GameObject completion;
    [SerializeField] GameObject[] advancementObject;
    [SerializeField] GameObject rankObject, rankPrefab;
    [SerializeField] InputField nameInput;
    int loadingStage = 0;
    private void Start()
    {
        for (int i = 0; i < _hidestage.Length; i++)
        {
            if (i>0&&!GlobalManager.Instance.stageComplete[i-1]) _hidestage[i].SetActive(true);
            else _hidestage[i].SetActive(false);
        }
    }
    public void OpenStageMenu(int stage)
    {
        if (stage>0 && !GlobalManager.Instance.stageComplete[stage - 1]) return;
        loadingStage = stage;
        if (GlobalManager.Instance.stageComplete[stage]) completion.SetActive(false);
        else completion.SetActive(true);
        int timer = GlobalManager.Instance.stageRecords[stage];
        recordText.text = ((timer / 60 < 10) ? "0" + timer / 60 : (timer / 60)) + ":" + ((timer % 60 < 10) ? "0" + timer % 60 : (timer % 60));
        stageNameText.text = stageDesc[stage].stageName;
        stageDescText.text = stageDesc[stage].stageDescription;
        Advancement[] a = GlobalManager.Instance.advancements.Get(stage);
        for(int i = 0; i < advancementObject.Length; i++)
        {
            advancementObject[i].SetActive(false);
        }
        for(int i = 0; i < a.Length; i++)
        {
            advancementObject[i].SetActive(true);
            advancementObject[i].transform.GetChild(1).gameObject.SetActive(a[i].completed);
            advancementObject[i].transform.GetChild(2).GetComponent<Text>().text = a[i].dataFile.description;
        }
        List<Rank> ranks = new List<Rank>();
        for(int i = 0; i < PlayerPrefs.GetInt("RankCount" + stage, 0); i++)
        {
            Rank tmp;
            tmp.name = PlayerPrefs.GetString("RankName" + stage + i);
            tmp.time = PlayerPrefs.GetInt("Rank" + stage + i);
            ranks.Add(tmp);
        }
        ranks.Sort(delegate(Rank r1, Rank r2)
        {
            if (r1.time < r2.time) return 1;
            if (r1.time == r2.time) return 0;
            return -1;
        });
        for(int i = rankObject.transform.childCount-1; i >= 0; i--)
        {
            Destroy(rankObject.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < ranks.Count; i++)
        {
            Text tmp = Instantiate(rankPrefab, rankObject.transform).GetComponent<Text>();
            tmp.transform.localPosition = new Vector2(0.0f, 2490.0f - 20.0f * i);
            tmp.text = (i+1) + ": " + ranks[i].name + ": " + ((ranks[i].time / 60 < 10) ? "0" + ranks[i].time / 60 : (ranks[i].time / 60)) + ":" + ((ranks[i].time % 60 < 10) ? "0" + ranks[i].time % 60 : (ranks[i].time % 60));
        }
        nameInput.text = GlobalManager.Instance.username;
        anim.SetTrigger("Open");
    }
    public void CloseStageMenu()
    {
        anim.SetTrigger("Close");
    }
    public void StageLoad()
    {
        GlobalManager.Instance.LoadStage(loadingStage);
    }
    public void ReturnToMain()
    {
        GlobalManager.Instance.LoadScene("Main");
    }
    public void ChangeRankName()
    {
        GlobalManager.Instance.username = nameInput.text;
    }
}
[System.Serializable] struct StageDesc
{
    public string stageName;
    [TextArea] public string stageDescription;
}
[System.Serializable] struct Rank
{
    public string name;
    public int time;
}