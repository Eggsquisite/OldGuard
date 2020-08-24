using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public delegate void Timer();
    public static event Timer TimerEnd;

    public EnemySpawner enemySpawner;
    public float increaseFreq = 59f;
    public float freqCooldown = 2f;
    public float maxGameTime = 180f;
    public GameObject startText, endText;
    public Text enemiesKilled, enemiesKilled2;

    private float timer = 0f;
    private float freqTimer = 0f;
    private float seconds;
    public static bool gameWon = false;
    private bool gameStart = false;
    private bool freqCD = false;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (seconds == increaseFreq && !freqCD)
            {   
                // every X seconds, decrease the spawn delay
                enemySpawner.SetSpawnDelay(0.5f, 0.75f);
                Debug.Log("increasing spawn freq " + seconds);
                freqCD = true;
            }

            if (freqCD)
            {
                if (freqTimer < freqCooldown)
                    freqTimer += Time.deltaTime;
                else if (freqTimer >= freqCooldown)
                {
                    freqTimer = 0f;
                    freqCD = false;
                }
            }

            if (timer < maxGameTime)
            {
                timer += Time.deltaTime;
                //minutes = Mathf.FloorToInt(timer / 60);
                seconds = Mathf.FloorToInt(timer % 60);
            }
            else if (timer >= maxGameTime)
            {
                // event added from Enemy.cs
                gameWon = true;
                TimerEnd?.Invoke();
                gameStart = false;
                enemySpawner.StopEnemySpawn();
            }
        }

        if (gameWon && !PlayerManager.death)
        {
            endText.SetActive(true);
            enemiesKilled.text = Enemy.enemiesDead.ToString();
            enemiesKilled2.text = Enemy.enemiesDead.ToString();
            gameWon = false;
        }
    }

    public void StartGame()
    {
        gameStart = true;
        startText.SetActive(true);
        StartCoroutine(TimerCooldown());
    }

    private IEnumerator TimerCooldown()
    {
        yield return new WaitForSecondsRealtime(5f);
        startText.SetActive(false);
    }
}
