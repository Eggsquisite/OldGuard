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
    Rigidbody2D currentPlayerRB;

    // Start is called before the first frame update
    void Start()
    {
        if (currentPlayerRB == null)
        {
            currentPlayerRB = soldier.GetComponent<Rigidbody2D>();
            soldierControl = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
            SwitchCharacter();
    }

    private void FixedUpdate()
    {
        currentPlayerRB.MovePosition(currentPlayerRB.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void SwitchCharacter()
    {
        // default character is soldier
        soldierControl = !soldierControl;

        if (soldierControl)
            currentPlayerRB = soldier.GetComponent<Rigidbody2D>();
        else
            currentPlayerRB = wizard.GetComponent<Rigidbody2D>();
    }

    public bool GetCharacterControl()
    {
        return soldierControl;
    }
}
