using System.Collections.Generic;

public abstract class AMission
{
    public readonly string Name;
    public readonly string Description;
    public abstract List<WayPoint> WayPoints { get; }
    public readonly int Reward;
    public abstract float Completion { get; }

    public AMission(string name, string description, int reward)
    {
        Name = name;
        Description = description;
        Reward = reward;
    }
}
