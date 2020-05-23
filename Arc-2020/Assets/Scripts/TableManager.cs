using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    /// <summary>
    /// A global reference to the specific TableManager for the game.
    /// To access TableManager methods and members from any script, use TableManager.Manager
    /// </summary>
    [HideInInspector]
    public static TableManager Manager = null;

    /// <summary>
    /// Holds all acoring values, independant for all elements on the board
    /// </summary>
    [HideInInspector]
    public Dictionary<Object, int[]> ScoringTable = new Dictionary<Object, int[]>();
    /// <summary>
    /// The current game's score (read only).
    /// To score, call TableManager.Manager.Score()
    /// </summary>
    [HideInInspector]
    public int GameScore { get; private set; } = 0;
    /// <summary>
    /// The amount of balls to start the game with
    /// </summary>
    public int StartingBalls = 3;
    /// <summary>
    /// Amount of balls remaining (read only)
    /// </summary>
    [HideInInspector]
    public int BallsRemaining { get; private set; }

    private List<INotify> _subscribers = new List<INotify>();

    // Awake is called before all start methods, meaning the table manager
    // will exist for any objects to access in their start methods
    void Awake()
    {
        // Ensure there is only one TableManager for the game
        if (Manager != null) { Destroy(gameObject); return; }
        Manager = this;
        BallsRemaining = StartingBalls - 1; // -1 becuase the first ball is automatically dispensed
    }


    /// <summary>
    /// Adds scoring for an onject to the scoring table
    /// </summary>
    /// <param name="toRegister">The object that can score. Use [this] keyword from calling object</param>
    /// <param name="scoreTypes">All possible scores. Either as int, int, int... or int[]. Will use the same order for scoreTypes in Score()</param>
    public void RegisterScores(Object toRegister, params int[] scoreTypes)
    {
        ScoringTable.Add(toRegister, scoreTypes);
    }

    /// <summary>
    /// Adds relevant score to the game score
    /// </summary>
    /// <param name="scoreFrom">The object that is scoring. Use [this] keywoard</param>
    /// <param name="scoreType">The index to the score. Same order as when score was registed (starting at 0)</param>
    public void Score(Object scoreFrom, int scoreType = 0)
    {
        GameScore += ScoringTable[scoreFrom][scoreType];
    }

    public void DrainBall()
    {
        BallsRemaining--;
        EndOfBall();
        if (BallsRemaining <= 0) GameOver();
        else NotifyAll(EventNotify.NewBall);
    }

    private void EndOfBall()
    {
        NotifyAll(EventNotify.EndOfBall);

        // Calculate end-of-ball bonus
        // Play relevant sounds
        // Activate relevant lights
        // Show relevant information
        // Start new ball
        // Include stall for appropriate amount of time (especially to let sounds play out)

    }

    private void GameOver()
    {
        // Note: EndOfBall() has already been called

        // Play relevant sounds
        // Activate relevant lights
        // Show relevant information
        // Do highscore stuff
        // End game (return to menu)

        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Subscribe to event notifications (on a per object basis)
    /// </summary>
    /// <param name="subscriber">The object to be notified. Must implement INotify</param>
    public void Subscribe(INotify subscriber)
    {
        if(!_subscribers.Contains(subscriber))
            _subscribers.Add(subscriber);
    }

    /// <summary>
    /// Unsubscribe from event notifications (on a per object basis)
    /// </summary>
    /// <param name="subscriber">The object to be removed. Must be already subscribed</param>
    public void Unsubscribe(INotify subscriber)
    {
        if (_subscribers.Contains(subscriber))
            _subscribers.Remove(subscriber);
    }

    /// <summary>
    /// Used to inform all subscibers of an event.
    /// Any script is allowed notify everybody if necessary.
    /// Requires event to be defined in EventNotfiy (in INotify.cs)
    /// </summary>
    /// <param name="notify"></param>
    public void NotifyAll(EventNotify notify)
    {
        foreach(INotify obj in _subscribers)
        {
            obj.Notify(notify);
        }
    }
}
