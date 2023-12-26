using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacao : MonoBehaviour
{
    // Start is called before the first frame update



    public Animator animator;
    public float animationLength;
    public bool isReloading = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "spin")
            {
                animationLength = clip.length;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void reloadplssss(float amount)
    {
        if (!isReloading)
        {
            StartCoroutine(reload(amount));
        }
    }
    public IEnumerator reload(float amount)
    {
        
        isReloading = true;
        animator.SetBool("reloading", true);

        yield return new WaitForSeconds(animationLength * amount);
        
        
        animator.SetBool("reloading", false);
        gameObject.GetComponent<gunINfo>().bullets = gameObject.GetComponent<gunINfo>().maxbullets;
        isReloading = false;
        
    }

}
