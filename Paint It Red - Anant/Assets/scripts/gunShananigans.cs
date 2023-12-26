using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class gunShananigans : MonoBehaviour
{
    public float forceAgainstObjs = 1000f;
    public float kickForce = 500f;
    public float damage;
    public float damageMultiplier;
    public float kickMultiplier;

    public GameObject[] guns;

    public string leftGunTag;
    public string rightGunTag;

    public progressbar right;
    public progressbar left;

    public BloodyHell bh;

    public GameObject enemyBlood;
    GameObject leg;

    bool activePower = false;
    
    bool nuhUh;

    // Start is called before the first frame update
    void Start()
    {
        //enenmyBlood = GetComponent<ParticleSystem>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        leg = GameObject.Find("leg");
        right = GameObject.Find("rightAmmo").GetComponent<progressbar>();
        left = GameObject.Find("leftAmmo").GetComponent<progressbar>();

        

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        right = GameObject.Find("rightAmmo").GetComponent<progressbar>();
        left = GameObject.Find("leftAmmo").GetComponent<progressbar>();

        SaveGuns();



    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
        if (bh.DamageUp && !activePower)
        {
            damage *= damageMultiplier;
            activePower = true;
        }
        if (!bh.DamageUp && activePower)
        {
            damage = 1;
            activePower = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject baby = GameObject.Find("right");
            if (baby != null)
            {
                throwed(baby);
            }

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject baby = GameObject.Find("left");
            if (baby != null)
            {
                throwed(baby);
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            
            GameObject righty = GameObject.Find("right");
            try
            {
                shoot(righty.tag, true, righty);
            }
            catch (NullReferenceException)
            {
                shoot("no guns :(((", true, righty);
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject lefty = GameObject.Find("left");
            try
            {
                shoot(lefty.tag, false, lefty);
            }
            catch (NullReferenceException)
            {
                shoot("no guns :(((", false, lefty);
            }

        }

        





        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shoot("kick", true, null);
        }

        if (GameObject.Find("right"))
        {
            
            GameObject baby = GameObject.Find("right");
            right.gameObject.SetActive(true);
            right.max = baby.GetComponent<gunINfo>().maxbullets;
            right.current = baby.GetComponent<gunINfo>().bullets;
            
        }
        else
        {
            
            right.gameObject.SetActive(false);
        }
        if (GameObject.Find("left"))
        {
            
            GameObject baby = GameObject.Find("left");
            left.gameObject.SetActive(true);
            left.max = baby.GetComponent<gunINfo>().maxbullets;
            left.current = baby.GetComponent<gunINfo>().bullets;

        }
        else
        {

            left.gameObject.SetActive(false);
        }





    }
    void shoot(string gunType, bool rightArm, GameObject hasGun)
    {
        //ve se e kick ou n

        

        if (gunType == "kick")
        {

            StartCoroutine(KickAction());
        }
        else
        {



            if (hasGun != null)
            {
                GameObject.Find("Main Camera").GetComponent<animate>().Shaky();


                animacao anim = hasGun.GetComponent<animacao>();

                

                if (!anim.isReloading)
                {


                    //pistola
                    if (gunType == "pistol")
                    {

                        RaycastHit hit;


                        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
                        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
                        {


                            if (hit.collider.gameObject.CompareTag("enemy"))
                            {
                                enemyInfo enemy = hit.collider.GetComponent<enemyInfo>();
                                enemy.blood.Play();

                                enemy.enemyHealth -= damage;



                            }



                        }
                    }
                    if (gunType == "rifleGun")
                    {

                        RaycastHit hit;


                        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
                        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
                        {


                            if (hit.collider.gameObject.CompareTag("enemy"))
                            {
                                enemyInfo enemy = hit.collider.GetComponent<enemyInfo>();
                                enemy.blood.Play();

                                enemy.enemyHealth -= damage / 1.5f;



                            }



                        }
                    }

                    if (gunType == "shotgun")
                    {
                        ShootShotgun(hasGun);
                    }










                    StartCoroutine(reload(hasGun.GetComponent<gunINfo>(), anim));
                }
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
                {

                    if (hit.collider.gameObject.name.Contains("gun"))
                    {
                        gunPickUp(hit, rightArm);
                    }
                }
            }
        }


    }
    void gunPickUp(RaycastHit gun, bool right)
    {
        gun.transform.SetParent(gameObject.transform, false);
        gun.transform.localRotation = Quaternion.identity;
        gun.collider.gameObject.layer = 7;
        gun.rigidbody.isKinematic = true;
        if (right)
        {
            gun.transform.name = "right";
            gun.transform.localPosition = new Vector3(1.024f, -0.491f, 1.21f);
        }
        else
        {
            gun.transform.name = "left";
            gun.transform.localPosition = new Vector3(-1.024f, -0.491f, 1.21f);
        }
    }
    void throwed(GameObject baby)
    {
        baby.layer = 0;
        baby.transform.parent = null;
        baby.GetComponent<Rigidbody>().isKinematic = false;
        baby.GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Acceleration);
        baby.name = "gun";
    }
    
    IEnumerator kickDown()
    {
        while (!transform.parent.GetComponent<movement>().isGrounded)
        {
            Debug.DrawRay(transform.parent.position, -transform.parent.up * 2f, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position, -transform.parent.up, out hit, 2f))
            {



                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(transform.parent.forward * kickForce, ForceMode.Acceleration);
                }
                if (hit.collider.gameObject.tag == "enemy")
                {

                    enemyInfo enemy = hit.collider.GetComponent<enemyInfo>();
                    enemy.blood.Play();
                    enemy.enemyHealth -= damage * kickMultiplier;


                }
                yield return new WaitForFixedUpdate();

            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator reload(gunINfo info, animacao anim)
    {
        
        if (info.bullets > 0)
        {
            if (!anim.isReloading)
            {
                anim.isReloading = true;
                info.bullets -= 1;
                yield return new WaitForSeconds(info.seconds);
                

                anim.isReloading = false;
                if(info.bullets == 0)
                {
                    anim.reloadplssss(info.amountReloaded);
                }
            }
        }
        
    }

    IEnumerator KickAction()
    {
        RaycastHit hit;
        
        if (transform.parent.GetComponent<movement>().isGrounded && !nuhUh)
        {
            
            leg.GetComponent<legAnimate>().Kick();
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
            {




                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(transform.forward * kickForce, ForceMode.Acceleration);
                }
                if (hit.collider.gameObject.tag == "enemy")
                {

                    enemyInfo enemy = hit.collider.GetComponent<enemyInfo>();
                    enemy.blood.Play();
                    enemy.enemyHealth -= damage * kickMultiplier;


                }
                nuhUh = true;
                yield return new WaitForSeconds(1);
                nuhUh = false;

            }
        }
        else
        {
            yield return StartCoroutine(kickDown());
        }
    }
    void ShootShotgun(GameObject shotgun)
    {
        
        int numberOfPellets = 8; 
        float spreadAngle = 20f;
        
        for (int i = 0; i < numberOfPellets; i++)
        {
            
            Quaternion rotation = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0);
            

            RaycastHit hit;
            if (Physics.Raycast(transform.position, rotation * transform.forward, out hit, 20f))
            {
                
                


                if (hit.collider.gameObject.CompareTag("enemy"))
                {
                    enemyInfo enemy = hit.collider.GetComponent<enemyInfo>();
                    enemy.blood.Play();
                    
                    
                    enemy.enemyHealth -= damage/2;
                }
            }

        }
        

       
    }
   

    void SaveGuns()
    {
        GameObject lefty = GameObject.Find("left");
        GameObject righty = GameObject.Find("right");

        if(lefty != null)
        {
            leftGunTag = lefty.tag;

        }
        else
        {
            leftGunTag = null;
        }
        if (righty != null)
        {
            rightGunTag = righty.tag;

        }
        else
        {
            rightGunTag = null;
        }
    }

    public void LoadGuns()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        try
        {
            if (leftGunTag != null || leftGunTag != "")
            {
                GameObject lefty = Instantiate(GetGun(leftGunTag));
                lefty.GetComponent<Rigidbody>().isKinematic = true;
                lefty.transform.SetParent(gameObject.transform, false);
                lefty.transform.name = "left";
                lefty.transform.localPosition = new Vector3(-1.024f, -0.491f, 1.21f);
                lefty.transform.localRotation = Quaternion.identity;
            }
        }

        catch (ArgumentException)
        {

        }

        try
        {
            if (rightGunTag != null || rightGunTag != "")
            {
                GameObject righty = Instantiate(GetGun(rightGunTag));
                righty.GetComponent<Rigidbody>().isKinematic = true;
                righty.transform.SetParent(gameObject.transform, false);
                righty.transform.name = "right";
                righty.transform.localPosition = new Vector3(1.024f, -0.491f, 1.21f);
                righty.transform.localRotation = Quaternion.identity;
            }
        }
        catch(ArgumentException)
        {

        }

    }

    
    GameObject GetGun(string objectName)
    {
        
            foreach (GameObject obj in guns)
            {
                if (obj.name == objectName)
                {
                    return obj;
                }
            }
        
        return null; 
    }

}
