using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Transform soldier = null;
    [SerializeField] Transform pivot = null;
    [SerializeField] PlayerManager playerManager = null;

    [Header("Sword Modifiers")]
    [SerializeField] float swordSpeed = 200f;
    [SerializeField] float swordGraceTimer = 0.75f;
    [SerializeField] float swordCooldown = 0.5f;
    [SerializeField] int swordDamage = 10;

    [Header("Sword Sounds")]
    [SerializeField] AudioClip swordSwing;
    [SerializeField] AudioClip swordHit;

    SpriteRenderer sp;
    Collider2D coll;
    AudioSource audioSource;

    private float swordBaseTimer = 0f;
    private float swordCDTimer = 0f;
    private bool inControl = false;
    private bool attackCooldown = false;

    void Start()
    {
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        if (coll == null) coll = GetComponent<Collider2D>();
        if (audioSource == null) audioSource = Camera.main.GetComponent<AudioSource>();

        coll.enabled = false;
    }

    void Update()
    {
        CheckControl();

        if (inControl)
        {
            if (!attackCooldown)
                CheckSwordSpeed();

            GetMousePos();
        }

        if (attackCooldown)
            AttackCooldown();
    }

    private void CheckControl()
    {
        var temp = playerManager.GetCharacterControl();
        if (temp)
            inControl = true;
        else
        {
            inControl = false;
            coll.enabled = false;
        }
    }

    private void AttackCooldown()
    {
        // useless right now
        if (swordCDTimer < swordCooldown)
            swordCDTimer += Time.deltaTime;
        else if (swordCDTimer >= swordCooldown)
        {
            swordCDTimer = 0;
            attackCooldown = false;
        }
    }

    private void CheckSwordSpeed()
    {
        var mouseCursorSpeed = Input.GetAxis("Mouse X") / Time.deltaTime + Input.GetAxis("Mouse Y") / Time.deltaTime;
        if (Mathf.Abs(mouseCursorSpeed) >= swordSpeed && !attackCooldown)
            Attacking(true);
        else if (Mathf.Abs(mouseCursorSpeed) <= swordSpeed / 2)
            AttackGraceTimer();
    }

    private void AttackGraceTimer()
    {
        if (swordBaseTimer <= swordGraceTimer)
            swordBaseTimer += Time.deltaTime;
        else if (swordBaseTimer >= swordGraceTimer)
        {
            Attacking(false);
            swordBaseTimer = 0;
        }
    }

    private void GetMousePos()
    {
        Vector3 v3Pos = Camera.main.WorldToScreenPoint(pivot.position);
        v3Pos = Input.mousePosition - v3Pos;
        float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

        pivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (pivot.transform.rotation.z > 0.7 || pivot.transform.rotation.z < -0.7)
            sp.flipY = true;
        else if (pivot.transform.rotation.z < 0.7 || pivot.transform.rotation.z > -0.7)
            sp.flipY = false;
    }

    public void Attacking(bool status)
    {
        coll.enabled = status;
        attackCooldown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            audioSource.PlayOneShot(swordHit);
            collision.GetComponent<Enemy>().DamageTaken(swordDamage, soldier);
        }
    }
}
