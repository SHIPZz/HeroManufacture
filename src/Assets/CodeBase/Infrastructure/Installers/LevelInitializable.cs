using CodeBase.Common.Services.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInitializable : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform _heroSpawnPoint;
        
        private ILevelProvider _levelProvider;

        [Inject]
        private void Construct(ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
        }

        public void Initialize()
        {
            _levelProvider.HeroSpawnPoint = _heroSpawnPoint;
        }
    }
}