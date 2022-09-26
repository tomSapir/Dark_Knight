using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string m_NewGameScene;

    void Start()
    {
        
    }

    public void OnClickNewGameBtn()
    {
        SceneManager.LoadScene(m_NewGameScene);
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
