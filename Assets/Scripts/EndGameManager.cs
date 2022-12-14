using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private string m_MainMenuSceneName;

    void Start()
    {
        Destroy(GameObject.Find("UI_Canvas"));
    }


    public void OnClickMainMenuBtn()
    {
        SceneManager.LoadScene(m_MainMenuSceneName);
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
