using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWall : MonoBehaviour
{
    
    Animator anim;
    public int amountKilled;
    public PlayerCombat playerScript;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // Destroy(gameObject, 2f);
       

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerScript.totalKilled);
        if(amountKilled <= playerScript.totalKilled)
        {
            blowUpWall();
        }
    }

    void blowUpWall() {
        anim.SetTrigger("destroy");
        Destroy(gameObject, 2f);
    }
}
