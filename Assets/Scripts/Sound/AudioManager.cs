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

    public void PlaySFX(int i_SFXIndex)
    {
        m_SFX[i_SFXIndex].Stop();
        m_SFX[i_SFXIndex].Play();
    }
    public void PlaySFXAdjusted(int i_SFXAdjust)
    {
        m_SFX[i_SFXAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(i_SFXAdjust);
    }

}
