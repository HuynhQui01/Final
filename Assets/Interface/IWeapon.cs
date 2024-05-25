using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



public interface IWeapon
{
    public string Name { get; set; }
    public string Rarity { get; set; }
    public float Damge { get; set; }
    public void RightAttack();
    public void LeftAttack();
    public void StopAttack();

}
