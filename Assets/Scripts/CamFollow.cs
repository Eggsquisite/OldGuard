using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] float smoothSpeed = 5f;
    [SerializeField] float xVal = 3f;
    [SerializeField] float yVal = 3f;
    [SerializeField] Vector3 offset;

    private GameObject currentPlayer;
    private Vector3 desiredPosition, smoothedPosition;
    private Transform soldier, wizard;
    private bool playerSet = false;
    bool isWizard = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playerSet)
        {
            var temp = playerManager.GetCurrentPlayer();
            if (temp != null)
            {
                currentPlayer = temp;
                playerSet = true;
            }
        }
        else
        {

            if (!isWizard)
            {
                desiredPosition = currentPlayer.transform.position + offset;
            }
            else
            {
                desiredPosition = new Vector3(
                    Mathf.Clamp(currentPlayer.transform.position.x, wizard.position.x - xVal, wizard.position.x + xVal),
                    Mathf.Clamp(currentPlayer.transform.position.y, wizard.position.y - yVal, wizard.position.y + yVal),
                    transform.position.z);
            }
            
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    public void UpdatePlayer()
    {
        isWizard = !playerManager.GetCharacterControl();
        currentPlayer = playerManager.GetCurrentPlayer();

        if (isWizard)
        {
            wizard = currentPlayer.transform;
            currentPlayer = playerManager.GetWizardLight();
        }
    }
}
