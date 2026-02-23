using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandInvoker
{

    private PlayerActions actor;

    private MoveCommand _moveCommand;

    private Command currentCommand;

    public CommandInvoker(PlayerActions actor)
    { 
        this.actor = actor;

        _moveCommand = new MoveCommand(actor);
    }

    public void Execute()
    { 
        
    }

}
