using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Level Settings")]
    public GameObject hazard;
    public GameObject EnemyShip;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private int score;

    [Space]

    [Header("Text References")]
    public TextMeshProUGUI CountText;
    public GameObject scoreUI;
    public GameObject pauseMenuUI;
    public GameObject resumeButtonUI;
    public GameObject settingsMenuUI;

    [Space]

    [Header("GameOver Menu Reference")]
    public GameObject gameoverMenuUI;

    public static bool gameover;
    public static bool restart;
    public static bool GameIsPaused;
    public static bool OptionsMenuIsOpen;

    [Space]

    [Header("Animation Reference")]

    public Animator gameoveranimation;

    public static GameController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        gameover = false;
        restart = false;
        GameIsPaused = false;
        OptionsMenuIsOpen = false;
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && OptionsMenuIsOpen)
            {
                ReturnSettings();
            }

            else if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }

            if (gameover)
            {
                gameoverMenuUI.SetActive(false);
                gameoveranimation.SetBool("IsOpen", false);
            }


        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        Vector3 spawnPositionShip = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                        GameObject shipClone;
                        Rigidbody rbClone;

                        Quaternion spawnRotationEnemy = Quaternion.Euler(0,180,0);

                        shipClone = Instantiate(EnemyShip, spawnPositionShip, spawnRotationEnemy) as GameObject;

                        rbClone = shipClone.GetComponent<Rigidbody>();
                        float CloneShift = new float();
                        CloneShift = Random.Range(-20, 20);
                        rbClone.velocity = transform.TransformDirection(Vector3.forward * -5);

                        rbClone.AddForce(CloneShift * 2, 0.0f, 0.0f);

                        yield return new WaitForSeconds(spawnWait);
                        break;

                    default:
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;

                        Instantiate(hazard, spawnPosition, spawnRotation);

                        yield return new WaitForSeconds(spawnWait);
                        break;
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void SethazardCount (string sethazardCount)
    {
       hazardCount = int.Parse(sethazardCount);
    }

    public void AddScore(int newScoreValue)
    {
        score = score + newScoreValue;
        CountText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameoverMenuUI.SetActive(true);
        gameoveranimation.SetBool("IsOpen", true);
        gameover = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        scoreUI.SetActive(true);

        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
    }

    public void OpenSettings()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        OptionsMenuIsOpen = true;
    }

    public void ReturnSettings()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        OptionsMenuIsOpen = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        scoreUI.SetActive(false);

        if (gameover)
        {
            resumeButtonUI.SetActive(false);
        }

        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Pause");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
