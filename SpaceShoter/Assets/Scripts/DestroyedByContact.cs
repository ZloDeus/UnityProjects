using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyedByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Boundary":
                return;

            case "Player":
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
                break;

            default:
                Instantiate(explosion, transform.position, transform.rotation);
                gameController.AddScore(scoreValue);
                break;

        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
