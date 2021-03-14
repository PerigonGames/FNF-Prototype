using UnityEngine;

public class FrisbeeLauncher : MonoBehaviour
{
    [SerializeField] private Frisbee _frisbee;
    [SerializeField] private Transform _curvePoint;
    [SerializeField, Range(0, 100)] private float _initialSpeed;
    
    [Header("Debug Controls")]
    [SerializeField] private KeyCode _launchButton = KeyCode.Space;
    [SerializeField] private KeyCode _resetButton = KeyCode.R;

    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;


    private Transform _parent;
    private Rigidbody _frisbeeRb;
    private bool _hasLaunched = false;
    
    private void Start()
    {
        _frisbeeRb = _frisbee.GetComponent<Rigidbody>();
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
        
        _frisbeeRb.transform.parent = null;
        _frisbeeRb.useGravity = false;
        //_frisbee.velocity = Vector3.forward * _initialSpeed;
        _frisbeeRb.AddForce(transform.forward*_initialSpeed, _forceMode);
        _frisbeeRb.AddTorque(transform.forward*_initialSpeed, _forceMode);
    }

    private void Reset()
    {
        _frisbeeRb.AddForce(Vector3.zero,ForceMode.Impulse);
        _frisbeeRb.AddTorque(Vector3.zero, ForceMode.Impulse);
        _frisbeeRb.velocity = Vector3.zero;
        _frisbeeRb.GetComponent<Collider>().isTrigger = true;
        _frisbeeRb.transform.position = Vector3.zero;
        _frisbeeRb.transform.rotation = Quaternion.identity;
        _frisbeeRb.transform.parent = _parent;
        _hasLaunched = false;
    }

    private Vector3 GetCurvePoint(float delta, Vector3 startingPos, Vector3 turningPos, Vector3 endingPos)
    {
        float inverseDelta = 1 - delta;
        float deltaSquared = delta*delta;
        float inverseDeltaSquared = inverseDelta * inverseDelta;
        return (inverseDeltaSquared * startingPos) + (2 * inverseDelta * delta * turningPos) + (deltaSquared * endingPos);
    }
}
