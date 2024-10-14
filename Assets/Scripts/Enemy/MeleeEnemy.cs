using System.Security.Cryptography;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private PlayerDetector _playerDetector;
    private float _cooldownTimer;
    [SerializeField] private float _attackCooldown;
    private Health _enemyHealth;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        _playerDetector = GetComponent<PlayerDetector>();
        _enemyHealth = GetComponent<Health>();

        _enemyHealth.OnDamage += TakeDamage;
        _enemyHealth.OnDeath += Die;
    }

    protected override void Update()
    {
        _cooldownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

    private void VerifyCanAttack()
    {
        if (isReadyToAttack() && _playerDetector.IsNearPlayer())
            Attack();
    }

    private void Attack()
    {
        _cooldownTimer = 0;
        animator.SetTrigger("attack");
        if (_playerDetector.getPlayerCollider().TryGetComponent(out Health playerHealth))
        {
            playerHealth.reduceLifeCount();
        }
    }

    private bool isReadyToAttack()
    {
        return _cooldownTimer > _attackCooldown;
    }

    private void TakeDamage()
    {
        animator.SetTrigger("hurt");
    }

    private void Die()
    {
        animator.SetTrigger("die");
    }
}
