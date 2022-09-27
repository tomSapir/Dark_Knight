using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager m_Instance;

    void Awake()
    {
        if(m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum eMusicType { MainMenu, LevelMusic, BossMusic }
    public AudioSource m_MainMenuMusic, m_LevelMusic, m_BossMusic;
    public AudioSource[] m_SFX;

    public void PlayMainMenuMusic()
    {
        m_LevelMusic.Stop();
        m_BossMusic.Stop();
        m_MainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if(!m_LevelMusic.isPlaying)
        {
            m_MainMenuMusic.Stop();
            m_BossMusic.Stop();
            m_LevelMusic.Play();
        }
    }

    public void PlayBossMusic()
    {
        m_LevelMusic.Stop();
        m_BossMusic.Play();
    }
}
