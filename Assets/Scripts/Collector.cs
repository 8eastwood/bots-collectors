using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _speed = 4;

    private Rigidbody _targetSupplyBox;

    private void Start()
    {
        if (_targetSupplyBox != null)
        {
            StartCoroutine(Move());
        }
    }

    public void RecieveTargetPosition(Rigidbody target)
    {
        _targetSupplyBox = target;
    }

    private IEnumerator Move()
    {
        if (_targetSupplyBox != null)
        {
            Debug.Log(_targetSupplyBox);
            while (isActiveAndEnabled)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetSupplyBox.transform.position,
                    _speed * Time.deltaTime);
                
                Debug.Log("im riding to the" + _targetSupplyBox.transform.position);

                yield return null;
            }
        }
    }
}