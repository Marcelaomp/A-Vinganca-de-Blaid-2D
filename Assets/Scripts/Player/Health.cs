using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _lives;

    public event Action OnDeath;
    public event Action OnDamage;

    public void reduceLifeCount()
    {
        _lives--;
        HandleDamageTaken();
    }

    private void HandleDamageTaken()
    {
        if (_lives <= 0)
        {
            OnDeath?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }
    }
}
