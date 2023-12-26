using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemyInfo : MonoBehaviour
{
    public float enemyHealth;
    bool ded = false;
    public LayerMask groundLayer;

    public GameObject enemyBlood;

    

    public ParticleSystem blood;
    public Rigidbody rb;
    public ArtificialUnintelligence unintelligence;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            
            if (!ded)
            {
                GameObject.Find("Main Camera").GetComponent<animate>().Shake();
                GameObject bloodyStuffy = Instantiate(enemyBlood, gameObject.transform);
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
                {
                    print(hit.point);
                    bloodyStuffy.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);

                }
                
                print(bloodyStuffy.transform.position);
                bloodyStuffy.transform.rotation = Quaternion.identity;
                bloodyStuffy.transform.parent = null;
                ded = true;
            }
            
            rb.freezeRotation = false;
            Destroy(unintelligence);
            if (unintelligence.manType.Contains("gun"))
            {
                try {
                    GameObject baby = transform.Find("look at player")
    .GetComponentsInChildren<Transform>(true)
    .FirstOrDefault(t => t.name.Contains("gun")).gameObject;

                    if (baby != null)
                    {

                        baby.transform.parent = null;
                        baby.name = "gun";
                        baby.GetComponent<BoxCollider>().enabled = true;
                        baby.GetComponent<Rigidbody>().isKinematic = false;
                    
                        
                    }
                }
                catch (NullReferenceException)
                {
                        
                }


            }
            Invoke("Die", 2f);
        }
    }
    void Die()
    {
        
        Destroy(gameObject);
        

    }
}
