using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public delegate void Timer();
    public static event Timer TimerEnd;

    public EnemySpawner enemySpawner;
    public float increaseFreq = 59f;
    public float maxGameTime = 180f;
    public GameObject startText, endText;

    private float timer = 0f;
    private float baseFreq;
    private float minutes, seconds;
    private bool gameWon = false;
    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        baseFreq = increaseFreq;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (seconds == increaseFreq)
            {   
                // every X seconds, decrease the spawn delay
                enemySpawner.SetSpawnDelay(0.5f, 0.75f);
                Debug.Log("increasing spawn freq " + seconds);
                increaseFreq += baseFreq;
            }

            if (timer < maxGameTime)
            {
                timer += Time.deltaTime;
                minutes = Mathf.FloorToInt(timer / 60);
                seconds = Mathf.FloorToInt(timer % 60);
            }
            else if (timer >= maxGameTime)
            {
                // event added from Enemy.cs
                TimerEnd?.Invoke();
                gameWon = true;
                gameStart = false;
                enemySpawner.StopEnemySpawn();
            }
        }

        if (gameWon)
            endText.SetActive(true);
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
