using UnityEngine;

public class DeathState : IState
{
    private GameObject _gameObject;

    public DeathState(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void ApplyState(Animator animator, Transform transform)
    {
        animator.Play(AnimationKeys.DeathAnimationKey);

        if (_gameObject.GetComponent<SphereCollider>() == null)
            return;

        _gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
