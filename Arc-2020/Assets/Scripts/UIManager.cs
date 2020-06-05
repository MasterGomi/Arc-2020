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
    }

    void Update()
    {
        ScoreField.text = "Score: \n" + _data.GameScore.ToString();
        BallsField.text = "Balls: \n" + _data.BallsRemaining.ToString();
    }
}
