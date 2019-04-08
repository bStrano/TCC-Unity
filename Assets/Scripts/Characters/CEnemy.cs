using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CEnemy : Character
{
//    [SerializeField] protected Image healthUI;
    private AudioSource audioSource;
    protected Animator animator;
    [SerializeField] private string immunity;
//    protected int maxHealth;
    private Vector2 target;
    [SerializeField] private bool left;

    private Vector2 initialPosition;
//    [SerializeField] private GameObject HealthCanvas;

    // Efeitos Visuais
//     [SerializeField] private GameObject fireParticle;
    [SerializeField] private GameObject fireExplosionParticle;
    [SerializeField] private GameObject LightningParticle;
    [SerializeField] private GameObject LightningParticle2;


    [SerializeField] private GameObject IceParticle;

    public string Immunity
    {
        get { return immunity; }
        set { immunity = value; }
    }

    // Use this for initialization
    public virtual void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();
        initialPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isWalking)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }


    public void AttackPlayer(Player player, Vector2 nextPlayerPosition)
    {
        int playerPositionY;
        int playerPositionX;
        if (nextPlayerPosition == Vector2.zero)
        {
            playerPositionY = (int) Math.Truncate(player.transform.position.y);
            playerPositionX = (int) Math.Truncate(player.transform.position.x);
        }
        else
        {
            playerPositionY = (int) Math.Truncate(nextPlayerPosition.y);
            playerPositionX = (int) Math.Truncate(nextPlayerPosition.x);
        }

        int transformX = (int) Math.Truncate(transform.position.x);
        int transformY = (int) Math.Truncate(transform.position.y);


        if (playerPositionY == transformY)
        {
            int difference = Math.Abs(playerPositionX - transformX);
            Debug.Log("DIFF: " + difference);
            if (difference == 1|| difference == 0 )
            {
                Attack();
            }
            else if (difference > 1 && difference <=  3)
            {
                if (Math.Abs(transformX - (int) Math.Truncate(initialPosition.x)) >= 2)
                {
                    Debug.Log("TESTTTTEEEEEEEEEEEEEEEEE");
                    Debug.Log(Math.Abs(transformX - (int) Math.Truncate(initialPosition.x)));
                    MoveToSpawnPoint();
                }

                MoveToPlayer();
            }
            else
            {
                MoveToSpawnPoint();
            }
        }
        else
        {
            if (transform.position.x != initialPosition.x)

            {
                MoveToSpawnPoint();
            }
        }
    }

    void MoveToPlayer()
    {
        Rotate(true);
        Vector2 nextPosition = GetNextPosition(true);
        if (Math.Abs((nextPosition.x - initialPosition.x)) > 2)
        {
            MoveToSpawnPoint();
        }
        else
        {
            StartCoroutine(this.Move(nextPosition));
        }
    }

    void MoveToSpawnPoint()
    {
        Rotate(false);
        Vector2 nextPosition = GetNextPosition(false);
        if ((Math.Abs(transform.position.x - initialPosition.x) > 0.8))
        {
            StartCoroutine(this.Move(nextPosition));
        }
        else
        {
            Rotate(true);
        }
    }

    void Rotate(bool toPlayer)
    {
        // player == true
        // spawn = false
        bool condition = left;
        if (toPlayer)
        {
            condition = !left;
        }

        if (condition)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    Vector2 GetNextPosition(bool inverse)
    {
        StopAllCoroutines();
//         true = Move to player
//         false = Initial Position
        bool condition = left;
        if (inverse)
        {
            condition = !left;
        }

        Vector2 nextPosition;
        if (condition)
        {
            nextPosition = new Vector2(transform.position.x - 1, transform.position.y);
            Debug.Log("XXX: " + nextPosition);
        }
        else
        {
            nextPosition = new Vector2(transform.position.x + 1, transform.position.y);
            Debug.Log("YYY: " + nextPosition);
        }

        return nextPosition;
    }


    protected virtual void Attack()
    {
        Debug.Log("Attack 1");
    }

    void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }


    public void Respawn()
    {

        SetupActualHealth();
        animator.SetBool("DieBack", false);
        animator.SetBool("DieFront", false);
        animator.SetBool("Idle", true);
        isDead = false;
        gameObject.transform.position = initialPosition;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().WakeUp();
    }
    
    public override void Die()
    {
        base.Die();
        if (transform.rotation.y == 180)
        {
            animator.SetBool("DieFront", true);
        }
        else
        {
            animator.SetBool("DieBack", true);
        }

        gameObject.layer = LayerMask.NameToLayer("Enemy");

        Debug.Log("DIE");
        CancelInvoke();
        if (audioSource != null)
        {
            audioSource.enabled = false;
        }

        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        isDead = true;
        //gameObject.SetActive(false);
//        HealthCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isDead)
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(damage);
            }
        }
    }


    public void ReceiveSpellDamage(int damage, Spell.ElementEnum spell)
    {
        this.TakeDamage(damage);
        switch (spell)
        {
            case Spell.ElementEnum.Fire:
                fireExplosionParticle.SetActive(true);
                break;
            case Spell.ElementEnum.Lightning:
                LightningParticle.SetActive(true);
                LightningParticle2.SetActive(true);
                break;
            case Spell.ElementEnum.Ice:
                IceParticle.SetActive(true);
                break;
        }

//        fireParticle.SetActive(true);
    }
}