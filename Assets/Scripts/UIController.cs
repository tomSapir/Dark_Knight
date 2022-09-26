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

    public void UpdateHealth(int i_CurrentHealth, int i_MaxHealth)
    {
        m_HealthSlider.maxValue = i_MaxHealth;
        m_HealthSlider.value = i_CurrentHealth;
    }
}
