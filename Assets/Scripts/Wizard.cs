using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] WizardLight wizardLight = null;

    [SerializeField] float burstCD = 2f;

    private float burstTimer = 0f;
    private bool inControl = false;
    private bool burstReady = false;

    // Start is called before the first frame update
    void Start()
    {
        burstReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.paused && !PlayerManager.death)
        {
            if (!burstReady) BurstCooldown();

            CheckControl();

            if (inControl)
            {
                if (Input.GetMouseButtonDown(0) && burstReady)
                {
                    wizardLight.Burst();
                    burstReady = false;
                }
            }
        }
    }

    private void CheckControl()
    {
        // For wizard
        var temp = playerManager.GetCharacterControl();
        if (!temp)
            inControl = true;
        else
            inControl = false;
    }

    private void BurstCooldown()
    {
        if (burstTimer < burstCD)
            burstTimer += Time.deltaTime;
        else if (burstTimer >= burstCD)
        {
            burstTimer = 0f;
            burstReady = true;
            wizardLight.BurstReady();
        }
    }

    public GameObject GetWizardLight()
    {
        return wizardLight.gameObject;
    }
}
