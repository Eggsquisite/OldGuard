using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{

    [SerializeField] float healthValue = 1f;
    [SerializeField] AudioClip pickupSound = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().AddHealth(healthValue);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
