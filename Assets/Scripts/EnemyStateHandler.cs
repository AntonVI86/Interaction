using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private List<Transform> _patrolPoints = new();
    private Transform _defaultPoint;

    private IMoveable _mover;
    private IRotator _rotator;

    private Rigidbody _rigidbody;
    private PlayerCharacter _target;

    private float _speed = 3f;

    private IState _actionState;
    private IState _idleState;

    private IState _currentState;

    public Animator AnimatorPlayer => _animator;
    public IMoveable Mover => _mover;
    public IRotator Rotator => _rotator;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();

        _mover = new EnemyMover(_speed, _rigidbody);
        _rotator = new EnemyRotator(transform);
    }

    private void Update() =>
        _currentState.ApplyState();

    public void Initialize(IState idleState, IState actionState, PlayerCharacter target, Transform defaultPoint)
    {
        _target = target;
        _defaultPoint = defaultPoint;

        _idleState = idleState;
        _actionState = actionState;

        _currentState = _idleState;
    }

    public void SetPatrolPoints(SpawnPoint spawnPoint) =>
        _patrolPoints = spawnPoint.GetPatrolPoints();

    private void ChangeState(IState newState)
    {
        _currentState = newState;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _animator.Play(AnimationKeys.IdleAnimationKey);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter player))
            ChangeState(_actionState);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacter player))
            ChangeState(_idleState);
    }
}
