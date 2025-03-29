
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	public GameObject Option;

	public GameObject Title;

	[SerializeField] Animator anim;
	[SerializeField] SceneMusic music;
    private void Awake()
    {
		if (GlobalManager.Instance.openingPlayed == false)
		{
			music.MusicStop();
			anim.Play("Opening");
			GlobalManager.Instance.openingPlayed = true;
		}
    }

    public void OnClickGameQuit()
	{
		Application.Quit();
	}

	public void OnClickGameStart()
	{
		GlobalManager.Instance.LoadScene("StageSelect");
	}

	public void OnClickgoingMain()
	{
		Option.gameObject.SetActive(false);
		Title.gameObject.SetActive(true);
	}


	public void OnClickoption()
	{
		Option.gameObject.SetActive(true);
		Title.gameObject.SetActive(false);

	}

	public void Activate(GameObject a)
	{
		a.SetActive(true);
	}
	public void DeActivate(GameObject a)
	{
		a.SetActive(false);
	}
	public void DeleteData()
	{
		GlobalManager.Instance.DataReset();
	}
	public void DebugStageUnlock()
	{
		for(int i = 0; i < GlobalManager.Instance.stageCount; i++)
		{
            GlobalManager.Instance.SetStageComplete(i);
        }
	}
}
