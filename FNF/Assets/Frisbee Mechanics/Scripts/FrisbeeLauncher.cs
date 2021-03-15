using UnityEngine;
using UnityEngine.Animations;

public class FrisbeeLauncher : MonoBehaviour
{
    private const float AIMFOV = 40f;
    private const float REGULARFOV = 60f;
    
    [SerializeField] private Frisbee _frisbee;
    [SerializeField] private Transform _curvePoint;
    [SerializeField, Range(0, 100)] private float _initialSpeed;
    
    [Space]
    [Header("Debug Controls")]
    [SerializeField] private KeyCode _launchButton = KeyCode.Space;
    [SerializeField] private KeyCode _resetButton = KeyCode.R;
    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
    
    private Transform _parent;
    private Rigidbody _frisbeeRb;
    private Camera _camera;
    private bool _isAiming = false;
    private bool _hasLaunched = false;
    
    private void Start()
    {
        _frisbeeRb = _frisbee.GetComponent<Rigidbody>();
        _parent = _frisbee.transform.parent;
        _camera = Camera.main;
    }

    private void Update()
    {
        //If aiming rotate the player towards the camera foward, if not reset the camera rotation on the x axis
        if (_isAiming)
        {
            RotateToCamera(transform);
        }
        else
        {
            Vector3 eulerTransform = transform.eulerAngles;
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(eulerTransform.x,0,0.2f), eulerTransform.y, eulerTransform.z);
        }

        if (Input.GetMouseButtonDown(1) && !_hasLaunched)
        {
            AimConstraint(tru)
        }


        // if (Input.GetKeyDown(_launchButton) && !_hasLaunched)
        // {
        //     _hasLaunched = true;
        //     Launch();
        // }
        //
        // if (Input.GetKeyDown(_resetButton))
        // {
        //     Reset();
        // }
    }

    private void Launch()
    {
        if (_frisbee == null)
        {
            return;
        }
        
        _frisbeeRb.transform.parent = null;
        _frisbeeRb.useGravity = false;
        Vector3 forward = transform.forward*_initialSpeed;
        _frisbeeRb.AddForce(forward, _forceMode);
        _frisbeeRb.AddTorque(forward, _forceMode);
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

    private void Aim(bool state)
    {
        if (state)
        {
            _camera.fieldOfView = AIMFOV;
            // Show trail effects here
        }
        else
        {
            _camera.fieldOfView = REGULARFOV;
            // Turn off trail effects
        }
    }

    private Vector3 GetCurvePoint(float delta, Vector3 startingPos, Vector3 turningPos, Vector3 endingPos)
    {
        float inverseDelta = 1 - delta;
        float deltaSquared = delta*delta;
        float inverseDeltaSquared = inverseDelta * inverseDelta;
        return (inverseDeltaSquared * startingPos) + (2 * inverseDelta * delta * turningPos) + (deltaSquared * endingPos);
    }

    private void RotateToCamera(Transform t)
    {
        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_camera.transform.forward),10);
    }
}