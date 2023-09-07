using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarLine : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _carPrefab;

    private float _spawnNewCarPos;
    private DirectionEnum _direction = DirectionEnum.Forward;

    public DirectionEnum Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private void CreateStartCars(int carAmount = 1)
    {
        UnityEvent carSpawn = new UnityEvent();
        carSpawn.AddListener(CreateNewCar);
        float reduceNumber = MechConstants.LINE_Y_SCALE / (carAmount + 1);
        _spawnNewCarPos = (MechConstants.LINE_Y_SCALE / 2 - reduceNumber) * (int)_direction;
        float spawnPos = _carPrefab.transform.localPosition.y - reduceNumber;
        for (int i = 0; i < carAmount; i++)
        {
            Car car;
            car = Instantiate(_carPrefab, transform).GetComponent<Car>();
            car.transform.localPosition = new Vector2(0, spawnPos);
            car.GoalPosition = new Vector2(0, MechConstants.GOAL_Y_POSITION * (int)_direction);
            car.SpawnPos = _spawnNewCarPos;
            car.CarSpawn = carSpawn;
            spawnPos -= reduceNumber;
        }
    }

    private void Start()
    {
        CreateStartCars(Random.Range(MechConstants.MIN_START_CAR, MechConstants.MAX_START_CAR));
    }

    public void CreateNewCar()
    {
        UnityEvent carSpawn = new UnityEvent();
        carSpawn.AddListener(CreateNewCar);
        Car car;
        car = Instantiate(_carPrefab, transform).GetComponent<Car>();
        car.transform.localPosition = new Vector2(0, car.transform.localPosition.y * (int)_direction);
        car.GoalPosition = new Vector2(0, MechConstants.GOAL_Y_POSITION * (int)_direction);
        car.SpawnPos = _spawnNewCarPos;
        car.CarSpawn = carSpawn;
    }
}
