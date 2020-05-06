using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControler : MonoBehaviour
{
    public bool IsLeft;
    public float MaxAngle;
    public float AngleStep;

    private float _currentRotation = 0;
    private KeyCode _controlKey;
    private int posMod = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (IsLeft)
        {
            _controlKey = KeyCode.LeftArrow;
            posMod = 1;
        }
        else _controlKey = KeyCode.RightArrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_controlKey))
        {
            if(_currentRotation < MaxAngle)
            {
                _currentRotation += AngleStep;
                if(_currentRotation > MaxAngle)
                {
                    float dif = _currentRotation - MaxAngle;
                    _currentRotation -= dif;
                    GetComponent<Transform>().Rotate(new Vector3(0, 0, 1), posMod * (AngleStep - dif), Space.World);
                }
                else GetComponent<Transform>().Rotate(new Vector3(0, 0, 1), posMod * AngleStep, Space.World);
            }
        }
        else
        {
            if (_currentRotation > 0f)
            {
                _currentRotation -= AngleStep;
                if (_currentRotation < 0f)
                {
                    float dif = _currentRotation;
                    _currentRotation -= dif;
                    GetComponent<Transform>().Rotate(new Vector3(0, 0, 1), -1 * posMod * (AngleStep + dif), Space.World);
                }
                else GetComponent<Transform>().Rotate(new Vector3(0, 0, 1), posMod * -AngleStep, Space.World);
            }
        }
    }
}
