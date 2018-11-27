using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public GameObject ropeConnectionPointInHook;
    public GameObject[] hitCheckers;

    GameObject player;
    Rigidbody2D rb2D;
    SpringJoint2D hookConnection;
    LineRenderer hookRope;
    HitRay hitRay;

    bool hit;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        hookRope = GetComponent<LineRenderer>();
        hitRay = Camera.main.GetComponent<HitRay>();
    }

	void Update ()
    {
        Vector3[] ropePosition = new Vector3[2];
        ropePosition[0] = player.transform.position;
        ropePosition[1] = ropeConnectionPointInHook.transform.position;
        hookRope.SetPositions(ropePosition);

        for (int i = 0; i < hitCheckers.Length; i++)
        {
            hit = hitRay.RayHitObject(hitCheckers[i].transform.position);
            if (hit)
            {
                break;
            }
        }

        if (hit)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;

            if (!player.GetComponent<SpringJoint2D>())
            {
                hookConnection = player.AddComponent<SpringJoint2D>();

                hookConnection.connectedAnchor = ropeConnectionPointInHook.transform.position;
                hookConnection.autoConfigureDistance = false;
                hookConnection.dampingRatio = 1;
            }
            hookConnection.distance -= Input.GetAxis("Horizontal") * 0.02f;
        }
        else
        {
            Destroy(player.GetComponent<SpringJoint2D>());
        }
    }

}
