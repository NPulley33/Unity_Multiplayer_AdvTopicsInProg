using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MoveCommand : Command
{
    public MoveCommand(PlayerActions actor) : base(actor)
    { 
        
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}
