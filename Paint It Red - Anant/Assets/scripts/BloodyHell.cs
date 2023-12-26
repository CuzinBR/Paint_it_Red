using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BloodyHell : MonoBehaviour
{
    [Header("StuffLmao")]
    public float bloodMeter;
    
    public bool redAround;
    
    public float checkRedRadius;
    public LayerMask redLayer;
    public progressbar bar;
    [Header("Powerup && Helth")]
    public health helth;
    public float Speedtimer = 15f;
    public float Damagetimer = 15f;

    public bool speedUp = false;
    public bool DamageUp = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        bar = GameObject.Find("bloodbar").GetComponent<progressbar>();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bar = GameObject.Find("bloodbar").GetComponent<progressbar>();
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {
        if(bloodMeter > 3)
        {
            bloodMeter = 3;
        }
        if(bloodMeter < 0)
        {
            bloodMeter = 0;
        }
        
        
        
        redAround = Physics.CheckSphere(transform.position, checkRedRadius, redLayer);
        


        bar.current = bloodMeter;

        if (Input.GetKey(KeyCode.F) && bloodMeter < 3 && redAround)
        {
            // Find all red colliders within the specified radius
            Collider[] redColliders = Physics.OverlapSphere(transform.position, checkRedRadius, redLayer);

            // Sort the red colliders by distance to the player
            Array.Sort(redColliders, (a, b) => Vector3.Distance(transform.position, a.transform.position)
                                                    .CompareTo(Vector3.Distance(transform.position, b.transform.position)));

            // Iterate through the sorted red colliders
            foreach (Collider redCollider in redColliders)
            {
                GameObject red = redCollider.gameObject;
                if (red != null && bloodMeter < 3)
                {
                    // Calculate the amount of blood needed to reach the maximum of 3
                    float bloodNeeded = 3 - bloodMeter;

                    // If picking this red won't exceed the maximum, add it to the bloodMeter and destroy the red
                    if (bloodNeeded > 0)
                    {
                        bloodMeter++;
                        Destroy(red);
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && bloodMeter > 0 && helth.current <100)
        {
            helth.current += 10;
            bloodMeter--;

        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && bloodMeter >= 2 && !speedUp)
        {

            bloodMeter -= 2;
            
            StartCoroutine(Speedy());
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && bloodMeter >= 3 && !DamageUp)
        {
            bloodMeter -= 3;
           
            StartCoroutine(Damagy());
            
        }


    }
    
    
    IEnumerator Speedy()
    {
        speedUp = true;
        while (Speedtimer > 0)
        {
            
            
            yield return new WaitForSeconds(1);
            Speedtimer -= 1;
        }
        speedUp = false;
        Speedtimer = 15;
        
    }
    IEnumerator Damagy()
    {
        DamageUp = true;
        while (Damagetimer > 0)
        {

            
            yield return new WaitForSeconds(1);
            Damagetimer -= 1;
        }
        DamageUp = false;
        Damagetimer = 15;
        
    }
    IEnumerator Heal()
    {
        
        while(Input.GetKey(KeyCode.Alpha1))
        {
            if(bloodMeter<=0 || helth.current >=100)
            {
                if(helth.current >= 100)
                {
                    helth.current = 100;
                }
                break;
            }


            yield return new WaitForSeconds(1);
            helth.current+=10;
            bloodMeter--;
            
        }
        
    }
}
