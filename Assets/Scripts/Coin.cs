using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour {


    public bool HasCoin(Transform transform)
    {
        Vector3 transformAbs = new Vector3(transform.position.x, (float) Math.Truncate(transform.position.y), 0);
        Debug.Log(this.transform.position.x + " / " +  Math.Abs(this.transform.position.y) + " / " +  transform.position.x + " / " +  Math.Truncate(transform.position.y));
        Vector3 thisTransformAbs = new Vector3(this.transform.position.x, (float) Math.Truncate(this.transform.position.y), 0);
        if ( transformAbs == thisTransformAbs)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool RemoveCoin(Transform transform)
    {
        if (HasCoin(transform))
        {
            this.gameObject.SetActive(false);
            return true;
        } else
        {
            return false;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
