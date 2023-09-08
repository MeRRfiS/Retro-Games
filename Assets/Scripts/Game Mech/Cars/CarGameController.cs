using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarGameController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _road;

    [Header("UI window")]
    [SerializeField] private Transform _gameUI;

    private bool _gameIsOver = false;
    private float _gameSpeed;
    private float _carSpeed;
    private float _score = 0;

    private static CarGameController instance;

    public static CarGameController GetInstance() => instance;

    public bool GameIsOver
    {
        get => _gameIsOver;
    }

    public float GameSpeed
    {
        get => _gameSpeed;
    }

    public float CarSpeed
    {
        get => _carSpeed;
    }

    private void CreateRoad()
    {
        Road road = Instantiate(_road, _gameUI).GetComponent<Road>();
        UnityEvent creareRoadEvent = new UnityEvent();
        creareRoadEvent.AddListener(CreateRoad);
        road.CreateRoadEvent = creareRoadEvent;
    }

    private void CreateStartedRoad()
    {
        Road road = Instantiate(_road, _gameUI).GetComponent<Road>();
        road.transform.localPosition = new Vector2(MechConstants.X_POS_FOR_SPAWN_ROAD, 0);
        UnityEvent creareRoadEvent = new UnityEvent();
        creareRoadEvent.AddListener(CreateRoad);
        road.CreateRoadEvent = creareRoadEvent;
    }

    private void ApplyIncreasingSpeed()
    {
        if (_gameIsOver) return;
        if (_gameSpeed >= GameConstants.MAX_GAME_SPEED) return;

        _gameSpeed += Time.deltaTime * GameConstants.GAME_STAT_MULTIPLIER;
    }

    private void ApplyIncreasingCarSpeed()
    {
        if (_gameIsOver) return;
        if (_carSpeed >= GameConstants.MAX_CAR_SPEED) return;

        _carSpeed += Time.deltaTime * GameConstants.GAME_STAT_MULTIPLIER;
    }

    private void ApplyIncreasingScore()
    {
        if (_gameIsOver) return;

        _score += Time.deltaTime;
        UIController.GetInstance().UpdateScoreText(_score);
    }

    private void CheckingGameOver()
    {
        if (PlayerController.GetInstance().PlayerHearts != 0) return;

        if (PlayerPrefs.HasKey(KeyConstants.SCORE_KEY))
        {
            if(PlayerPrefs.GetInt(KeyConstants.SCORE_KEY) < _score)
            {
                PlayerPrefs.SetInt(KeyConstants.SCORE_KEY, Mathf.FloorToInt(_score));
            }
        }
        else
        {
            PlayerPrefs.SetInt(KeyConstants.SCORE_KEY, Mathf.FloorToInt(_score));
        }

        UIController.GetInstance().OpenGameOverWindow();
        _gameIsOver = true;
    }

    private void Awake()
    {
        instance = this;
        _gameSpeed = GameConstants.START_GAME_SPEED;
        _carSpeed = GameConstants.START_CAR_SPEED;
    }

    private void Start()
    {
        CreateStartedRoad();
    }

    private void Update()
    {
        ApplyIncreasingSpeed();
        ApplyIncreasingCarSpeed();
        ApplyIncreasingScore();
        CheckingGameOver();
    }
}
