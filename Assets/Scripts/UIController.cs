using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _finalScore;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private List<GameObject> _hearts;

    [Header("Windows")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _game;


    private static UIController instance;

    public static UIController GetInstance() => instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateScoreText(float score)
    {
        int accurateScore = (int)Mathf.Floor(score);
        _score.text = accurateScore.ToString();
    }

    public void RemoveHeart()
    {
        GameObject heart = _hearts.Last();
        _hearts.Remove(heart);
        Destroy(heart);
    }

    public void OpenGameOverWindow()
    {
        string scoreText = _score.text;
        if (PlayerPrefs.GetInt(KeyConstants.SCORE_KEY) <= int.Parse(_score.text))
        {
            scoreText += " (New Record)";
        }
        _finalScore.text = String.Format(_finalScore.text, scoreText);
        _score.text = "0000";
        _gameOver.SetActive(true);
        _game.SetActive(false);
    }
}
