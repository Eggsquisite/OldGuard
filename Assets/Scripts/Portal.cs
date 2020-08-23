using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float tpCooldown = 2f;
    [SerializeField] Transform teleportDest = null;

    Collider2D coll;
    private float tpTimer = 0f;
    private bool teleported = false;


    private void Start()
    {
        if (coll == null) coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!teleported)
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        else
            TeleportCD();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            teleportDest.GetComponent<Portal>().PlayerTeleported();
            collision.transform.position = teleportDest.position;
        }
    }

    public void PlayerTeleported()
    {
        teleported = true;
        coll.enabled = false;
    }

    private void TeleportCD()
    {
        if (tpTimer < tpCooldown)
            tpTimer += Time.deltaTime;
        else if (tpTimer >= tpCooldown)
        {
            tpTimer = 0;
            teleported = false;
            coll.enabled = true;
        }
    }
}
