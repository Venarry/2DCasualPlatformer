using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement : MonoBehaviour, IImpulsable
{
    const string AxisHorizontal = "Horizontal";

    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _jumpHeight = 0.2f;
    [SerializeField] private float _jumpLength = 0.2f;
    [SerializeField] private float _gravityModifier = 1f;
    [Range(0, 0.99f)] [SerializeField] private float _impulseDurationMultiply = 0.9f;
    [Range (0, 1)] [SerializeField] private float _stepAngle = 0.65f;

    private float _colliderOffset;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveForce;
    private Vector2 _jumpForce;
    private Vector2 _impulse;
    private RaycastHit2D[] _hits = new RaycastHit2D[16];

    public float Velocity => _moveForce.magnitude;
    private float _moveDirection => Input.GetAxis(AxisHorizontal);
    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _colliderOffset = GetComponent<BoxCollider2D>().edgeRadius;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.Q))
            AddImpulse(new Vector2(-0.3f, 0.3f));
    }

    private void FixedUpdate()
    {
        SetMoveForce();
        Translate();
        JumpForceReduce();
        ImpulceReduce();
    }

    public void AddImpulse(Vector2 impulse)
    {
        _impulse += impulse;
    }

    private void SetMoveForce()
    {
        _moveForce.x = _moveDirection * _speed;
    }

    private void Translate()
    {
        int countOfHit;

        countOfHit = _rigidbody2D.Cast(_moveForce + _impulse, _hits, _colliderOffset);

        if(countOfHit == 0)
            _rigidbody2D.position += _moveForce + _impulse;

        for (int i = 0; i < countOfHit; i++)
        {
            Vector2 currentNormal = _hits[i].normal;

            if (currentNormal.y > _stepAngle)
            {
                _rigidbody2D.position += _moveForce + _impulse;
            }
        }

        //_rigidbody2D.position += _moveForce + _impulse;
        _rigidbody2D.position += _jumpForce;
    }

    private void Jump()
    {
        if (IsGrounded() == false)
            return;

        _jumpForce.y = _jumpHeight;
        _impulse += new Vector2(_jumpLength * _moveDirection, 0);
    }

    private void JumpForceReduce()
    {
        if(IsGrounded() == false)
        {
            _jumpForce.y -= _gravityModifier;
        }
        else if (_jumpForce.y + _impulse.y <= _gravityModifier)
        {
            _jumpForce.y = -0.01f;
        }
    }

    private void ImpulceReduce()
    {
        float minImpulce = 0.01f;

        if(_impulse.magnitude > minImpulce)
        {
            _impulse *= _impulseDurationMultiply;
        }
        else
        {
            _impulse = Vector2.zero;
        }
    }

    private bool IsGrounded() =>
        _rigidbody2D.Cast(Vector2.down, _hits, _colliderOffset) > 0;
}
