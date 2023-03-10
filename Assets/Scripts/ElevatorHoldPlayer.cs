using UnityEngine;

public class ElevatorHoldPlayer : MonoBehaviour
{
    [SerializeField] private bool _PlayerOnPlatform = false;
    private GameObject _Player;
    private Vector3 _PrevPosition;

    private void Start()
    {
        _Player = GameObject.Find("Player");
        _PrevPosition = transform.position;
    }

    void Update()
    {
        if(_PlayerOnPlatform)
            _Player.transform.position -= (_PrevPosition - transform.position);

        _PrevPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _PlayerOnPlatform = true;
        }
            
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _PlayerOnPlatform = false;
        }
            
    }
}
