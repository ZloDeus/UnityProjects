using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireControll : MonoBehaviour
{
    public GameObject shot;
    public Transform ShotSpawn;

    private GameObject shotClone;
    private float fireRate;
    private float nextFire;

    AudioSource fireSound;

    private void Start()
    {
        fireRate = 0.25f;
    }

    void Update()
    {
        if ((Time.time > nextFire) && !GameController.GameIsPaused)
        {
            shot.transform.position = ShotSpawn.transform.position;
            shotClone = Instantiate(shot) as GameObject;
            nextFire = Time.time + fireRate;
            fireSound = GetComponent<AudioSource>();
            fireSound.Play();
            
        }
    }

}
