using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDispenser : MonoBehaviour, INotify
{
    public GameObject BallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        TableManager.Manager.Subscribe(this);
        Dispense();
    }

    public void Notify(EventNotify notify)
    {
        switch (notify)
        {
            case EventNotify.NewBall:
                Dispense();
                break;
        }
    }

    private void Dispense()
    {
        Instantiate(BallPrefab);
    }
}
