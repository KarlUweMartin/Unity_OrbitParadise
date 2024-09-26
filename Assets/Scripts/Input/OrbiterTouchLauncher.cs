using UnityEngine;
using UnityEngine.EventSystems;

public class OrbiterTouchLauncher : MonoBehaviour
{
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() || Models.TouchingUi)
        {
            if (_isTouching)
            {
                FinishTouch();
            }
            return;
        }

        if (Input.touchCount == 1)
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
        if (_isTouching && _mass < 700)
        {
            _mass += 4;
        }
    }

    private void StartTouch(Vector3 touch)
    {
        if (_cubeInstance == null)
        {
            var camDist = Vector3.Distance(Camera.main.transform.position, _gravityObject.transform.position);
            _cubeInstance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _cubeInstance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, camDist)); ;
            _cubeInstance.transform.localScale = Vector3.one * .12f;
            _cubeInstance.GetComponent<Renderer>().material = _sphereMaterial;

            _initialTouch = touch;
            _isTouching = true;
            _indicatorLine.enabled = true;
        }
    }

    private void UpdateTouch(Vector3 touch)
    {
        if (_cubeInstance != null)
        {
            _isTouching = true;
            var camDist = Vector3.Distance(Camera.main.transform.position, _gravityObject.transform.position);
            var touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, camDist));
            var direction = touchPosition - _cubeInstance.transform.position;
            _velocity = 250 + Models.OrbitCameraDistance * Vector3.Distance(_cubeInstance.transform.position, touchPosition);

            _cubeInstance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(_initialTouch.x, _initialTouch.y, camDist)); ;
            _cubeInstance.transform.rotation = Quaternion.LookRotation(direction);
            _cubeInstance.transform.localScale = Vector3.one * Utils.RemapRange(_mass, 0, 2000, .12f, .5f);

            _indicatorLine.SetPosition(0, _cubeInstance.transform.position);
            _indicatorLine.SetPosition(1, touchPosition);
        }
    }

    private void ReleaseTouch(Vector3 touch)
    {
        if (_cubeInstance != null)
        {
            LaunchOribiter(_cubeInstance.transform.position, _cubeInstance.transform.eulerAngles, _velocity, _mass);
        }

        FinishTouch();
    }

    private void FinishTouch()
    {
        _indicatorLine.SetPosition(0, Vector3.zero);
        _indicatorLine.SetPosition(1, Vector3.zero);
        _mass = 25; 
        _velocity = 0;
        _isTouching = false;
        Destroy(_cubeInstance);
    }

    public void LaunchOribiter(Vector3 position, Vector3 direction, float velocity, int mass)
    {
        var orbiter = Instantiate(_orbiterPrefab).GetComponent<Orbiter>();
        orbiter.StartPosition = position;
        orbiter.StartRotation = direction;
        orbiter.Mass = mass;
        orbiter.Velocity = velocity;
        orbiter.GravitiObject = _gravityObject;
    }


    [SerializeField] private GameObject _orbiterPrefab;
    [SerializeField] private GravityObject _gravityObject;
    [SerializeField] private Material _sphereMaterial;
    [SerializeField] private LineRenderer _indicatorLine;

    private Vector3 _initialTouch;
    private GameObject _cubeInstance = null;
    private int _mass = 25;
    private float _velocity = 0;
    private bool _isTouching = false;

}