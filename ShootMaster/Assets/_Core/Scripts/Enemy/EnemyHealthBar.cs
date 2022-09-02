using System;
using _Core.Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Enemy
{
    [RequireComponent(typeof(AbstractEnemy))]
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fill;
        [SerializeField] private GameObject _healthbar;
        private AbstractEnemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<AbstractEnemy>();
            _enemy.OnHit += Hit;
            _slider.value = _enemy.GetAbstractEnemyHealth();
            _fill.color = _gradient.Evaluate(1f);
        }

        private void Hit()
        {
            _slider.value--;
            _fill.color = _gradient.Evaluate(_slider.normalizedValue);
            if (_slider.value == 0)
            {
                _healthbar.SetActive(false);
                _enemy.OnHit -= Hit;
            }
        }
    }
}