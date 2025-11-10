using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public const int RightButton = 1;
    public const string MouseAxeX = "Mouse X";
    public const string MouseAxeY = "Mouse Y";

    [SerializeField] private float _sensitivity;

    private float _aroundXRotation;
    private float _aroundYRotation;

    private float _minAngle = -40f;
    private float _maxAngle = 20f;

    private void Awake()
    {
        _aroundYRotation = transform.eulerAngles.y;
        _aroundXRotation = transform.eulerAngles.x;
    }

    private void Update()
    {
        float mouseDeltaX = Input.GetAxis(MouseAxeX) * _sensitivity;
        float mouseDeltaY = Input.GetAxis(MouseAxeY) * _sensitivity;

        if (Input.GetMouseButton(RightButton))
        {
            _aroundXRotation += mouseDeltaX;
            _aroundYRotation += mouseDeltaY;

            _aroundYRotation = Mathf.Clamp(_aroundYRotation, _minAngle, _maxAngle);

            Quaternion rotation = Quaternion.Euler(-_aroundYRotation, _aroundXRotation, 0);

            transform.rotation = rotation;
        }       
    }
}