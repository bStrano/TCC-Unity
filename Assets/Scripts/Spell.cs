using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private string elementString;
    private ElementEnum element;

    public enum ElementEnum
    {
        Fire,
        Ice,
        Lightning
    }

    private Vector3 target;


    // Use this for initialization
    void Awake()
    {
        switch (elementString)
        {
            case "fire":
                element = ElementEnum.Fire;
                break;
            case "ice":
                element = ElementEnum.Ice;
                break;
            case "lightning":
                element = ElementEnum.Lightning;
                break;
        }

        //rb = GetComponent<Rigidbody2D>();
        //      target = GameObject.Find("Skeleton").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Cast(Vector2 direction)
    {
        target = direction;
        Debug.Log("Cast: " + direction);
        Debug.Log("Target");
        Debug.Log(target);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CEnemy enemy = other.GetComponent<CEnemy>();

            enemy.ReceiveSpellDamage(1,element);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
//	    Debug.Log(transform.position + " / " +target);
        if (transform.position == target)
        {
            Destroy(this.gameObject);
        }
    }
}