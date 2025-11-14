using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _target;

    [SerializeField] private List<SpawnPoint> _points;
    [SerializeField] private List<EnemyStateHandler> _enemyPrefabs;

    private IState _idleState;
    private IState _actionState;

    private void Awake() =>
        Create();
    
    private void Create()
    {
        foreach (SpawnPoint point in _points)
        {
            int enemyIndex = Random.Range(0, _enemyPrefabs.Count);

            EnemyStateHandler enemy = Instantiate(_enemyPrefabs[enemyIndex], point.transform.position, Quaternion.identity);

            SetState(enemy, point);

            enemy.Initialize(_idleState, _actionState);
        }
    }

    private void SetState(EnemyStateHandler enemy, SpawnPoint point)
    {
        switch (point.IdleType)
        {
            case IdleTypes.Idle:
                _idleState = new IdleState(enemy.AnimatorPlayer);
                break;
            case IdleTypes.Patrol:
                _idleState = new PatrolState(point.GetPatrolPoints(), enemy.transform, enemy.Mover, enemy.Rotator, enemy.AnimatorPlayer);
                break;
            case IdleTypes.RandomMove:
                _idleState = new RandomMoveState(enemy.transform, point.transform, enemy.Mover, enemy.Rotator, enemy.AnimatorPlayer);
                break;
            default:
                Debug.Log("Unknown Idle Type");
                break;
        }

        switch (point.ActionType)
        {
            case ActionTypes.Chase:
                _actionState = new ChaseState(_target.transform, enemy.transform, enemy.Mover, enemy.Rotator, enemy.AnimatorPlayer);
                break;
            case ActionTypes.Escape:
                _actionState = new EscapeState(_target.transform, enemy.transform, enemy.Mover, enemy.Rotator, enemy.AnimatorPlayer, enemy.MaxDistanceToEscape);
                break;
            case ActionTypes.Death:
                _actionState = new DeathState(enemy.gameObject, enemy.AnimatorPlayer);
                break;
            default:
                Debug.Log("Unknown Action Type");
                break;
        }
    }
}
