using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private List<Transform> _points = new List<Transform>();
    private Queue<Vector3> _patrolPoints;

    private Transform _transform;
    private Vector3 _targetPoint;

    private IMoveable _mover;
    private IRotator _rotator;

    private Animator _animator;

    private float _deadZone = 0.05f;

    public PatrolState(List<Transform> points, Transform transform, IMoveable movable, IRotator rotator, Animator animator)
    {
        _points = points;
        _transform = transform;

        _mover = movable;
        _rotator = rotator;

        _animator = animator;

        GetPatrolPoints();
    }

    public void ApplyState()
    {
        Vector3 direction = GetDistanceToTarget();

        _animator.Play(AnimationKeys.WalkAnimationKey);

        if (direction.magnitude < _deadZone)
            SwitchTarget();

        _mover.ProcessMoveTo(direction.normalized);
        _rotator.ProcessRotateTo(direction);
    }

    private Vector3 GetDistanceToTarget() => _targetPoint - _transform.position;

    private void GetPatrolPoints()
    {
        _patrolPoints = new Queue<Vector3>();

        foreach (Transform point in _points)
        {
            _patrolPoints.Enqueue(point.position);
        }

        SwitchTarget();
    }

    private void SwitchTarget()
    {
        _targetPoint = _patrolPoints.Dequeue();
        _patrolPoints.Enqueue(_targetPoint);
    }
}
