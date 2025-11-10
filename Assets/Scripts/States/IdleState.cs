using UnityEngine;

public class IdleState : IState
{
    public void ApplyState(Animator animator, Transform transform)
    {
        animator.Play(AnimationKeys.IdleAnimationKey);
    }
}
