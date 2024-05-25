using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silme : MonoBehaviour, IEnemy
{
    Animator _animator;
    Rigidbody2D rb;
    Collider2D collider2D;
    bool canMove = true;
    public float Health
    {
        set
        {
            if (value < _health)
            {
                _animator.SetTrigger("getHIt");
            }
            _health = value;
            if (Health <= 0)
            {
                _animator.SetBool("defeated", true);
                canMove = false;
                collider2D.enabled = false; 
            }
        }
        get { return _health; }
    }
    public float Damage { set { _damage = value; } get { return _damage; } }

    public DetectedZone detectedZone;

    float _health = 3;
    float _damage = 1;
    public float moveSpeed = 200f;


    public void OnHit(float damage)
    {
        Health -= damage;
        print(Health);
    }


    public void OnCollisionEnter2D(Collision2D col)
    {
        IPlayer player = col.collider.GetComponent<IPlayer>();
        if (player != null)
        {
            player.OnHit(Damage);
        }
    }

    public void OnHit(float damage, Vector2 knockBack)
    {
        Health -= damage;
        rb.AddForce(knockBack);
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (detectedZone.detectedObjects.Count > 0)
            {
                Vector2 direction = (detectedZone.detectedObjects[0].transform.position - transform.position).normalized;
                rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
    void Remove(){
        Destroy(gameObject);
    }
}
