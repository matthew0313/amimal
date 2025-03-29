using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;
    public AdvancementManager advancements;
    public SoundManager sounds;

    [SerializeField] private GameObject[] _hidestage;

    const int sc = 5;
    public int stageCount = sc;
    public bool[] stageComplete = new bool[sc];
    public int[] stageRecords = new int[sc];
    public bool sceneChanging = false;
    public bool openingPlayed = false;

    public UnityEvent onSceneChange = new UnityEvent();
    public UnityEvent onVolumeChange = new UnityEvent();
    public float volume = 1.0f;

    public string username = "NoName";

    GameObject globalObject = null;
    Animator globalAnim = null;
    string changingScene;
    public static GlobalManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GlobalManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GlobalManager>();
                    singletonObject.name = "SelectManager (Singleton)";
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            globalObject = Instantiate(Resources.Load("Prefabs/DontDestroyObject") as GameObject);
            globalAnim = globalObject.transform.GetChild(0).GetComponent<Animator>();
            advancements = globalObject.transform.GetChild(1).GetComponent<AdvancementManager>();
            sounds = globalObject.transform.GetChild(2).GetComponent<SoundManager>();
            LoadData();
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(globalObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < stageCount; i++)
        {
            stageComplete[i] = PlayerPrefs.GetInt("Stage" + i, 0) > 0;
            stageRecords[i] = PlayerPrefs.GetInt("Time" + i, 0);
        }
        volume = PlayerPrefs.GetFloat("Volume", 1.0f);
    }
    public void LoadStage(int stageNum)
    {
        if (sceneChanging) return;
        sceneChanging = true;
        changingScene = "" + stageNum;
        onSceneChange.Invoke();
        globalAnim.SetTrigger("SceneChange");
    }
    public void LoadScene(string sceneName)
    {
        if (sceneChanging) return;
        sceneChanging = true;
        changingScene = sceneName;
        onSceneChange.Invoke();
        globalAnim.SetTrigger("SceneChange");
    }
    public void ExecuteChange()
    {
        sceneChanging = false;
        SceneManager.LoadScene(changingScene);
        globalAnim.SetTrigger("SceneChanged");
    }

    public void SetStageComplete(int stageNumber)
    {
        stageComplete[stageNumber] = true;
        PlayerPrefs.SetInt("Stage" + stageNumber, 1);
    }
    public void SetRecord(int stage, int record)
    {
        stageRecords[stage] = record;
        PlayerPrefs.SetInt("Time" + stage, record);
    }
    public void SetVolume(float setVolume)
    {
        volume = setVolume;
        PlayerPrefs.SetFloat("Volume", volume);
        onVolumeChange.Invoke();
    }
    public void DataReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("Volume", volume);
        LoadData();
        advancements.LoadData();
    }
    public void AddRank(int stage, int time)
    {
        PlayerPrefs.SetString("RankName" + stage + PlayerPrefs.GetInt("RankCount" + stage, 0), username);
        PlayerPrefs.SetInt("Rank" + stage + PlayerPrefs.GetInt("RankCount" + stage, 0), time);
        PlayerPrefs.SetInt("RankCount" + stage, PlayerPrefs.GetInt("RankCount" + stage, 0) + 1);
    }
}
