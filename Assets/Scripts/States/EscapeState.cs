using UnityEngine;

public class EscapeState : IState
{
    private float _speed = 3f;

    private Transform _target;
    private Rigidbody _rigidbody;

    private IMoveable _mover;
    private IRotator _rotator;

    private float _maxDistanceToEscape = 5f;

    public EscapeState(Transform target, Rigidbody rigidbody)
    {
        _target = target;
        _rigidbody = rigidbody;

        _mover = new EnemyMover(_speed, _rigidbody);
        _rotator = new EnemyRotator();
    }

    public void ApplyState(Animator animator, Transform transform)
    {
        Vector3 direction = _target.position - transform.position;

        if (direction.magnitude > _maxDistanceToEscape)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            animator.Play(AnimationKeys.AfraidAnimationKey);
            return;
        }

        animator.Play(AnimationKeys.RunAnimationKey);
        _mover.ProcessMoveTo(-direction.normalized);
        _rotator.ProcessRotateTo(-direction, transform);           
    }
}
