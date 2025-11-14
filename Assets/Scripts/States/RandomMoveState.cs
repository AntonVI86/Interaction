using UnityEngine;

public class RandomMoveState : IState
{
    private float _deadZone = 0.05f;

    private Vector3 _targetPoint;

    private Transform _transform;
    private Vector3 _defaultPosition;

    private IMoveable _mover;
    private IRotator _rotator;

    private Animator _animator;

    private float _minDistanceToDefaultPoint = -3f;
    private float _maxDistanceToDefaultPoint = 3f;

    public RandomMoveState(Transform transform, Transform defaultPoint, IMoveable movable, IRotator rotator, Animator animator)
    {
        _defaultPosition = defaultPoint.position;
        _transform = transform;

        _mover = movable;
        _rotator = rotator;

        SetTargetPoint();
        _animator = animator;
    }

    public void ApplyState()
    {
        _animator.Play(AnimationKeys.WalkAnimationKey);

        Vector3 direction = _targetPoint - _transform.position;

        if (direction.magnitude <= _deadZone)
            SetTargetPoint();

        _mover.ProcessMoveTo(direction.normalized);
        _rotator.ProcessRotateTo(direction);

    }

    private void SetTargetPoint()
    {
        _targetPoint = new Vector3(_defaultPosition.x + Random.Range(_minDistanceToDefaultPoint, _maxDistanceToDefaultPoint),
            _defaultPosition.y, _defaultPosition.z + Random.Range(_minDistanceToDefaultPoint, _maxDistanceToDefaultPoint));
    }
}
