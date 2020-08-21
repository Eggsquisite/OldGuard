using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLight : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    private Vector3 mousePosition;


    // Start is called before the first frame update
    void Start()
    {
        transform.parent = new GameObject("Light").transform;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
    }
}
