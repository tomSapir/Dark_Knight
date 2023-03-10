using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _RigidBody;
    [SerializeField] private float  _Speed = 2f;
    [SerializeField] private bool _IsHorizontal;
    [SerializeField] private bool _HitTrigger;
    [SerializeField] private bool _IsMovingUp;

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(!_IsHorizontal) // vertical
        {
            // Moving up
            if(_IsMovingUp && !_HitTrigger)
            {
                _RigidBody.velocity = Vector2.up * _Speed;
            }

            // Moving down
            if(!_IsMovingUp && !_HitTrigger)
            {
                _RigidBody.velocity = Vector2.down * _Speed;
            }
        }

        if (_IsHorizontal) // horizontal
        {
            // Moving right
            if (_IsMovingUp && !_HitTrigger)
            {
                _RigidBody.velocity = Vector2.right * _Speed;
            }

            // Moving left
            if (!_IsMovingUp && !_HitTrigger)
            {
                _RigidBody.velocity = Vector2.left * _Speed;
            }
        }
    }

    void ChangeDirection()
    {
        _IsMovingUp = !_IsMovingUp;
        _HitTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D i_Other)
    {
        if(i_Other.gameObject.tag == "Elevator Trigger")
        {
            _HitTrigger = true;
            _RigidBody.velocity = Vector2.zero;
            Invoke("ChangeDirection", 3);
        }
    }
}
