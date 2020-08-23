using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Transform pivot = null;
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] float swordSpeed = 200f;
    [SerializeField] float swordGraceTimer = 0.75f;

    [SerializeField] int swordDamage = 10;

    SpriteRenderer sp;
    Collider2D coll;

    private float swordBaseTimer = 0f;
    private bool inControl = false;

    void Start()
    {
        // pivot = new GameObject().transform;
        // transform.parent = pivot;

        if (sp == null) sp = GetComponent<SpriteRenderer>();
        if (coll == null) coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    void Update()
    {
        CheckControl();

        if (inControl)
        {
            CheckSwordSpeed();
            GetMousePos();
        }
    }

    private void CheckControl()
    {
        var temp = playerManager.GetCharacterControl();
        if (temp)
            inControl = true;
        else
            inControl = false;
    }

    private void CheckSwordSpeed()
    {
        var mouseCursorSpeed = Input.GetAxis("Mouse X") / Time.deltaTime + Input.GetAxis("Mouse Y") / Time.deltaTime;
        if (Mathf.Abs(mouseCursorSpeed) >= swordSpeed)
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
            //attacking = false;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collision.GetComponent<Enemy>().Damage(swordDamage);
    }
}
