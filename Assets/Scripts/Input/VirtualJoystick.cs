using UnityEngine;
using UnityEngine.Events;

public class VirtualJoystick : MonoBehaviour
{
    public UnityEvent<Vector3, Vector3, float, float> OnRelease = new();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartTouch(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                UpdateTouch(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                ReleaseTouch(touch.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartTouch(Input.mousePosition);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            UpdateTouch(Input.mousePosition);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseTouch(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (_updateMass) 
        {
            _mass += 4f;
        }
    }

    private void StartTouch(Vector3 touch) 
    {
        if (_cubeInstance == null) 
        {
            _cubeInstance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _cubeInstance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f)); ;
            _cubeInstance.transform.localScale = Vector3.one * .2f;
            _cubeInstance.GetComponent<Renderer>().material = _sphereMaterial;

            _initialTouch = touch;
            _updateMass = true;
        }
    }

    private void UpdateTouch(Vector3 touch)
    {
        if (_cubeInstance != null)
        {
            var touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f));
            var direction = touchPosition - _cubeInstance.transform.position;

            _cubeInstance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(_initialTouch.x, _initialTouch.y, 10f)); ;
            _cubeInstance.transform.rotation = Quaternion.LookRotation(direction);
            _cubeInstance.transform.localScale = Vector3.one * _mass / 1000;
        }
    }

    private void ReleaseTouch(Vector3 touch) 
    {
        if (_cubeInstance != null)
        {
            var touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f));
            var velocity = 600 * Vector3.Distance(_cubeInstance.transform.position, touchPosition);
            OnRelease.Invoke(_cubeInstance.transform.position, _cubeInstance.transform.eulerAngles, velocity, _mass);

            _updateMass = false;
            _mass = 25;
        }

        Destroy(_cubeInstance);
        _cubeInstance = null;
    }



    [SerializeField] private Material _sphereMaterial;
    private Vector3 _initialTouch;
    private GameObject _cubeInstance = null;
    private float _mass = 25;
    private bool _updateMass = false;

}