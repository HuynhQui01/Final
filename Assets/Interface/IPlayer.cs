using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IPlayer{
    public float Health { get; set; }
    public float Damage { get; set; }
    public int SoulPoints { get; set; }

    public void OnHit(float damge);
}