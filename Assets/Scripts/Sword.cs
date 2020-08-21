using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] Transform pivot = null;
    [SerializeField] PlayerManager playerManager = null;

    SpriteRenderer sp;
    Collider2D coll;

    private bool inControl = false;
    private int damage = 1;

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
            GetMousePos();
    }

    private void CheckControl()
    {
        if (playerManager.GetCharacterControl())
            inControl = true;
        else if (!playerManager.GetCharacterControl())
            inControl = false;
    }

    private void GetMousePos()
    {
        Vector3 v3Pos = Camera.main.WorldToScreenPoint(pivot.transform.position);
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
            collision.GetComponent<Enemy>().Damage(damage);
    }
}
