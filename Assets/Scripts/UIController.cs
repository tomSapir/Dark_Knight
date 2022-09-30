using UnityEngine;
using UnityEngine.UI;

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
}
