using CodeBase.Animations;
using CodeBase.UI.CharacterSelect.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.Cameras
{
    public class ApplyVignetteEffectOnCharacterSelect : MonoBehaviour
    {
        private const int SkipFirstSelection = 1;
        
        [SerializeField] private VignetteAnimator _vignetteAnimator;
        
        private ICharacterSelectionService _characterSelectionService;

        [Inject]
        private void Construct(ICharacterSelectionService characterSelectionService) => 
            _characterSelectionService = characterSelectionService;

        private void Start()
        {
            _characterSelectionService.CurrentCharacterId
                .Skip(SkipFirstSelection)
                .Subscribe(_ => _vignetteAnimator.Play())
                .AddTo(this);
        }
    }
}