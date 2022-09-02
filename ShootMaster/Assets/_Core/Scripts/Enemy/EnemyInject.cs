using UnityEngine;
using Zenject;

namespace _Core.Scripts.Enemy
{
    public class EnemyInject : Enemy
    {
        public class FactoryEnemyInject : PlaceholderFactory<EnemyInject> { }
    }
}
