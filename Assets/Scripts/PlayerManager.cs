using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject soldier = null;
    [SerializeField] GameObject wizard = null;

    [Header("Player Stats")]
    public float health;
    public int numOfHearts;
    public float moveSpeed = 3f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [Header("Slow Mode Variables")]
    [SerializeField] float slowMultiplier = 0.5f;
    [SerializeField] float slowMaxTime = 1f;

    Vector2 movement;
    GameObject currentPlayer;

    private float baseHealth;
    private bool soldierControl = false;
    private bool findCharacter = false;

    private void OnEnable()
    {
        Ghost.OnStart += SetCharacters;
        EnemyAI.OnStart += SetSoldier;
    }

    private void OnDisable()
    {
        Ghost.OnStart -= SetCharacters;
        EnemyAI.OnStart -= SetSoldier;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        baseHealth = health;
        if (currentPlayer == null)
        {
            soldierControl = true;
            currentPlayer = soldier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
            currentPlayer.GetComponent<Player>().SetMovementAnim(true);
        else
            currentPlayer.GetComponent<Player>().SetMovementAnim(false);
            
        if (Input.GetKeyDown(KeyCode.Q))
            SwitchCharacter();

        if (findCharacter)
        {
            if (Time.timeScale < 1f)
                Time.timeScale += Time.unscaledDeltaTime / slowMaxTime;
            else if (Time.timeScale >= 1f)
            {
                Time.timeScale = 1f;
                findCharacter = false;
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i == health - 0.5f)
                hearts[i].sprite = halfHeart;
            else if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }

    }

    private void FixedUpdate()
    {
        var currentPlayerRB = currentPlayer.GetComponent<Rigidbody2D>();
        currentPlayerRB.MovePosition(currentPlayerRB.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private List<Transform> SetCharacters()
    {
        var temp = new List<Transform>();

        temp.Add(soldier.transform);
        temp.Add(wizard.transform);

        Debug.Log("setting up the ghost");
        return temp;
    }

    private Transform SetSoldier()
    {
        return soldier.transform;
    }

    private void SwitchCharacter()
    {
        // default character is soldier
        soldierControl = !soldierControl;
        currentPlayer.GetComponent<Player>().SetMovementAnim(false);

        if (soldierControl)
            currentPlayer = soldier;
        else
            currentPlayer = wizard;

        Camera.main.gameObject.GetComponent<CamFollow>().UpdatePlayer();
        Time.timeScale = slowMultiplier;
        findCharacter = true;
    }

    public void DamageTaken(float dmgVal)
    {
        health -= dmgVal;
        Debug.Log("player health: " + health);

        if (health <= 0)
            Death();
    }

    public void AddHealth(float value)
    {
        health += value;
        if (health > baseHealth)
            health = baseHealth;
    }

    public bool GetCharacterControl()
    {
        return soldierControl;
    }

    public GameObject GetCurrentPlayer()
    {
        if (currentPlayer != null)
            return currentPlayer;

        return null;
    }

    public GameObject GetWizardLight()
    {
        return wizard.GetComponent<Wizard>().GetWizardLight();
    }

    private void Death()
    { 
    
    }
}
