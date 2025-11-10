using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _target;

    [SerializeField] private List<SpawnPoint> _points;
    [SerializeField] private List<EnemyStateHandler> _enemyPrefabs;

    private void Awake() =>
        Create();
    
    private void Create()
    {
        foreach (SpawnPoint point in _points)
        {
            int enemyIndex = Random.Range(0, _enemyPrefabs.Count);
            EnemyStateHandler enemy = Instantiate(_enemyPrefabs[enemyIndex], point.transform.position, Quaternion.identity);
            enemy.SetPatrolPoints(point);
            enemy.Initialize(point.IdleType, point.ActionType, _target, point.transform);
        }
    }
}
