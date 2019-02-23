using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private string element;
    
    
    private Vector3 target;


	// Use this for initialization
	void Awake () {
        //rb = GetComponent<Rigidbody2D>();
  //      target = GameObject.Find("Skeleton").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Cast(Vector2 direction)
	{
		target = direction;
		Debug.Log("Cast: " + direction);
		Debug.Log("Target");
		Debug.Log(target);
	}

    private void FixedUpdate()
    {
	    float step = speed * Time.deltaTime;

	    // move sprite towards the target location

	    transform.position = Vector2.MoveTowards(transform.position, target, step);
	    Debug.Log(transform.position + " / " +target);
	    if (transform.position == target)
	    {
		    Destroy(this.gameObject);
	    }
	   // rb.velocity = new Vector2(speed, 0);
        // Vector2 direction = target.position - transform.position;
        //  rb.velocity = direction.normalized * speed;
       //Debug.DrawLine(transform.position, transform.forward,Color.yellow);
       // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward,range);
       // if (!hit.Equals(null))
        //{
          //  Debug.Log(hit.transform.name);
        //}
            
        
       
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
  //      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
