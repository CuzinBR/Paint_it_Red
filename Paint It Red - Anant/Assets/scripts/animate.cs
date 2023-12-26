using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;

    public void Shake()
    {
        anim.SetTrigger("shake");
    }
    public void Shaky()
    {
        anim.SetTrigger("shakebit");
    }
}
