using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Transform _defaultPoint;

    private List<Transform> _patrolPoints = new();

    private Rigidbody _rigidbody;
    private PlayerCharacter _target;    

    private IdleTypes _idleType;
    private ActionTypes _actionType;

    private IState _actionState;
    private IState _idleState;

    private IState _currentState;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();


    private void Update() =>
        _currentState.ApplyState(_animator, transform);

    public void Initialize(IdleTypes idleType, ActionTypes actionType, PlayerCharacter target, Transform defaultPoint)
    {
        _idleType = idleType;
        _actionType = actionType;
        _target = target;
        _defaultPoint = defaultPoint;

        SetState();

        _currentState = _idleState;
    }

    public void SetState()
    {
        switch (_idleType)
        {
            case IdleTypes.Idle:
                _idleState = new IdleState(); 
                break;
            case IdleTypes.Patrol:
                _idleState = new PatrolState(_patrolPoints, _rigidbody);
                break;
            case IdleTypes.RandomMove:
                _idleState = new RandomMoveState(_rigidbody, _defaultPoint);
                break;
            default:
                Debug.Log("Unknown Idle Type");
                break;
        }

        switch (_actionType)
        {
            case ActionTypes.Chase:
                _actionState = new ChaseState(_target.transform, _rigidbody);
                break;
            case ActionTypes.Escape:
                _actionState = new EscapeState(_target.transform, _rigidbody);
                break;
            case ActionTypes.Death:
                _actionState = new DeathState(gameObject);
                break;
            default:
                Debug.Log("Unknown Action Type");
                break;
        }
    }

    public void SetPatrolPoints(SpawnPoint spawnPoint) =>
        _patrolPoints = spawnPoint.GetPatrolPoints();

    public Vector3 GetDistanceToPlayer() => _target.transform.position - transform.position;

    private void ChangeState(IState newState)
    {
        _rigidbody.velocity = Vector3.zero;
        _animator.Play(AnimationKeys.IdleAnimationKey);

        _currentState = newState;
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
