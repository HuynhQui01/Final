using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlayer
{
    public float moveSpeed = 1f;
    Vector2 movementInput;
    Collider2D collider2D;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    bool canMove = true;
    public SwordHitBox swordHitBox;
    float _health = 1;

    public float Health { set{
        _health = value;
        if(_health <= 0){
            animator.SetBool("isDead", true);
            collider2D.enabled = false;
            canMove = false;
        }
    } get{return _health;}}
    public float Damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int SoulPoints { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }


            if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }

        }
        else
        {
           
        }
    }

    bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }

    }

    void OnFire(){
        canMove = false;
        animator.SetTrigger("attack");
    }

    public void Attack(){
        
        if(spriteRenderer.flipX == true){
            swordHitBox.LeftAttack();
            print("left");
        } else if(spriteRenderer.flipY == false){
            swordHitBox.RightAttack();
            print("right");
        }
        
    }

    public void EndAttack(){
        swordHitBox.StopAttack();
        canMove = true;
    }

    public void OnHit(float damage){
        Health -= damage;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
