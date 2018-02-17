using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementRestriction
{
    public float xMin, xMax, zMin, zMax;
}

public class MovementController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject shotClone;
    private float nextFire;
    private bool accel;
    AudioSource fireSound;

    [Header("Movement")]
    public float modifierSpeed;
    public float modifierAccel;
    public float tilt;
    public float fireRate;

    [Space]

    [Header("GameArenaRestriction")]
    public MovementRestriction movementRestriction;

    [Space]

    [Header("Mix")]
    public GameObject shot;
    public Transform ShotSpawn;

    public static MovementController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if ((Input.GetButton("Fire1") && Time.time > nextFire) && !GameController.GameIsPaused)
        {
            shot.transform.position = ShotSpawn.transform.position;
            shotClone = Instantiate(shot) as GameObject;
            nextFire = Time.time + fireRate;
            fireSound = GetComponent<AudioSource>();
            fireSound.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        accel = Input.GetButton("Accel");

        rb = GetComponent<Rigidbody>();

        if ((moveHorizontal != 0) || (moveVertical != 0))
        {
            switch (accel)
            {
                case false:
                    rb.velocity = new Vector3(moveHorizontal * modifierSpeed, 0.0f, moveVertical * modifierSpeed);
                    Debug.Log("NormalSpeed");
                    break;

                case true:
                    rb.velocity = new Vector3(moveHorizontal * modifierAccel, 0.0f, moveVertical * modifierAccel);
                    Debug.Log("Sprint");
                    break;
            }
        }

        rb.position = new Vector3
        (
           Mathf.Clamp(rb.position.x, movementRestriction.xMin, movementRestriction.xMax),
           0.0f,
           Mathf.Clamp(rb.position.z, movementRestriction.zMin, movementRestriction.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}