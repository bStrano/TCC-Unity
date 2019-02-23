using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Character
{
    public Tilemap map;

    [SerializeField] private GameObject[] spellPrefab;
    [SerializeField] private Transform[] exitPoints;
    private int exitIndex = 2;
    private Transform target;

    private Sprite groundSprite;
    private Command lastMovementCommand = Command.Walk_Bot;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void StopAllCouroutines()
    {
        StopAllCoroutines();
        isWalking = false;
    }

    IEnumerator Move(Vector3 nextPosition)
    {
        isWalking = true;
        rb.velocity = direction.normalized * speed;
        while (transform.position != nextPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            yield return null;
        }

        isWalking = false;
    }


    bool MoveIfPossible(Vector3 nextPosition)
    {
        // groundSprite = map.GetSprite(Vector3Int.RoundToInt(new Vector3(transform.position.x, transform.position.y)));
        Sprite nextPositionSprite =
            map.GetSprite(Vector3Int.RoundToInt(new Vector3(nextPosition.x - 0.10f, nextPosition.y)));
        //Debug.Log(Vector3Int.RoundToInt(new Vector3(nextPosition.x-0.10f, nextPosition.y)));
        //Debug.Log(" O próximo tile é : " + nextPositionSprite.name);
        //Debug.Log(nextPositionSprite.name == "sand_tile" || nextPositionSprite.name.Contains("grass"));
        //Debug.Log("Next Position Sprite: " + nextPositionSprite);
        if (nextPositionSprite.name == "sand_tile" || nextPositionSprite.name.Contains("grass"))
        {
            StartCoroutine(Move(nextPosition));
            return true;
        }
        else
        {
            direction.x = 0;
            direction.y = 0;
            isWalking = false;
            return false;
        }
    }

    public bool CollectCoin()
    {
        Debug.Log("Collect Coin");
        return ObjectsManager.instance.RequestCoinCollect(transform);
    }

    public bool OpenChest()
    {
        Direction direction = ObjectsManager.instance.RequestOpenChest(transform);
        if (direction != Direction.NONE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MoveToDirection(Command direction)
    {
        if (!isWalking)
        {
            Vector3 directionBeforeMovement = new Vector3(this.direction.x, this.direction.y, 0);
            isWalking = true;
            Vector3 position = transform.position;
            switch (direction)
            {
                case (Command.Walk_Top):
                    exitIndex = 0;
                    return TryMove(Command.Walk_Top, position.x, position.y + 1, 0, 0, 1);
                case (Command.Walk_Right):
                    exitIndex = 1;
                    return TryMove(Command.Walk_Right, position.x + 1, position.y, 1, 1, 0);
                case (Command.Walk_Bot):
                    return TryMove(Command.Walk_Bot, position.x, position.y - 1, 2, 0, -1);
                case (Command.Walk_Left):
                    return TryMove(Command.Walk_Left, position.x - 1, position.y, 3, -1, 0);
                default:
                    return false;
            }
        }
        else
        {
            Debug.Log("false");
            return false;
        }
    }

    private bool TryMove(Command command, float posX, float posY, int exitIndex, int directionX, int directionY)
    {
        bool hasMoved = MoveIfPossible(new Vector3(posX, posY));
        if (hasMoved)
        {
            this.direction.x = directionX;
            this.direction.y = directionY;
            this.exitIndex = exitIndex;
            this.lastMovementCommand = command;
            Debug.Log(lastMovementCommand);
        }

        return hasMoved;
    }


    public bool setActiveCommand(Command playerCommand)
    {
        //PlayerCommand playerCommand = (PlayerCommand)Enum.Parse(typeof(PlayerCommand), command);
        if (playerCommand == Command.Walk_Top || playerCommand == Command.Walk_Right ||
            playerCommand == Command.Walk_Bot || playerCommand == Command.Walk_Left)
        {
            //Debug.Log("Iniciando a movimentação do personagem, ---- " + playerCommand.ToString());
            return MoveToDirection(playerCommand);
        }
        else if (playerCommand == Command.Collect_Coin)
        {
            return CollectCoin();
        }
        else if (playerCommand == Command.Open_Chest)
        {
            return OpenChest();
        }
        else if (playerCommand == Command.Fireball)
        {
            return AttackTest("Fireball");
        }
        else if (playerCommand == Command.Ice)
        {
            return AttackTest("Ice");
        }
        else if (playerCommand == Command.Lightning)
        {
            return AttackTest("Lightning");
        }
        else
        {
            return false;
        }
    }


    public bool AttackTest(string spellName)
    {
        if (!isAttacking && !isWalking)
        {
            StartCoroutine(Attack(spellName));
        }

        return true;
    }

    IEnumerator Attack(string spellName)
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
        GameObject prefab;
        switch (spellIndex)
        {
            case ("Fireball"):
                prefab = Instantiate(spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity);
                break;
            case ("Lightning"):
                prefab = Instantiate(spellPrefab[1], exitPoints[exitIndex].position, Quaternion.identity);
                break;
            case ("Ice"):
                prefab = Instantiate(spellPrefab[2], exitPoints[exitIndex].position, Quaternion.identity);
                break;
            default:
                return;
        }

        Spell spell = prefab.GetComponent<Spell>();
        Vector3 position = prefab.transform.position;
        switch (lastMovementCommand)
        {
            case Command.Walk_Top:
                prefab.transform.rotation = Quaternion.Euler(0, 0, 90);
                spell.Cast(new Vector2(position.x, position.y+2) );
                break;
            case Command.Walk_Bot:
                prefab.transform.rotation = Quaternion.Euler(0, 0, 270);
                spell.Cast(new Vector2(position.x, position.y-2) );
                break;
            case Command.Walk_Left:
                prefab.transform.rotation = Quaternion.Euler(0, 180, 0);
                spell.Cast(new Vector2(position.x-2, position.y) );
                break;
            case Command.Walk_Right:
                prefab.transform.rotation = Quaternion.Euler(0, 0, 0);
                spell.Cast(new Vector2(position.x+2, position.y) );
                break;
        }

    }
}