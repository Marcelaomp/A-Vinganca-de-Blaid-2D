using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavoir : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _jumpForce = 6;
    
    [Header("Propriedades de ataque")]
    [SerializeField] private Transform _attackObject;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private OnGroundChecker _onGroundChecker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _onGroundChecker = GetComponent<OnGroundChecker>();
        GetComponent<Health>().OnDeath += HandlePlayerDeath;
    }

    void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;
    }

    void Update()
    {
        float movementDirection = MovePlayer();
        FlipPlayerAcordingToDirection(movementDirection);
    }

    private void HandleJump()
    {
        if (_onGroundChecker.IsOnGround())
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private float MovePlayer()
    {
        float movementDirection = GameManager.Instance.InputManager.Movement * _movementSpeed;
        _rigidbody.velocity = new Vector2(movementDirection, _rigidbody.velocity.y);
        return movementDirection;
    }

    private void FlipPlayerAcordingToDirection(float movementDirection)
    {
        if (movementDirection < 0)
        {
            _spriteRenderer.flipX = true;
            // transform.localScale = new Vector2(-1, 1);
        }
        else if (movementDirection > 0)
        {
            _spriteRenderer.flipX = false;
            //  transform.localScale = new Vector2(1, 1);
        }
    }

    private void HandlePlayerDeath()
    {
        GameManager.Instance.InputManager.DisablePlayerInput();
        if (_onGroundChecker.IsOnGround())
        {
            GetComponent<Collider2D>().enabled = false;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(_attackObject.position, _attackRange, enemyLayer);

        foreach (Collider2D hitEnemy in hitEnemies)
        {
            if (hitEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.reduceLifeCount();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackObject.position, _attackRange);
    }
}
