using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;

    private void Start()
    {
        if (PlayerPrefs.HasKey(KeyConstants.SCORE_KEY))
        {
            _score.text = PlayerPrefs.GetInt(KeyConstants.SCORE_KEY).ToString();
        }
        else
        {
            PlayerPrefs.SetInt(KeyConstants.SCORE_KEY, 0);
            _score.text = "0000";
        }
    }
}
