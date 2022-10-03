using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController m_Instance;

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Slider m_HealthSlider;
    [SerializeField] private Image m_FadeScreen;
    [SerializeField] private float m_FadeSpeed = 2f;
    [SerializeField] private string m_MainMenuSceneName;
    [SerializeField] private GameObject m_PauseScreen;
    [SerializeField] private GameObject m_HowToPlayScreen;
    [SerializeField] private GameObject m_PauseControlsContainer;
    private bool m_FadingToBlack, m_FadingFromBlack;

    void Update()
    {
        if(m_FadingToBlack)
        {
            m_FadeScreen.color = new Color(m_FadeScreen.color.r, m_FadeScreen.color.g, m_FadeScreen.color.b, Mathf.MoveTowards(m_FadeScreen.color.a, 1f, m_FadeSpeed * Time.deltaTime));
            if(m_FadeScreen.color.a == 1f)
            {
                m_FadingToBlack = false;
            }
        }
        else if(m_FadingFromBlack)
        {
            m_FadeScreen.color = new Color(m_FadeScreen.color.r, m_FadeScreen.color.g, m_FadeScreen.color.b, Mathf.MoveTowards(m_FadeScreen.color.a, 0f, m_FadeSpeed * Time.deltaTime));
            if (m_FadeScreen.color.a == 0f)
            {
                m_FadingFromBlack = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAndUnpause();
        }
    }

    public void UpdateHealth(int i_CurrentHealth, int i_MaxHealth)
    {
        m_HealthSlider.maxValue = i_MaxHealth;
        m_HealthSlider.value = i_CurrentHealth;
    }

    public void StartFadeToBlack()
    {
        m_FadingToBlack = true;
        m_FadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        m_FadingToBlack = false;
        m_FadingFromBlack = true;
    }

    public void PauseAndUnpause()
    {
        if(!m_PauseScreen.activeSelf)
        {
            m_PauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            setSelectedButtonToNull();
            m_PauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void OnClickMainMenuBtnPauseMenu()
    {
        setSelectedButtonToNull();
        Time.timeScale = 1f;

        Destroy(PlayerHealthController.m_Instance.gameObject);
        PlayerHealthController.m_Instance = null;

        Destroy(RespawnController.m_Instance.gameObject);
        RespawnController.m_Instance = null;

        m_Instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(m_MainMenuSceneName);
    }

    public void OnClickQuitBtnPauseMenu()
    {
        Application.Quit();
    }

    public void OnClickHowToPlayBtn()
    {
        m_PauseControlsContainer.SetActive(false);
        m_HowToPlayScreen.SetActive(true);
    }

    public void OnClickBackFromHowToPlayBtn()
    {
        setSelectedButtonToNull();
        m_PauseControlsContainer.SetActive(true);
        m_HowToPlayScreen.SetActive(false);
    }

    private void setSelectedButtonToNull()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
