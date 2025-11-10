using UnityEngine;

public class ChaseState : IState
{
    private float _speed = 3f;

    private Transform _target;
    private Rigidbody _rigidbody;

    private IMoveable _mover;
    private IRotator _rotator;

    private float _maxDistanceToChase = 10f;

    public ChaseState(Transform target, Rigidbody rigidbody)
    {
        _target = target;
        _rigidbody = rigidbody;

        _mover = new EnemyMover(_speed, _rigidbody);
        _rotator = new EnemyRotator();
    }

    public void ApplyState(Animator animator, Transform transform)
    {
        animator.Play(AnimationKeys.RunAnimationKey);

        Vector3 direction = _target.position - transform.position;

        if (direction.magnitude > _maxDistanceToChase)
            return;

        _mover.ProcessMoveTo(direction.normalized);
        _rotator.ProcessRotateTo(direction, transform);
    }
}
