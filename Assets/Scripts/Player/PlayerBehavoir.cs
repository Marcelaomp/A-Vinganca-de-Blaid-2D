using UnityEngine;

public class PlayerBehavoir : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _jumpForce = 6;

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
}
