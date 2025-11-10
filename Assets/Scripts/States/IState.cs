using UnityEngine;

public interface IState
{
    void ApplyState(Animator animator, Transform transform);
}
