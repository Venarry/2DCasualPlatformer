using Unity.VisualScripting;
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

    private IInputsHandler _inputsHandler;
    private float _colliderOffset = 0.05f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveForce;
    private Vector2 _jumpForce;
    private Vector2 _impulse;
    private RaycastHit2D[] _hits = new RaycastHit2D[16];

    private float _moveDirection => _inputsHandler.MoveDirection.x;
    public float Velocity => _moveForce.magnitude;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _colliderOffset = GetComponent<BoxCollider2D>().edgeRadius;
    }

    public void Init(IInputsHandler inputsHandler)
    {
        _inputsHandler = inputsHandler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();

        SetMoveForce();
        Translate();
        ReduceJumpForce();
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
        Vector2 moveForce = (_moveForce + _impulse) * Time.deltaTime;
        Vector2 gravirtForce = _jumpForce * Time.deltaTime;
        int countOfHit = CastTo(moveForce, false);

        float moveForceMulti = 1;

        for (int i = 0; i < countOfHit; i++)
        {
            Vector2 currentNormal = _hits[i].normal;

            if (1 - Mathf.Abs(currentNormal.y) < _stepAngle)
            {
                moveForceMulti += Mathf.Abs(currentNormal.x);
                break;
            }
            else
            {
                moveForceMulti = 0;
                break;
            }
        }

        _rigidbody2D.MovePosition(
            _rigidbody2D.position + moveForce * moveForceMulti + gravirtForce);
    }

    private int CastTo(Vector2 direction, bool enableTriggerObjects)
    {
        int materialColliderHitsCount = 0;

        int countOfHit = _rigidbody2D.Cast(direction, _hits, _colliderOffset);

        if (enableTriggerObjects == false)
        {
            for (int i = 0; i < countOfHit; i++)
            {
                if (_hits[i].collider.isTrigger == false)
                {
                    materialColliderHitsCount++;
                }
            }

            return materialColliderHitsCount;
        }
        else
        {
            return countOfHit;
        }
    }

    private void TryJump()
    {
        if (IsGrounded() == false)
        {
            return;
        }

        _jumpForce.y = _jumpHeight;
        _impulse += new Vector2(_jumpLength * _moveDirection, 0);
    }

    private void ReduceJumpForce()
    {
        _jumpForce.y -= _gravityModifier;
        float stayGravity = -0.01f;

        if (IsGrounded() == true && _jumpForce.y + _impulse.y <= stayGravity)
        {
            _jumpForce.y = stayGravity;
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

    private bool IsGrounded()
    {
        int countOfHit = CastTo(Vector2.down, false);

        return countOfHit > 0;
    }
}
