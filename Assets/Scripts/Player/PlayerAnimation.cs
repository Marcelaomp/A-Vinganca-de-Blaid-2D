using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private OnGroundChecker _onGroundChecker;

    private Health _playerHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _onGroundChecker = GetComponent<OnGroundChecker>();
        _playerHealth = GetComponent<Health>();

        // GameManager.Instance.InputManager.OnAttack += Attack;
        
        _playerHealth.OnDamage += TakeDamage;
        _playerHealth.OnDeath += Die;
    }

    private void Update()
    {
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;
        _animator.SetBool("isMoving", isMoving);

        bool isJumping = ! _onGroundChecker.IsOnGround();
        _animator.SetBool("isJumping", isJumping);
    }

    private void Attack()
    {
        _animator.SetTrigger("attack");
    }

    private void TakeDamage()
    {
        _animator.SetTrigger("hurt");
    }

    private void Die()
    {
        if (_onGroundChecker.IsOnGround())
            _animator.SetTrigger("die");
    }
}
