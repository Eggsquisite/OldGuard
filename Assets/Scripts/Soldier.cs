using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] Sword sword = null;
    [SerializeField] float attackMaxWait = 2f;
    
    Animator anim;
    private bool attackReady = true;
    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
