using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legAnimate : MonoBehaviour
{
    public Animator anim;
    public void Kick()
    {
        anim.SetTrigger("kick");
        
    }
}
