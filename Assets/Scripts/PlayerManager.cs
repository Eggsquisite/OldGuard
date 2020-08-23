using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject soldier = null;
    [SerializeField] GameObject wizard = null;

    [SerializeField] float moveSpeed = 3f;

    [Header("Slow Mode Variables")]
    [SerializeField] float slowMultiplier = 0.5f;
    [SerializeField] float slowMaxTime = 1f;

    Vector2 movement;
    GameObject currentPlayer;

    private bool soldierControl = false;
    private bool findCharacter = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
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

    }

    private void FixedUpdate()
    {
        var currentPlayerRB = currentPlayer.GetComponent<Rigidbody2D>();
        currentPlayerRB.MovePosition(currentPlayerRB.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
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
}
