using System;
using UnityEngine;

public class FightHealth : MonoBehaviour
{
    public Action<int> Damaged;

    [SerializeField] private bool _isPlayer;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _fightCanvas;

        public void TakeDamage(int damage)
    {
        _health -= damage;
        
        Damaged?.Invoke(_health);
        TryDie();
    }

    private void TryDie()
    {
        if (_health <= 0)
        {
            if (_isPlayer)
            {
                _playerHealth.TakeDamage(10000);
            }
            else
            {
                _victoryPanel.SetActive(true);
                _fightCanvas.SetActive(false);
                _animator.SetBool("Die", true);
            }
        }
    }
}
