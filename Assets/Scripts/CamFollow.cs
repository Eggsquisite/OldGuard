using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] float smoothSpeed = 5f;
    [SerializeField] Vector3 offset;

    private GameObject currentPlayer;
    private bool playerSet = false;

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
            Vector3 desiredPosition = currentPlayer.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}
