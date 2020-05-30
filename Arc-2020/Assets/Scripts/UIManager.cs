using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreField;
    public Text BallsField;

    private TableManager _data;

    void Start()
    {
        _data = TableManager.Manager;
        ScoreField.text = "Score: " + _data.GameScore.ToString();
        BallsField.text = "Balls remaining: " + _data.BallsRemaining.ToString();
    }

    void Update()
    {
        ScoreField.text = "Score: " + _data.GameScore.ToString();
        BallsField.text = "Balls remaining: " + _data.BallsRemaining.ToString();
    }
}
