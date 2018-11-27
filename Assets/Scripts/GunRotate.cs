using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{

    public GameObject player;
    float angle; 

	void Update ()
    {

        Vector3 gunDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (player.transform.localScale.x == -1)
        {
            //Atan2(y,x) Returns the angle in radians whose Tangent is y/x and convert to angle in degree
            angle = Mathf.Atan2(gunDirection.y * -1, gunDirection.x * -1) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}
}
