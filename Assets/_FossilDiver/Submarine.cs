using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Submarine : MonoBehaviour
{
    //Controls Movement Ship:
    // Move - WASD
    // Ascend - Space
    // Descend - Shift
    // Controls Movement Arm:
    // Move - WASD
    // Engage/Grab - Space
    // Stabilize - Shift
    //
    // Switch State - E/F

    [SerializeField] private GameObject _thirdPersonCamera, _firstPersonCamera;

    [SerializeField] private KeyCode _forward, _backward, _left, _right, _descend, _ascend, _switchCamera;
    [SerializeField] private float _moveSpeed, _angularSpeed = 1f;
    [SerializeField] private float _ascendDescendSpeed = 1f;
    [SerializeField] private float _maxVelocity, _maxAngularVelocity, _maxHeight;

    [SerializeField] private Transform _leftForcePos, _rightForcePos;

    [SerializeField] private Image _curtain;

    private bool _isInputActive = true;
    private Rigidbody _rigidbody;

    private UnityEvent _forwardEvent = new UnityEvent();
    private UnityEvent _backwardEvent = new UnityEvent();
    private UnityEvent _leftEvent = new UnityEvent();
    private UnityEvent _rightEvent = new UnityEvent();
    private UnityEvent _ascendEvent = new UnityEvent();
    private UnityEvent _descendEvent = new UnityEvent();

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        SwitchControlPattern(SubmarineState.Movement);
    }

    private void Update()
    {
        if (!_isInputActive) return;

        if (Input.GetKey(_forward) && !MaxVelocityExceeded())
        {
            _rigidbody.velocity = transform.forward * _moveSpeed;
        }
        if (Input.GetKey(_backward) && !MaxVelocityExceeded())
        {
            _rigidbody.velocity = -transform.forward * _moveSpeed/3;
        }
        if (Input.GetKey(_left) && !MaxAngularVelocityExceeded())
        {
            _rigidbody.AddForceAtPosition(transform.right * _angularSpeed, _leftForcePos.position, ForceMode.Force);
        }
        if (Input.GetKey(_right) && !MaxAngularVelocityExceeded())
        {
            _rigidbody.AddForceAtPosition(-transform.right * _angularSpeed, _rightForcePos.position, ForceMode.Force);
        }
        if (Input.GetKey(_descend))
        {
            _rigidbody.AddRelativeForce(-transform.up * _ascendDescendSpeed, ForceMode.Force);
        }
        if (Input.GetKey(_ascend))
        {
            if (transform.position.y <= _maxHeight)
            {
                _rigidbody.AddRelativeForce(transform.up * _ascendDescendSpeed, ForceMode.Force);
            }
        }

        if(Input.GetKey(_switchCamera))
        {
            StartCoroutine(SwitchCamera(0.5f));
        }

    }

    private bool MaxVelocityExceeded()
    {
        return _rigidbody.velocity.sqrMagnitude > _maxVelocity;
    }

    private bool MaxAngularVelocityExceeded()
    {
        return _rigidbody.angularVelocity.sqrMagnitude > _maxAngularVelocity;
    }

    private IEnumerator SwitchCamera(float timePerFade)
    {
        _isInputActive = false;
        var temp = 0f;
        var color = _curtain.color;
        while(temp/timePerFade < timePerFade)
        {
            temp += Time.deltaTime;
            color.a = temp/timePerFade;
            _curtain.color = color;
             yield return null;
        }
        color.a = 1f;
        _curtain.color = color;

        _firstPersonCamera.SetActive(!_firstPersonCamera.activeSelf);
        _thirdPersonCamera.SetActive(!_thirdPersonCamera.activeSelf);

        yield return new WaitForSeconds(0.2f);

        while (temp / timePerFade > 0)
        {
            temp -= Time.deltaTime;
            color.a = temp / timePerFade;
            _curtain.color = color;
            yield return null;
        }
        color.a = 0f;
        _curtain.color = color;

        _isInputActive = true;
    }

    private void SwitchControlPattern(SubmarineState submarineState)
    {
        if (submarineState == SubmarineState.Movement)
        {

        }
        else if (submarineState == SubmarineState.Arm)
        {

        }
    }


}

public enum SubmarineState
{
    Movement,
    Arm
}
