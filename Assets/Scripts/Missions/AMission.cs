using System.Collections.Generic;

public abstract class AMission
{
    private string name;
    private string description;
    public abstract List<WayPoint> WayPoints { get; }
    private int reward;
    public abstract float Completion { get; }

    public AMission(string name, string description, int reward)
    {
        this.name = name;
        this.description = description;
        this.reward = reward;
    }
}
