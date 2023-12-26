using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    public int Health = 10;
    public float gravity = 20f;
    public float kickDownForce = 100f;

    bool speedInc = false;

    

    public BloodyHell bloodyHell;

    public CharacterController controller;

    public Transform groundCheck;
    public Transform celingCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    public Vector3 pos;

    public bool revived;
    public bool isGrounded;
    public bool CellingTouch;
    // Start is called before the first frame update
    void Start()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        Time.timeScale = 1;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        Time.timeScale = 1;
        
        if(!revived)
        {
            SaveLastPosition();
        }

    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame


    private void LateUpdate()
    {
        if (revived)
        {
            
            SetPos(pos);
             
            GameObject.Find("Main Camera").GetComponent<gunShananigans>().LoadGuns();
            gameObject.GetComponent<BloodyHell>().bloodMeter = 0;
            

            
            revived = false;
        }
    }


    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        CellingTouch = Physics.CheckSphere(celingCheck.position, -groundDistance, groundMask);
        
        if (bloodyHell.speedUp && !speedInc)
        {
            speed += 5;
            speedInc = true;
            
        }
        if (!bloodyHell.speedUp && speedInc)
        {
            speedInc = false;
            speed = 10f;
        }



        Vector3 move = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = 0;
            }
        }
        if (CellingTouch)
        {
            velocity.y = -5;
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded) 
        {
            velocity.y = -kickDownForce;
           
        }

        

        


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            StartCoroutine(damage());
        }
    }

    IEnumerator damage()
    {
        while (Health >0)
        {
            yield return new WaitForSeconds(1);
            Health--;
        }
    }

    void ApplyInstantForce(Vector3 force)
    {
        
        Vector3 newPosition = transform.position + force;


        

        
    }


    void SaveLastPosition()
    {
        // Save the current position to PlayerPrefs
        pos = transform.position;
        PlayerPrefs.SetFloat("LastPosX", pos.x);
        PlayerPrefs.SetFloat("LastPosY", pos.y);
        PlayerPrefs.SetFloat("LastPosZ", pos.z);
    }
    void SetPos(Vector3 pos)
    {
        
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    

}
