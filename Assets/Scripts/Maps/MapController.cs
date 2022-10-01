using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController m_Instance;

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

    public GameObject[] m_Maps;

    void Start()
    {
        foreach (GameObject map in m_Maps)
        {
            if (PlayerPrefs.GetInt("Map_" + map.name) == 1)
            {
                map.SetActive(true);
            }
        }
    }

    public void ActivateMap(string i_MapToActivate)
    {
        foreach(GameObject map in m_Maps)
        {
            if(map.name == i_MapToActivate)
            {
                map.SetActive(true);
                PlayerPrefs.SetInt("Map_" + i_MapToActivate, 1);
            }
        }
    }
}
