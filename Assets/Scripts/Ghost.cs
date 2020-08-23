using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float minMoveSpeed = 1f;
    [SerializeField] float maxMoveSpeed = 2f;
    [SerializeField] List<Transform> characters;

    Vector3 smoothedPosition;
    List<GameObject> targetCharacters = null;

    Transform target;
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = characters[Random.Range(0, 2)];
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
