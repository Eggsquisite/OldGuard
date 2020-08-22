using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLight : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] PlayerManager playerManager = null;

    private Vector3 mousePosition;
    private bool inControl = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = new GameObject("Light").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckControl();

        if (inControl)
            MoveLight();
    }

    private void CheckControl()
    {
        var temp = playerManager.GetCharacterControl();
        if (!temp)
            inControl = true;
        else
            inControl = false;
    }

    private void MoveLight()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
    }

}
