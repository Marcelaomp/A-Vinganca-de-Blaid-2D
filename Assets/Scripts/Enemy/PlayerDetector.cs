using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private Transform _playerDetectorObject;
    [SerializeField] private Vector2 _playerDetectorSize;
    [SerializeField] private LayerMask _playerLayer;

   public Collider2D getPlayerCollider() 
    {
        return Physics2D.OverlapBox(_playerDetectorObject.position, _playerDetectorSize, 0f, _playerLayer);
    }

    public bool IsNearPlayer() 
    {
        Collider2D playerCollider = getPlayerCollider();
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if(_playerDetectorObject != null)
        {
            if (IsNearPlayer()) 
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawWireCube(_playerDetectorObject.position, _playerDetectorSize);
        }
    }
}
