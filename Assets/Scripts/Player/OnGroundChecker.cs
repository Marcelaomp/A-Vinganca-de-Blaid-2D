using UnityEngine;

public class OnGroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckerObject;
    [SerializeField] private Vector2 _groundCheckerSize;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsOnGround()
    {
        return Physics2D.OverlapBox(_groundCheckerObject.position, _groundCheckerSize, 0f, _groundLayer);
    }

    private void OnDrawGizmos() {
        if (IsOnGround())
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(_groundCheckerObject.position, _groundCheckerSize);
    }
}
