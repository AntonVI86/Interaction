using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SphereCollider _sphereCollider;

    private IMoveable _mover;
    private IRotator _rotator;

    private Rigidbody _rigidbody;

    private float _speed = 3f;

    private IState _actionState;
    private IState _idleState;

    private IState _currentState;

    public Animator AnimatorPlayer => _animator;
    public IMoveable Mover => _mover;
    public IRotator Rotator => _rotator;
    public float MaxDistanceToEscape => _sphereCollider.radius;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();

        _mover = new EnemyMover(_speed, _rigidbody);
        _rotator = new EnemyRotator(transform);
    }

    private void Update() =>
        _currentState.ApplyState();

    public void Initialize(IState idleState, IState actionState)
    {
        _idleState = idleState;
        _actionState = actionState;

        _currentState = _idleState;
    }

    private void ChangeState(IState newState)
    {
        _currentState = newState;

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
