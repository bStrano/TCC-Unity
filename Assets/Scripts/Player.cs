using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Character {
    public Tilemap map;

    [SerializeField]
    private GameObject[] spellPrefab;
    [SerializeField]
    private Transform[] exitPoints;
    private int exitIndex =2;
    private Transform target;

    private Sprite groundSprite;


    

    // Use this for initialization
    protected override void  Start () {
        base.Start();
 
        target = GameObject.Find("Skeleton").transform;
       
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();    
    }


     IEnumerator Move(Vector3 nextPosition)
    {
       // groundSprite = map.GetSprite(Vector3Int.RoundToInt(new Vector3(transform.position.x, transform.position.y)));
        Sprite nextPositionSprite = map.GetSprite(Vector3Int.RoundToInt(new Vector3(nextPosition.x, nextPosition.y)));
        Debug.Log(" O próximo tile é : " + nextPositionSprite.name);
        Debug.Log(nextPositionSprite.name == "sand_tile" || nextPositionSprite.name.Contains("grass"));
        if ( nextPositionSprite.name == "sand_tile" || nextPositionSprite.name.Contains("grass"))
        {

            rb.velocity = direction.normalized * speed;
            while(transform.position != nextPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                yield return null;
            }
            
        } else
        {
            Debug.Log("ELSE");
        }
        isWalking = false;

    }

    public void MoveToDirection(Command direction)
    {
        if (!isWalking)
        {
            isWalking = true;
            switch (direction)
            {
                case (Command.Walk_Top):
                    this.direction.x = 0;
                    this.direction.y = 1;
                    exitIndex = 0;
                    StartCoroutine(Move(new Vector3(transform.position.x, transform.position.y + 1)));
                    break;
                case (Command.Walk_Right):
                    exitIndex = 1;
                    this.direction.x = 1; 
                    this.direction.y = 0;
                    StartCoroutine(Move(new Vector3(transform.position.x + 1, transform.position.y)));
               
                    break;
                case (Command.Walk_Bot):
                    this.direction.x = 0;
                    this.direction.y = -1;
                    exitIndex = 2;
                    StartCoroutine(Move(new Vector3(transform.position.x, transform.position.y - 1)));
                    break;
                case (Command.Walk_Left):
                    this.direction.x = -1;
                    this.direction.y = 0;
                    exitIndex = 3;
                    StartCoroutine(Move(new Vector3(transform.position.x - 1, transform.position.y)));
                    break;
            }
        }


    }


    public void setActiveCommand(Command playerCommand)
    {

        //PlayerCommand playerCommand = (PlayerCommand)Enum.Parse(typeof(PlayerCommand), command);
        if (playerCommand == Command.Walk_Top || playerCommand == Command.Walk_Right || playerCommand == Command.Walk_Bot || playerCommand == Command.Walk_Left )
        {
            Debug.Log("Iniciando a movimentação do personagem, ---- " + playerCommand.ToString());
            MoveToDirection(playerCommand);  
        }
    }
    



    public void AttackTest(string spellName)
    {
        if (!isAttacking && !isWalking)
        {
            StartCoroutine(Attack(spellName));
        }
       
    }

    IEnumerator Attack (string spellName)
    {
        if (!isAttacking && !isWalking)
        {
            isAttacking = true;

            animator.SetBool("isAttacking", isAttacking);

            yield return new WaitForSeconds(1);

            CastSpell(spellName);

            StopAttack();
        }


    }

    public void StopAttack()
    {
        Debug.Log("Done");
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

    public void CastSpell(string spellIndex)
    {
        switch (spellIndex)
        {
            case ("Fireball"):
                Instantiate(spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity);
                break;
            case ("Lightning"):
                Instantiate(spellPrefab[1], exitPoints[exitIndex].position, Quaternion.identity);
                break;
            case ("Ice"):
                Instantiate(spellPrefab[2], exitPoints[exitIndex].position, Quaternion.identity);
                break;
            
        }
        
        
        
    }



}
