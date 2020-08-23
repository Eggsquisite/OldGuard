using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] AudioClip hurtSFX = null;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovementAnim(bool status)
    {
        anim.SetBool("isWalking", status);
    }

    public void DamageTaken(float dmgVal)
    {
        playerManager.DamageTaken(dmgVal);
        if (hurtSFX != null)
            Camera.main.GetComponent<AudioSource>().PlayOneShot(hurtSFX);
    }

    public void AddHealth(float value)
    {
        playerManager.AddHealth(value);
    }

}
