using UnityEngine;

public class EscapeState : IState
{
    private Transform _transform;
    private Transform _target;

    private Animator _animator;

    private IMoveable _mover;
    private IRotator _rotator;

    private float _maxDistanceToEscape;

    public EscapeState(Transform target, Transform transform, IMoveable mover, IRotator rotator, Animator animator, float maxDistanceToEscape)
    {
        _target = target;
        _transform = transform;

        _mover = mover;
        _rotator = rotator;

        _animator = animator;

        _maxDistanceToEscape = maxDistanceToEscape;
    }

    public void ApplyState()
    {
        Vector3 direction = _target.position - _transform.position;

        if (direction.magnitude >= _maxDistanceToEscape)
        {           
            _animator.Play(AnimationKeys.AfraidAnimationKey);
            _mover.ResetVelocity();
            return;
        }

        _animator.Play(AnimationKeys.RunAnimationKey);
        _mover.ProcessMoveTo(-direction.normalized);
        _rotator.ProcessRotateTo(-direction);           
    }
}
