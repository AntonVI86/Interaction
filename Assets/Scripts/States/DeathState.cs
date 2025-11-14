using UnityEngine;

public class DeathState : IState
{
    private GameObject _gameObject;

    private Animator _animator;

    public DeathState(GameObject gameObject, Animator animator)
    {
        _gameObject = gameObject;
        _animator = animator;
    }

    public void ApplyState()
    {
        _animator.Play(AnimationKeys.DeathAnimationKey);

        if (_gameObject.GetComponent<SphereCollider>() == null)
            return;

        _gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
