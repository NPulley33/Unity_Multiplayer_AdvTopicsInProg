using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamageable
{
    public float Health { get; protected set; }
    public float Max_Health { get; set; }

    public void TakeDamage();
}
