using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLight : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float speedSlowMult = 10f;
    [SerializeField] PlayerManager playerManager = null;

    
    Vector3 mousePosition;
    Animator anim;

    private float baseMoveSpeed;
    private bool inControl = false;
    private bool moveSpeedRecover = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = new GameObject("Light").transform;
        if (anim == null) anim = GetComponent<Animator>();
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
        anim.SetTrigger("burst");
        moveSpeed /= speedSlowMult;
    }

    private void BurstOff()
    {
        anim.SetTrigger("burstOff");
        moveSpeedRecover = true;
    }

    public void BurstReady()
    {
        anim.SetTrigger("burstReady");
    }
}
