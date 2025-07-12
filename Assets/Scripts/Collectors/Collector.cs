using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;

    private Coroutine _moveRoutine;
    private SupplyBox _targetSupplyBox;
    private Transform _spawnPoint;
    private DropOff _dropPoint;
    private Vector3 _currentTarget;
    private float _distanceToInteract = 4f;
    private bool _isBusy;

    public SupplyBox TargetSupplyBox => _targetSupplyBox;
    public bool IsBusy => _isBusy;

    private void Awake()
    {
        MarkAsFree();
    }

    private void Update()
    {
        if (_targetSupplyBox != null && _moveRoutine != null && !_targetSupplyBox.Rigidbody.isKinematic)
        {
            TryToPickUp();
        }
    }

    public void MarkAsBusy()
    {
        _isBusy = true;
    }

    public void Init()
    {
        if (_targetSupplyBox != null && !_isBusy)
        {
            MarkAsBusy();
            transform.LookAt(_targetSupplyBox.transform.position);
            MoveTo(_targetSupplyBox.transform.position);
        }
    }

    public void ResetToSpawnPoint()
    {
        MarkAsFree();
        StopCoroutine(_moveRoutine);
        transform.position = _spawnPoint.transform.position;
        transform.LookAt(_spawnPoint.transform.position);
    }

    public void RecieveTargetPosition(SupplyBox target)
    {
        if (_targetSupplyBox != null)
        {
            _targetSupplyBox = null;
        }

        _targetSupplyBox = target;
    }

    public void FreeFromTask()
    {
        _targetSupplyBox = null;
    }

    public void RecieveDropOffPosition(DropOff dropPoint)
    {
        _dropPoint = dropPoint;
    }

    public void RecieveSpawnPoint(Transform spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    private void MarkAsFree()
    {
        _isBusy = false;
    }

    private void MoveTo(Vector3 targetPosition)
    {
        _currentTarget = targetPosition;

        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
        }

        _moveRoutine = StartCoroutine(MoveToTargetRoutine());
    }

    private IEnumerator MoveToTargetRoutine()
    {
        if (_currentTarget != null)
        {
            _isBusy = true;

            while (isActiveAndEnabled)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentTarget,
                    _moveSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }

    private void TryToPickUp()
    {
        if (Vector3.Distance(transform.position, _targetSupplyBox.transform.position) <= _distanceToInteract)
        {
            _targetSupplyBox.OnPickUp(transform);

            if (_targetSupplyBox.Rigidbody.isKinematic)
            {
                transform.LookAt(_dropPoint.transform.position);
                MoveTo(_dropPoint.transform.position);
            }
        }
    }
}