using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ContinueBtn;
    [SerializeField] private GameObject m_QuitBtn;
    [SerializeField] private PlayerAbillityTracker m_PlayerAbillityTracker;
    public string m_NewGameScene;

    void Start()
    {
        m_ContinueBtn.SetActive(PlayerPrefs.HasKey("ContinueLevel"));

        if(!m_ContinueBtn.activeSelf)
        {
            m_QuitBtn.transform.position = m_ContinueBtn.transform.position;
        }

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
       
        if(PlayerPrefs.GetInt("CanDash") == 1)
        {
            m_PlayerAbillityTracker.m_CanDash = true;
        }

        if (PlayerPrefs.GetInt("CanDoubleJump") == 1)
        {
            m_PlayerAbillityTracker.m_CanDoubleJump = true;
        }

        if (PlayerPrefs.GetInt("CanSpeed") == 1)
        {
            m_PlayerAbillityTracker.m_CanIncreaseSpeed = true;
        }

        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
