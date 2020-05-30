using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Rollover : MonoBehaviour
{
    /// <summary>
    /// Score gained for triggering rollover
    /// </summary>
    public int Score;
    /// <summary>
    /// Minimum time (in seconds) between triggers (to prevent bugs where 
    /// you might be able to score multiple time if collisions are registered in quick succession,
    /// or if it's possible to roll back over the rollover and that shouldn't score)
    /// </summary>
    public float ResetTime;

    private Timer _resetTimer;
    private bool _canTrigger = true;

    private void Start()
    {
        if (Score > 0) TableManager.Manager.RegisterScores(this, Score);
        _resetTimer = new Timer(ResetTime * 1000)
        {
            AutoReset = false,
            Enabled = false,
        };
        _resetTimer.Elapsed += TriggerEnable;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!_canTrigger) return;

        if (Score > 0) TableManager.Manager.Score(this);
        _canTrigger = false;
        _resetTimer.Start();
    }

    private void TriggerEnable(object source = null, ElapsedEventArgs e = null)
    {
        _canTrigger = true;
    }
}
