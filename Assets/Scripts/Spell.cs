using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    private Transform target;
    [SerializeField]
    private float range;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
  //      target = GameObject.Find("Skeleton").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
         Vector2 direction = target.position - transform.position;
          rb.velocity = direction.normalized * speed;
       Debug.DrawLine(transform.position, transform.forward,Color.yellow);
       // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward,range);
       // if (!hit.Equals(null))
        //{
          //  Debug.Log(hit.transform.name);
        //}
            
        
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
