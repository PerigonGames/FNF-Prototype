using UnityEngine;

public class FrisbeeLauncher : MonoBehaviour
{
    [SerializeField] private Rigidbody _frisbee;
    [SerializeField] private Transform _target;
    [SerializeField, Range(0, 100)] private float _initialSpeed;

    [SerializeField] private KeyCode _launchButton = KeyCode.Space;

    private void Update()
    {
        if (Input.GetKeyDown(_launchButton))
        {
            Launch();
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
        _frisbee.velocity = Vector3.forward * _initialSpeed;
    }
}
