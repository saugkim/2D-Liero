using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public int radius = 10;
    bool hitToWall = false; 

	void Update ()
    {
        hitToWall = Camera.main.GetComponent<PaintRay>().PaintHitObject(gameObject.transform.position, radius);
        
        if(hitToWall)
        {
            Destroy(gameObject);
        }
	}

}
