using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Road : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Prefabs")]
    [SerializeField] private GameObject _linePrefab;

    private UnityEvent _createRoadEvent;

    public UnityEvent CreateRoadEvent
    {
        set => _createRoadEvent = value;
    }
    
    private void CreateLines(int linesAmount)
    {
        for (int i = 0; i < linesAmount; i++)
        {
            CarLine line = Instantiate(_linePrefab, transform).GetComponent<CarLine>();
            if(i % 2 == 1)
            {
                line.Direction = DirectionEnum.Backward;
            }
        }
    }

    private void ApplyMovement()
    {
        Vector2 direction = (transform.up * 0) + (transform.right * -1);
        _rigidbody.velocity = direction * CarGameController.GetInstance().GameSpeed;

        if(_createRoadEvent != null)
        {
            if(transform.localPosition.x <= MechConstants.X_POS_FOR_SPAWN_ROAD)
            {
                _createRoadEvent.Invoke();
                _createRoadEvent = null;
            }
        }
        if(transform.localPosition.x <= MechConstants.GOAL_X_ROAD_POS)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreateLines(Random.Range(MechConstants.MIN_COUNT_LINE, MechConstants.MAX_COUNT_LINE));
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }
}
