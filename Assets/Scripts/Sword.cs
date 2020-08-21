using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform pivot;
    public PlayerManager playerManager;
    public float fRadius = 3.0f;

    private bool inControl = false;
    SpriteRenderer sp;

    void Start()
    {
        // pivot = new GameObject().transform;
        // transform.parent = pivot;

        if (sp == null) sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckControl();

        if (inControl)
        {
            Vector3 v3Pos = Camera.main.WorldToScreenPoint(pivot.transform.position);
            v3Pos = Input.mousePosition - v3Pos;
            float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

            pivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        Debug.Log(pivot.transform.rotation.z);

        if (pivot.transform.rotation.z > 0.7 || pivot.transform.rotation.z < -0.7)
            sp.flipY = true;
        else if (pivot.transform.rotation.z < 0.7 || pivot.transform.rotation.z > -0.7)
            sp.flipY = false;
    }

    private void CheckControl()
    {
        if (playerManager.GetCharacterControl())
            inControl = true;
        else if (!playerManager.GetCharacterControl())
            inControl = false;
    }
}
