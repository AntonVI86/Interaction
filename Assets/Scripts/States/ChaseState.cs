using UnityEngine;

public class ChaseState : IState
{
    private Transform _transform;
    private Transform _target;

    private Animator _animator;

    private IMoveable _mover;
    private IRotator _rotator;

    private float _maxDistanceToChase = 10f;

    public ChaseState(Transform target, Transform transform, IMoveable mover, IRotator rotator, Animator animator)
    {
        _target = target;

        _transform = transform;

        _mover = mover;
        _rotator = rotator;

        _animator = animator;
    }

    public void ApplyState()
    {
        _animator.Play(AnimationKeys.RunAnimationKey);

        Vector3 direction = _target.position - _transform.position;

        if (direction.magnitude > _maxDistanceToChase)
            return;

        _mover.ProcessMoveTo(direction.normalized);
        _rotator.ProcessRotateTo(direction);
    }
}
