using UnityEngine;

public class MapActivator : MonoBehaviour
{
    public string m_MapToActivate;

    void Start()
    {
        MapController.m_Instance.ActivateMap(m_MapToActivate);
    }
}
