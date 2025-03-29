using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86.Avx;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentStage = 0;
    public GameObject player;
    public MovementController M_PlayerMovements;
    public NewCutsceneManager M_NewCutsceneManager;
    public PlayerInteraction M_PlayerInteraction;
    public PlayerCharacter M_PlayerCharacter;
    public CameraMovement M_CameraMovement;
    public Animator anim;
    public Ability[] animals;
    public int timeLimit;
    public bool timeLimited;
    [SerializeField] Text timerText;
    [SerializeField] GameObject[] gameEndHides, gameEndShows, gameOverHides;
    [SerializeField] bool cutsceneStart = true;
    [SerializeField] StartPipe startPipe = null;
    [SerializeField] GameObject newRecordText;
    [SerializeField] float blinkSpeed = 0.5f, deathWaitTime = 1.0f;
    [Header("Statistics")] public bool paused = false;
    public float timer = 0.0f;
    public bool timerStopped = true;
    public bool freeCam = false;
    public bool immortal = false, forcedImmortal = false;
    public int health = 5;
    [Header("Music")] public SceneMusic music;
    [SerializeField] AudioClip gameOverMusic, gameClearMusic;
    [Header("Utility")] public GameObject playerTargetMark;
    [SerializeField] Animator[] healthHeart = new Animator[5];
    [Header("Advancements")] public Advancement[] advancements;
    [SerializeField] AdvObject[] advObjects;
    public bool[] advFailed;
    public Background background;
    bool justCompleted = false;
    bool gameOver = false;
    IEnumerator immortalAnimation;
    [HideInInspector] public UnityEvent onDamage, onTimeProgress;
    private void Awake()
    {
        M_PlayerMovements = player.GetComponent<MovementController>();
        M_PlayerCharacter = player.GetComponent<PlayerCharacter>();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance = this;
        for(int i = 0; i < player.transform.GetChild(0).childCount; i++)
        {
            animals[i] = player.transform.GetChild(0).GetChild(i).GetComponent<Ability>();
        }
        advancements = GlobalManager.Instance.advancements.Get(currentStage);
        advFailed = new bool[advancements.Length];
        AdvancementSet();
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }
    private void AdvancementSet()
    {
        for (int i = 0; i < advObjects.Length; i++)
        {
            for(int k = 0; k < advObjects[i].objects.Length; k++)
            {
                advObjects[i].objects[k].SetActive(false);
            }
        }
        for (int i = 0; i < advancements.Length; i++)
        {
            for (int k = 0; k < advObjects[i].objects.Length; k++)
            {
                advObjects[i].objects[k].SetActive(true);
                advObjects[i].objects[k].transform.GetChild(1).gameObject.SetActive(advancements[i].completed);
                advObjects[i].objects[k].transform.GetChild(2).GetComponent<Text>().text = advancements[i].dataFile.description;
            }
        }
        for (int i = 0; i < advancements.Length; i++)
        {
            AdvancementData b = advancements[i].dataFile;
            Criteria a = b.criteria;
            int e = i;
            if (a == Criteria.noAbility)
            {
                animals[(int)b.noAbility.restrictedType].onChange.AddListener(delegate { AdvancementFailure(e); });
            }
            if (a == Criteria.timeLimit)
            {
                onTimeProgress.AddListener(delegate { if (timer > advancements[e].dataFile.timeLimit.timeLimit) AdvancementFailure(e); });
            }
            if (a == Criteria.hpAbove)
            {
                if (b.hpAbove.noDamage)
                {
                    onDamage.AddListener(delegate { AdvancementFailure(e); });
                }
                else
                {
                    onDamage.AddListener(delegate { Debug.Log(health); if (health < advancements[e].dataFile.hpAbove.hpRequired) AdvancementFailure(e); });
                }
            }
        }
    }
    private void Start()
    {
        StartCoroutine(SceneChangeWait());
    }
    public void SceneChangeFinished()
    {
        if (cutsceneStart) M_NewCutsceneManager.StartCutscene(0);
        else if (startPipe != null) startPipe.StartGame(); 
    }
    private void Update()
    {
        if (!timerStopped)
        {
            timer += Time.deltaTime;
            timerText.text = ((timer / 60 < 10) ? "0" + (int)timer / 60 : ((int)timer / 60)) + ":" + ((timer % 60 < 10) ? "0"+(int)timer % 60 : ((int)timer % 60));
            onTimeProgress.Invoke();
        }
    }
    private void LateUpdate()
    {
        if (!freeCam && !gameOver)
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
        }
        if (background != null) background.OnUpdate();
    }
    public void EndStage()
    {
        timerStopped = true;
        player.SetActive(false);
        for(int i = 0; i < gameEndHides.Length; i++)
        {
            gameEndHides[i].SetActive(false);
        }
        for (int i = 0; i < gameEndShows.Length; i++)
        {
            gameEndShows[i].SetActive(true);
        }
        music.MusicPlay(gameClearMusic);
        anim.SetTrigger("StageEnd");
        if (!GlobalManager.Instance.stageComplete[currentStage])
        {
            GlobalManager.Instance.SetStageComplete(currentStage);
            justCompleted = true;
        }
        for(int i = 0; i < advancements.Length; i++)
        {
            if (advFailed[i] == false)
            {
                GlobalManager.Instance.advancements.Complete(currentStage, i);
            }
            advObjects[i].objects[1].transform.GetChild(1).gameObject.SetActive(advancements[i].completed);
        }
    }
    public void CompareRecord()
    {
        GlobalManager.Instance.AddRank(currentStage, (int)timer);
        if (justCompleted) GlobalManager.Instance.SetRecord(currentStage, (int)timer);
        else if ((int)timer < GlobalManager.Instance.stageRecords[currentStage])
        {
            GlobalManager.Instance.SetRecord(currentStage, (int)timer);
            newRecordText.SetActive(true);
        }
    }
    public void ChangeScene(string scene)
    {
        Time.timeScale = 1;
        GlobalManager.Instance.LoadScene(scene);
    }
    public void Replay()
    {
        Time.timeScale = 1;
        GlobalManager.Instance.LoadStage(currentStage);
    }
    public void NextStage()
    {
        if (GlobalManager.Instance.stageComplete.Length - 1 == currentStage) return;
        GlobalManager.Instance.LoadStage(currentStage + 1);
    }
    IEnumerator SceneChangeWait()
    {
        while (true)
        {
            if (!GlobalManager.Instance.sceneChanging)
            {
                break;
            }
            yield return null;
        }
        SceneChangeFinished();
    }
    public void GetDamage(int damage, float immortalTime, bool ignoreImmortal, bool screenRed)
    {
        if (forcedImmortal) return;
        if (!ignoreImmortal && immortal) return;
        for(int i = 0; i < damage && health-i-1 >= 0; i++)
        {
            healthHeart[health-i-1].SetTrigger("Break");
        }
        health -= damage;
        onDamage.Invoke();
        if (screenRed) anim.SetTrigger("TakeDmg");
        if (health <= 0) GameOver();
        else
        {
            StartCoroutine(ImmortalTime(immortalTime));
        }
    }
    public void Immortal(float immortalTime)
    {
        StartCoroutine(ImmortalTime(immortalTime));
    }
    IEnumerator ImmortalTime(float immortalTime)
    {
        immortal = true;
        IEnumerator playing = ImmortalAnim();
        StartCoroutine(playing);
        yield return new WaitForSeconds(immortalTime);
        StopCoroutine(playing);
        M_PlayerMovements.renderer.color = new Color(M_PlayerMovements.renderer.color.r, M_PlayerMovements.renderer.color.g, M_PlayerMovements.renderer.color.b, 1);
        immortal = false;
    }
    IEnumerator ImmortalAnim()
    {
        Color32 tmp = M_PlayerMovements.renderer.color;
        while (true)
        {
            M_PlayerMovements.renderer.color = new Color(tmp.r, tmp.g, tmp.b, 0);
            yield return new WaitForSeconds(blinkSpeed);
            M_PlayerMovements.renderer.color = new Color(tmp.r, tmp.g, tmp.b, 1);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
    public void GameOver()
    {
        gameOver = true;
        player.SetActive(false);
        music.FadeOut();
        StartCoroutine(DeathWait());
    }
    IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(deathWaitTime);
        for (int i = 0; i < gameOverHides.Length; i++)
        {
            gameOverHides[i].SetActive(false);
        }
        music.MusicPlay(gameOverMusic);
        anim.SetTrigger("GameOver");
    }
    void AdvancementFailure(int num)
    {
        advFailed[num] = true;
        advObjects[num].objects[0].transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, 0.5f);
        advObjects[num].objects[1].transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, 0.5f);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
[System.Serializable] public struct AdvObject
{
    public GameObject[] objects;
}