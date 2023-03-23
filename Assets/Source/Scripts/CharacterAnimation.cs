using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _animatorSpeedHash = Animator.StringToHash("Speed");

    private void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        _animator.SetFloat(_animatorSpeedHash, Mathf.Abs(moveDirection));

        if(moveDirection > 0)
            _spriteRenderer.flipX = false;

        if (moveDirection < 0)
            _spriteRenderer.flipX = true;
    }
}
