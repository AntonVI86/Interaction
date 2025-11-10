using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private List<Transform> _points = new List<Transform>();
    private Queue<Vector3> _patrolPoints;

    private Vector3 _targetPoint;
    private Rigidbody _rigidbody;

    private float _speed = 2f;
    private float _deadZone = 0.05f;

    private EnemyMover _enemyMover;
    private EnemyRotator _enemyRotator;

    public PatrolState(List<Transform> points, Rigidbody rigidbody)
    {
        _points = points;

        _rigidbody = rigidbody;

        GetPatrolPoints();

        _enemyMover = new EnemyMover(_speed, _rigidbody);
        _enemyRotator = new EnemyRotator();
    }

    public void ApplyState(Animator animator, Transform transform)
    {
        Vector3 direction = GetDistanceToTarget(transform);

        animator.Play(AnimationKeys.WalkAnimationKey);

        if (direction.magnitude < _deadZone)
            SwitchTarget();

        _enemyMover.ProcessMoveTo(direction.normalized);
        _enemyRotator.ProcessRotateTo(direction, transform);
    }

    private Vector3 GetDistanceToTarget(Transform transform) => _targetPoint - transform.position;

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
