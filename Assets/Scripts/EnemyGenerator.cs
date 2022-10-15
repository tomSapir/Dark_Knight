using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform m_WhereToGenerate;
    [SerializeField] private GameObject m_EnemyToGenerate;
    [SerializeField] private float m_GapBetweenGeneration;

    private float m_Counter;

    void Start()
    {
        m_Counter = m_GapBetweenGeneration;
    }


    void Update()
    {
        m_Counter -= Time.deltaTime;

        if(m_Counter <= 0)
        {
            Instantiate(m_EnemyToGenerate, m_WhereToGenerate.position, Quaternion.identity);
            m_Counter = m_GapBetweenGeneration;
        }
    }
}
