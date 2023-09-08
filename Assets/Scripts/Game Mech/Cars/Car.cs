using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    private float _spawnPos;
    private Vector2 _goalPosition;
    private UnityEvent _carSpawnEvent;
    private CarLine _line;

    public float SpawnPos
    {
        set => _spawnPos = value;
    }

    public Vector2 GoalPosition
    {
        set => _goalPosition = value;
    }

    public UnityEvent CarSpawnEvent
    {
        set => _carSpawnEvent = value;
    }

    private void ApplyMovement()
    {
        Vector2 direction = (transform.up * 0) + (transform.right * -1);
        transform.localPosition = Vector2.MoveTowards(transform.localPosition,
                                                      _goalPosition,
                                                      Time.deltaTime * CarGameController.GetInstance().CarSpeed);
        if (_carSpawnEvent != null)
        {
            switch (_line.Direction)
            {
                case DirectionEnum.Forward:
                    if (transform.localPosition.y >= -(_spawnPos)) return;
                    break;
                case DirectionEnum.Backward:
                    if (transform.localPosition.y <= -(_spawnPos)) return;
                    break;
            }
            _carSpawnEvent.Invoke();
            _carSpawnEvent = null;
        }
        if (transform.localPosition.y == _goalPosition.y)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _line = transform.parent.GetComponent<CarLine>();
    }

    private void Update()
    {
        ApplyMovement();
    }
}
