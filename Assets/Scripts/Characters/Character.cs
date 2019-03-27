using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    TOP,
    RIGHT,
    BOT,
    LEFT,
    NONE
}

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected Vector3 direction;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected bool isWalking;
    protected bool isAttacking = false;
    protected bool isDead = false;


    [SerializeField] protected int damage;
    [SerializeField] protected int maxHealth;
    [SerializeField] private bool hasRandomHealth;
    [SerializeField] private int maxHealthMin;
    [SerializeField] private int maxHealthMax;
    protected int actualHealth;
    protected SpriteRenderer spriteRenderer;
    private static readonly int Y = Animator.StringToHash("y");
    private static readonly int X = Animator.StringToHash("x");

    public void stopWalking()
    {
        isWalking = false;
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    protected IEnumerator Move(Vector3 nextPosition)
    {
        isWalking = true;
//        rb.velocity = direction.normalized * speed;

        while (transform.position != nextPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            yield return null;
        }


        isWalking = false;
    }

    public Direction GetDirection()
    {
        float x = direction.x;
        float y = direction.y;
        if (x == 0 && y == 1)
        {
            return Direction.TOP;
        }
        else if (x == 1 && y == 0)
        {
            return Direction.RIGHT;
        }
        else if (x == 0 && y == -1)
        {
            return Direction.BOT;
        }
        else if (x == -1 && y == 0)
        {
            return Direction.LEFT;
        }
        else
        {
            Debug.Log("Invalid Direction");
            return Direction.NONE;
        }
    }


    public void SetupActualHealth()
    {
        if (hasRandomHealth)
        {
            this.maxHealth = Random.Range(this.maxHealthMin, this.maxHealthMax);
        }
        actualHealth = this.maxHealth;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        SetupActualHealth();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    protected virtual void FixedUpdate()
    {
    }


    public void HandleLayers()
    {
        if (isWalking && !isAttacking)
        {
            ActivateLayer("Walk Layer");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else if (isAttacking)
        {
            ActivateLayer("Attack Layer");
        }
        else
        {
            ActivateLayer("Iddle Layer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }


    public void TakeDamage(int damage)
    {
        this.actualHealth -= damage;
        Debug.Log("DIEEEEEEEEEEEEEEEEEEEEEEEEEEEE " + actualHealth + " / " + MaxHealth);


        if (actualHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage, string audioClip)
    {
        TakeDamage(damage);
        // Todo: Implementar sons de recebimento de dano
    }


    public virtual void Die()
    {
        isDead = true;
//        animator.SetBool("Run", false);
//        if (transform.rotation.y == 180)
//        {
//            animator.SetBool("DieFront", true);
//        }
//        else
//        {
//            animator.SetBool("DieBack", true);
//        }
//        GameOver.instance.DisplayGameOver();
    }

    public void AnimateMovement()
    {
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    public void ChangeLookingDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                animator.SetFloat(X, -1);
                animator.SetFloat(Y, 0);
                break;
            case Direction.RIGHT:
                animator.SetFloat(X, 1);
                animator.SetFloat(Y, 0);
                break;
        }
    }

    public bool IsMoving()
    {
        if ((direction.x == 0) && (direction.y == 0))
        {
            return false;
        }

        return true;
    }
}