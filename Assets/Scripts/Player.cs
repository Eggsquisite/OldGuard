using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClip hurtSFX = null;
    [SerializeField] GameObject warning = null;
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] float invulnMaxTime = 1f;

    Animator anim;
    private float invulnTimer = 0f;
    private bool invulnerable = false;
    private bool warningOn = false;
    public bool soldier = false;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable)
            InvulnerableCD();
    }

    private void InvulnerableCD()
    {
        if (invulnTimer < invulnMaxTime)
            invulnTimer += Time.deltaTime;
        else if (invulnTimer >= invulnMaxTime)
        {
            invulnTimer = 0f;
            invulnerable = false;
        }
    }

    public void SetMovementAnim(bool status)
    {
        anim.SetBool("isWalking", status);
    }

    public void DamageTaken(float dmgVal)
    {
        if (!invulnerable)
        {
            playerManager.DamageTaken(dmgVal);
            if (!PlayerManager.death)
                Camera.main.GetComponent<AudioSource>().PlayOneShot(hurtSFX);
            invulnerable = true;

            if (playerManager.GetCharacterControl() && !soldier)
            {
                // if wizard and soldier gets hurt

                Debug.Log("wizard in control and soldier hit");
                warning.SetActive(true);
                StartCoroutine(WarningCD());
            }
            else if (!playerManager.GetCharacterControl() && soldier)
            {
                // if soldier and wizard gets hurt
                Debug.Log("soldier in control and wizard hit");
                warning.SetActive(true);
                StartCoroutine(WarningCD());
            }
        }
    }

    private IEnumerator WarningCD()
    {
        yield return new WaitForSeconds(1f);
        warning.SetActive(false);
    }

    public void AddHealth(float value)
    {
        playerManager.AddHealth(value);
    }

}
