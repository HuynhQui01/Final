using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public interface IEnemy{
    public float Health { get; set; }
    public float Damage { get; set; }
    public void OnHit(float damage);
    public void OnHit(float damage, Vector2 knockBack);
}