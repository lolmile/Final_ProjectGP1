using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWall : MonoBehaviour
{
    
    Animator anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // Destroy(gameObject, 2f);
        Invoke("blowUpWall", 2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void blowUpWall() {
        anim.SetTrigger("destroy");
    }
}
