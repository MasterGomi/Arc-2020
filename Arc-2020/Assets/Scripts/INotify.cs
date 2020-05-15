public interface INotify
{
    /// <summary>
    /// Notify the object of an event, which the object can handle however necessary.
    /// </summary>
    /// <param name="notify">The event to notify of</param>
    void Notify(EventNotify notify);
}

/// <summary>
/// Represents events that other objects may need to be notified about.
/// </summary>
public enum EventNotify
{
    EndOfBall,
    NewBall,
    GameOver,
}