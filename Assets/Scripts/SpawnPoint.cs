using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> _patrolPoints;

    [SerializeField] private IdleTypes _idleType;
    [SerializeField] private ActionTypes _actionType;

    public IdleTypes IdleType => _idleType;
    public ActionTypes ActionType => _actionType;

    public List<Transform> GetPatrolPoints()
    {
        return _patrolPoints;
    }
}
