using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordHitBox : MonoBehaviour, IWeapon
{
    public Collider2D collider2D;
    Vector2 attackOffset;

    public float knockBackForce = 5000f;

    public string Name { set {_name=value;} get{return _name;}}
    public string Rarity { set {_rarity=value;} get{return _rarity;}}
    public float Damge { set{_damge = value;} get{return _damge;}}

    string _name = "sword";
    string _rarity = "normal";
    float _damge = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        attackOffset = transform.localPosition;
    }

    public void RightAttack()
    {
        collider2D.enabled = true;
        transform.localPosition = attackOffset;
    }

    public void LeftAttack()
    {
        collider2D.enabled = true;
        transform.localPosition = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    public void StopAttack()
    {
        collider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Enemy"){
            IEnemy enemy = col.GetComponent<IEnemy>();
            if(enemy != null ){
                Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector2 direction = (Vector2) (col.gameObject.transform.position - parentPosition).normalized;
                Vector2 knockBack = direction * knockBackForce;
                enemy.OnHit(_damge, knockBack);
            }
        }
    }
}
