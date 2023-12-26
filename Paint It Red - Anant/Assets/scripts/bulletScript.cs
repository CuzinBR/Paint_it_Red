using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 40;
    float damage;
    bool isGrounded;
    public float groundDistance = 0.4f;
    float timer;
    public LayerMask groundMask;
    public float lifeTime = 0;
    health playerhelth;
    void Start()
    {
        playerhelth = GameObject.Find("player").GetComponent<health>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        if (isGrounded)
        {

            Destroy(gameObject);
        }
        if(lifeTime > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            timer += Time.deltaTime;

            if (timer >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
        
        

    }
    public void SetDamage(float setDamage)
    {
        damage = setDamage;
    }
    public void SetLifeTime(float SetlifeTime)
    {
        lifeTime = SetlifeTime;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("player"))
        {
            
            playerhelth.current -= damage;


        }
        else
        {
            Destroy(gameObject);
        }
    }
}
