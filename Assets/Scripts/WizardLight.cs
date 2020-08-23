using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLight : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float speedSlowMult = 10f;
    [SerializeField] float enemySlowFactor = 3f;
    [SerializeField] PlayerManager playerManager = null;

    Vector3 mousePosition;
    Collider2D coll;
    Animator anim;

    private float baseMoveSpeed;
    private bool inControl = false;
    private bool moveSpeedRecover = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = new GameObject("Light").transform;
        if (anim == null) anim = GetComponent<Animator>();
        if (coll == null) coll = GetComponent<Collider2D>();

        coll.enabled = false;
        baseMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckControl();

        if (inControl)
            MoveLight();

        if (moveSpeedRecover)
        {
            if (moveSpeed < baseMoveSpeed)
                moveSpeed += Time.deltaTime * speedSlowMult / 2;
            else if (moveSpeed >= baseMoveSpeed)
            {
                moveSpeed = baseMoveSpeed;
                moveSpeedRecover = false;
            }
        }
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

    public void Burst()
    {
        coll.enabled = true;
        anim.SetTrigger("burst");
        moveSpeed /= speedSlowMult;
    }

    private void BurstOff()
    {
        coll.enabled = false;
        anim.SetTrigger("burstOff");
        moveSpeedRecover = true;
    }

    public void BurstReady()
    {
        anim.SetTrigger("burstReady");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ghost")
            collision.GetComponent<Enemy>().DamageTaken(999);
        else if (collision.tag == "Enemy")
            collision.GetComponent<Enemy>().Slowed(enemySlowFactor);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            collision.GetComponent<Enemy>().Unslowed();
    }
}
