using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public const string HorizontalAxe = "Horizontal";
    public const string VerticalAxe = "Vertical";

    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _animator;

    private float _speed = 6;

    private Rigidbody _rigidbody;

    private CharacterMover _mover;
    private CharacterRotator _rotator;

    private float _inputZ;
    private float _deadZone = 0.05f;

    private Vector3 _inputDirection;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _mover = new CharacterMover(_speed, _camera, _rigidbody);
        _rotator = new CharacterRotator(_camera);
    }

    private void Update()
    {
        _rotator.ProcessRotate(transform);

        _inputZ = Input.GetAxisRaw(VerticalAxe);
        _inputDirection = new Vector3(0, 0, _inputZ);

        if (_inputDirection.magnitude <= _deadZone)
        {
            _animator.Play(AnimationKeys.IdleAnimationKey);
            return;
        }

        _animator.Play(AnimationKeys.WalkAnimationKey);
    }

    private void FixedUpdate()
    {
        _mover.ProcessMoveTo(_inputDirection.normalized);
    }
}
