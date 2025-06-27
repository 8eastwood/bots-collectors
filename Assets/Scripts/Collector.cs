using System;
using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private ObjectHandler _objectHandler;
    [SerializeField] private float _moveSpeed = 4;

    private Coroutine _moveRoutine;
    private SupplyBox _targetSupplyBox;
    private DropOff _dropPoint;
    private Vector3 _currentTarget;
    private float _distanceToInteract = 4f;
    
    public SupplyBox TargetSupplyBox => _targetSupplyBox;

    private void Update()
    {
        if (_moveRoutine != null && _targetSupplyBox.PickingObjects.IsPickedUp == false)
        {
            TryToPickUp();
        }
    }

    public void InitFromPool()
    {
        if (_targetSupplyBox != null)
        {
            transform.LookAt(_targetSupplyBox.transform.position);
            MoveTo(_targetSupplyBox.transform.position);
        }
    }

    public void RecieveTargetPosition(SupplyBox target)
    {
        _targetSupplyBox = target;
    }

    public void RecieveDropOffPosition(DropOff dropPoint)
    {
        _dropPoint = dropPoint;
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
            _objectHandler.PickUp(_targetSupplyBox);

            if (_targetSupplyBox.PickingObjects.IsPickedUp)
            {
                transform.LookAt(_dropPoint.transform.position);
                MoveTo(_dropPoint.transform.position);
            }
        }
    }
}