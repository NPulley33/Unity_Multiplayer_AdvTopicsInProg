using UnityEngine;

public abstract class Command
{
    private PlayerActions actor;

    protected Command(PlayerActions actor)
    {
        this.actor = actor;
    }

    public abstract void Execute();

}
