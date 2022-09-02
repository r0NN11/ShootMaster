using _Core.Scripts.Enemy;
using UnityEngine;
using Zenject;

namespace _Core.Scripts
{
    public class UntitledInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawner>().FromInstance(FindObjectOfType<EnemySpawner>(true)).AsSingle();
            Container.Bind<GameStateDirector>().AsSingle();
            Container.Bind<EnemyController>().AsSingle();
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
            CreateEnemy();
        }

        private void CreateEnemy()
        {
            string NAME_ENEMY = "Enemy_Variant";

            Container.BindFactory<EnemyInject, EnemyInject.FactoryEnemyInject>()
                .FromComponentInNewPrefabResource(NAME_ENEMY);
        }
    }
}