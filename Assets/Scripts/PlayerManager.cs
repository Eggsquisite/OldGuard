using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject soldier = null;
    [SerializeField] GameObject wizard = null;

    [SerializeField] float moveSpeed = 3f;

    private bool soldierControl = false;

    Vector2 movement;
    GameObject currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
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

        if (soldierControl)
            currentPlayer = soldier;
        else
            currentPlayer = wizard;
    }

    public bool GetCharacterControl()
    {
        return soldierControl;
    }
}
