using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ContinueBtn;
    [SerializeField] private PlayerAbillityTracker m_PlayerAbillityTracker;
    public string m_NewGameScene;

    void Start()
    {
        m_ContinueBtn.SetActive(PlayerPrefs.HasKey("ContinueLevel"));
        AudioManager.m_Instance.PlayMainMenuMusic();
    }

    public void OnClickNewGameBtn()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(m_NewGameScene);
    }

    public void OnClickContinueBtn()
    {
        m_PlayerAbillityTracker.gameObject.SetActive(true);
        m_PlayerAbillityTracker.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
