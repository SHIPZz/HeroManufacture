using UnityEngine;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroAnimator
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        
        private static readonly int MovingStateHash = Animator.StringToHash("Moving");
        private static readonly int CollectingStateHash = Animator.StringToHash("Collecting");

        private readonly Animator _animator;

        public HeroAnimator(Animator animator) => _animator = animator;

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(SpeedHash, speed);
        }

        public bool IsCollectingAnimationPlaying() =>
            IsAnimationPlaying(CollectingStateHash);

        private bool IsAnimationPlaying(int animationHash)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.shortNameHash == animationHash && stateInfo.normalizedTime < 1f;
        }
    }
}