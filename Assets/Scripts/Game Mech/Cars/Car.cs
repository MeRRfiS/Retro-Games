using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    private float _spawnPos;
    private Vector2 _goalPosition;
    private UnityEvent _carSpawn;
    private CarLine _line;

    public float SpawnPos
    {
        set => _spawnPos = value;
    }

    public Vector2 GoalPosition
    {
        set => _goalPosition = value;
    }

    public UnityEvent CarSpawn
    {
        set => _carSpawn = value;
    }

    private void ApplyMovement()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition,
                                                      _goalPosition,
                                                      Time.deltaTime * MechConstants.CAR_SPEED);
        if (_carSpawn != null)
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
            _carSpawn.Invoke();
            _carSpawn = null;
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
