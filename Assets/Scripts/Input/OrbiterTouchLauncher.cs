using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbiterTouchLauncher : MonoBehaviour
{
    private void Start()
    {
        _infoCloseSequence = DOTween.Sequence();
        _infoCloseSequence.Pause();

        _infoCloseSequence.Append(_orbiterInfo.transform.DOScale(1.1f, 0.15f));
        _infoCloseSequence.Join(_orbiterInfo.DOColor(Color.white, 0.15f));
        _infoCloseSequence.Append(_orbiterInfo.DOFade(0, 2f));

        _infoCloseSequence.OnComplete(() => 
        {
            _orbiterInfo.enabled = false;
        });
        _infoCloseSequence.SetAutoKill(false);
    }

    void Update()
    {
        if ((Input.touchCount > 1 || Input.GetKeyDown(KeyCode.Mouse1)) && _isTouching)
        {
            FinishTouch();
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
        if (_isTouching && _mass <= 2000)
        {
            _mass += 6;
        }
    }

    private void StartTouch(Vector3 touch)
    {
        if (Models.TouchingUi || EventSystem.current.IsPointerOverGameObject()) return;

        if (_orbiter == null)
        {
            var camDist = Vector3.Distance(Camera.main.transform.position, _gravityObject.transform.position);
            _orbiter = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _orbiter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, camDist)); ;
            _orbiter.transform.localScale = Vector3.one * .12f;
            _orbiter.GetComponent<Renderer>().material = _sphereMaterial;

            _initialTouch = touch;
            _isTouching = true;
            _indicatorLine.enabled = true;

            _infoCloseSequence.Complete();
            _orbiterInfo.DOFade(.5f, 0);
            _orbiterInfo.transform.localScale = Vector3.one;
            _orbiterInfo.enabled = true;
        }
    }

    private void UpdateTouch(Vector3 touch)
    {
        if (_orbiter != null)
        {
            _isTouching = true;
            var camDist = Vector3.Distance(Camera.main.transform.position, _gravityObject.transform.position);
            var touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.x, touch.y, camDist));
            var direction = touchPosition - _orbiter.transform.position;
            var dist = Vector3.Distance(_orbiter.transform.position, touchPosition);

            _velocity = 250 + _mass * dist * (dist / 2);

            _orbiter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(_initialTouch.x, _initialTouch.y, camDist)); ;
            _orbiter.transform.rotation = Quaternion.LookRotation(direction);
            _orbiter.transform.localScale = Vector3.one * Utils.RemapRange(_mass, 0, 2000, .12f, .5f);

            _indicatorLine.SetPosition(0, _orbiter.transform.position);
            _indicatorLine.SetPosition(1, touchPosition);
            _orbiterInfo.text =
                $"<size=15>Mass</size>\n" +
                $"<b>{_mass}</b>\n" +
                "\n" +
                $"<size=15>Velocity</size>\n" +
                $"<b>{(int)(dist * 1000)}</b>";
        }
    }

    private void ReleaseTouch(Vector3 touch)
    {
        if (_orbiter != null)
        {
            LaunchOribiter(_orbiter.transform.position, _orbiter.transform.eulerAngles, _velocity, _mass);
        }

        FinishTouch();
    }

    private void FinishTouch()
    {
        if (Models.TouchingUi || EventSystem.current.IsPointerOverGameObject()) return;

        _indicatorLine.SetPosition(0, Vector3.zero);
        _indicatorLine.SetPosition(1, Vector3.zero);
        _mass = 25; 
        _velocity = 0;
        _isTouching = false;
        Destroy(_orbiter);

        _infoCloseSequence.Restart();
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
    private GameObject _orbiter = null;
    private int _mass = 25;
    private float _velocity = 0;
    private bool _isTouching = false;

    [SerializeField] private TextMeshProUGUI _orbiterInfo;

    private Sequence _infoCloseSequence;

}