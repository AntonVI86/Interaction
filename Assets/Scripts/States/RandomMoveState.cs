using UnityEngine;

public class RandomMoveState : IState
{
    private float _speed = 2f;
    private float _deadZone = 0.05f;

    private Rigidbody _rigidbody;

    private Vector3 _targetPoint;

    private Vector3 _defaultPosition;

    private EnemyMover _mover;
    private EnemyRotator _rotator;

    private float _minDistanceToDefaultPoint = -3f;
    private float _maxDistanceToDefaultPoint = 3f;

    public RandomMoveState(Rigidbody rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;

        _defaultPosition = transform.position;

        _mover = new EnemyMover(_speed, _rigidbody);
        _rotator = new EnemyRotator();

        SetTargetPoint();
    }

    public void ApplyState(Animator animator, Transform transform)
    {
        animator.Play(AnimationKeys.WalkAnimationKey);

        Vector3 direction = _targetPoint - transform.position;

        if (direction.magnitude <= _deadZone)
            SetTargetPoint();

        _mover.ProcessMoveTo(direction.normalized);
        _rotator.ProcessRotateTo(direction, transform);

    }

    private void SetTargetPoint()
    {
        _targetPoint = new Vector3(_defaultPosition.x + Random.Range(_minDistanceToDefaultPoint, _maxDistanceToDefaultPoint),
            _defaultPosition.y, _defaultPosition.z + Random.Range(_minDistanceToDefaultPoint, _maxDistanceToDefaultPoint));
    }
}
