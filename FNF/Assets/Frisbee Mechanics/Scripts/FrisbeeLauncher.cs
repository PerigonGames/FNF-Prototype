using System;
using UnityEngine;

public class FrisbeeLauncher : MonoBehaviour
{
    [SerializeField] private Rigidbody _frisbee;
    [SerializeField] private Transform _target;
    [SerializeField, Range(0, 100)] private float _initialSpeed;
    
    [Header("Debug Controls")]
    [SerializeField] private KeyCode _launchButton = KeyCode.Space;
    [SerializeField] private KeyCode _resetButton = KeyCode.R;

    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;


    private Transform _parent;
    private bool _hasLaunched = false;
    private void Start()
    {
        _parent = _frisbee.transform.parent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_launchButton) && !_hasLaunched)
        {
            _hasLaunched = true;
            Launch();
        }

        if (Input.GetKeyDown(_resetButton))
        {
            Reset();
        }
    }

    private void Launch()
    {
        if (_frisbee == null)
        {
            return;
        }

        _frisbee.transform.parent = null;
        _frisbee.useGravity = false;
        //_frisbee.velocity = Vector3.forward * _initialSpeed;
        _frisbee.AddForce(transform.forward*_initialSpeed, _forceMode);
        _frisbee.AddTorque(transform.forward*_initialSpeed, _forceMode);
    }

    private void Reset()
    {
        _frisbee.AddForce(Vector3.zero,ForceMode.Impulse);
        _frisbee.velocity = Vector3.zero;
        _frisbee.GetComponent<Collider>().isTrigger = true;
        _frisbee.transform.position = Vector3.zero;
        _frisbee.transform.rotation = Quaternion.identity;
        _frisbee.transform.parent = _parent;
        _hasLaunched = false;

    }

    #region Kinematic Equations

    private float CalculateDistance()
    {
        return 0.0f;
    }

    private float CalculateTime()
    {
        return 0.0f;
    }



    #endregion
}
