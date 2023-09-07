using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _linePrefab;

    
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

    private void Start()
    {
        CreateLines(Random.Range(MechConstants.MIN_COUNT_LINE, MechConstants.MAX_COUNT_LINE));
    }

    private void Update()
    {
        
    }
}
