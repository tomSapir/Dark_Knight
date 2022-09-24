using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController m_Instance;

    void Awake()
    {
        m_Instance = this;
    }

    [SerializeField] private Slider m_HealthSlider;

    public void UpdateHealth(int i_CurrentHealth, int i_MaxHealth)
    {
        m_HealthSlider.maxValue = i_MaxHealth;
        m_HealthSlider.value = i_CurrentHealth;
    }
}
