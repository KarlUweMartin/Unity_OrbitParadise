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
            else if (touch.phase == TouchPhase.Ended)
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

    private void StartTouch(Vector3 touch) 
    {
        _cubeInstance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _cubeInstance.transform.localScale = Vector3.one * .2f;
        _cubeInstance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f));
        _cubeInstance.GetComponent<Renderer>().material = _sphereMaterial;
        _cubeInstance.GetComponent<Collider>().enabled = false;

        _initialTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f));
    }

    private void UpdateTouch(Vector3 touch)
    {
        _mass = _mass + 1.5f;

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f));
        Vector3 direction = touchPosition - _cubeInstance.transform.position;
        _cubeInstance.transform.rotation = Quaternion.LookRotation(direction);
        _cubeInstance.transform.localScale = Vector3.one * _mass / 1000;
    }

    private void ReleaseTouch(Vector3 touch) 
    {

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, 10f));
        Vector3 direction = (touchPosition - _cubeInstance.transform.position).normalized;
        float velocity = 750 * Vector3.Distance(_cubeInstance.transform.position, touchPosition);

        OnRelease.Invoke(_cubeInstance.transform.position, _cubeInstance.transform.eulerAngles, velocity, _mass);
        _mass = 0;

        _cubeInstance.gameObject.SetActive(false);
        Destroy(_cubeInstance);
    }

    [SerializeField] private Material _sphereMaterial;
    private GameObject _cubeInstance = null;
    private Vector3 _initialTouchPosition;
    private float _mass = 0;

}