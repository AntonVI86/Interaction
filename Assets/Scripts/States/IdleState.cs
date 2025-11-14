using UnityEngine;

public class IdleState : IState
{
    private Animator _animator;

    public IdleState(Animator animator)
    {
        _animator = animator;
    }

    public void ApplyState()
    {
        _animator.Play(AnimationKeys.IdleAnimationKey);
    }
}
