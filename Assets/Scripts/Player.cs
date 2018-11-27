using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public GameObject testrayGround;
    bool grounded;

    public GameObject testrayHead;
    bool headHit;
    bool firstHeadHit;

    public GameObject testrayFront;
    bool frontHit;

    public GameObject testraySlope;
    bool slopeHit; 

    public GameObject ammo;
    public GameObject hook;
    public GameObject spawner;
    GameObject hookInstance;
   
    HitRay hitRay;
    PaintRay paintRay;

    public float jumpForce;
    public float movementSpeed;
    public float reducedSpeed;
    public int diggingRadius = 10;
    public float diggingSpeed;
    public bool digging = false;

    public ParticleSystem diggingParticle; 

    public float defaultGravity;

	void Start ()
    {

        rb2D = GetComponent<Rigidbody2D>();

        hitRay = Camera.main.GetComponent<HitRay>();
        paintRay = Camera.main.GetComponent<PaintRay>();

        ParticleSystem.EmissionModule em = diggingParticle.emission;
        em.enabled = false;
    }

	void Update()
    {
        MoveAndCheckFrontHitAndDigging();

        JumpAndCheckRoofAndGroundHit();

        ShootAmmo();

        ShootHook();
    }


    private void MoveAndCheckFrontHitAndDigging()
    {
        frontHit = hitRay.RayHitObject(testrayFront.transform.position);
        slopeHit = hitRay.RayHitObject(testraySlope.transform.position);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            ParticleSystem.EmissionModule particleEffect = diggingParticle.emission;

            if (frontHit == false)
            {
                if (slopeHit)
                {
                    transform.Translate(x * reducedSpeed * Time.deltaTime, reducedSpeed * Time.deltaTime, 0);
                }

                else
                {
                    transform.Translate(x * movementSpeed * Time.deltaTime, 0, 0);
                }

                particleEffect.enabled = false;
            }

            else
            {
                transform.Translate(x * diggingSpeed * Time.deltaTime, y * diggingSpeed * Time.deltaTime, 0);

                particleEffect.enabled = true;

                paintRay.PaintHitObject(transform.position, diggingRadius);
            }

            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }
    }

    private void JumpAndCheckRoofAndGroundHit()
    {

        grounded = hitRay.RayHitObject(testrayGround.transform.position);
        headHit = hitRay.RayHitObject(testrayHead.transform.position);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = Vector2.up * jumpForce;
            rb2D.gravityScale = defaultGravity;
            grounded = false;
        }

        if (grounded)
        {
            rb2D.gravityScale = 0;
            rb2D.velocity = new Vector2(0, 0);
        }
        else
        {
            rb2D.gravityScale = defaultGravity;
        }

        if (headHit == true && firstHeadHit == false)
        {
            firstHeadHit = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }

        if (firstHeadHit == true && headHit == false)
        {
            firstHeadHit = false;
        }
    }

    private void ShootHook()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!GetComponent<SpringJoint2D>())
            {
                hookInstance = Instantiate(hook, spawner.transform.position, Quaternion.identity);

                hookInstance.GetComponent<Rigidbody2D>().
                    AddForce(transform.localScale.x * spawner.transform.right * 15, ForceMode2D.Impulse);
            }
            else
            {
                Destroy(hookInstance);
                Destroy(GetComponent<SpringJoint2D>());
            }
        }
    }

    private void ShootAmmo()
    {
        if (Input.GetButtonDown("Fire1") && GUIUtility.hotControl == 0)
        {
            GameObject ammoInstance = Instantiate(ammo, spawner.transform.position, Quaternion.identity);

            ammoInstance.GetComponent<Rigidbody2D>().
                AddForce(transform.localScale.x * spawner.transform.right * 15, ForceMode2D.Impulse);
        }
    }
}
