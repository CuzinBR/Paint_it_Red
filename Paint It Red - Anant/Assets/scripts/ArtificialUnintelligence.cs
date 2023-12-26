using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class ArtificialUnintelligence : MonoBehaviour
{
    




    public float speedmin;
    public float speedmax;
    public float speed;
    public float damage = 10;
    public float attackDis;
    public float seeDis;
    public float fireRate = 2f;
    public GameObject player;
    public health helth;
    public string manType;
    public float bulletTimer = 0;
    public GameObject bulletPrefab;
    public float reloadTime;

    public bool canAttack = true;
    public bool seePlayer;
    public bool attackRange;

    public LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedmin, speedmax);
        player = GameObject.Find("player");
        helth = player.GetComponent<health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manType.Contains("gun"))
        {
            GameObject baby = transform.Find("look at player")
    .GetComponentsInChildren<Transform>(true)
    .FirstOrDefault(t => t.name.Contains("gun")).gameObject;

            
        }




        seePlayer = Physics.CheckSphere(transform.position, seeDis, playerMask);
        attackRange = Physics.CheckSphere(transform.position, attackDis, playerMask);
        if(transform.position.y <= -500)
        {
            Destroy(gameObject);
        }


        if (seePlayer && attackRange)
        {
            Attack(manType);

        }
        if (seePlayer && !attackRange)
        {
            
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.LookAt(player.transform.position);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
            

        }


    }
    void Attack(string type)
    {
        if (type == "puncher")
        {
            if (canAttack)
            {
                helth.current -= damage;
                StartCoroutine(reload());

            }
        }
        if (type == "gunner")
        {


            lookAtPlayer();

            if (canAttack)
            {
                StartCoroutine(FirePistol());
            }
        }
        if(type == "riflegunner")
        {
            lookAtPlayer();
            if (canAttack)
            {
                StartCoroutine(FireRifle());
                
            }
        }
        if (type == "shotgunner")
        {
            lookAtPlayer();
            if (canAttack)
            {
                StartCoroutine(FireShotgun());

            }
        }
    }

    void lookAtPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        transform.LookAt(player.transform.position);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
    }


    IEnumerator FirePistol()
    {
        GameObject pistol = transform.Find("look at player").transform.Find("gun").gameObject;
        while (pistol.GetComponent<gunINfo>().bullets > 0)
        {
            if (!seePlayer)
            {
                yield break;
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.Find("look at player").transform.Find("gun").transform.position, transform.Find("look at player").transform.Find("gun").transform.rotation);

            bulletScript bulletScript = bullet.GetComponent<bulletScript>();

            bulletScript.SetDamage(damage);
            pistol.GetComponent<gunINfo>().bullets--;

            



            canAttack = false;
            yield return new WaitForSeconds(2f);
            canAttack = true;
        }
        
        StartCoroutine(reload());
    }
    IEnumerator FireRifle()
    {
        GameObject rifle = transform.Find("look at player").transform.Find("rifle gun").gameObject;
        while (rifle.GetComponent<gunINfo>().bullets > 0)
        {
            if (!seePlayer)
            {
                yield break;
            }

            GameObject bullet = Instantiate(bulletPrefab, transform.Find("look at player").transform.Find("rifle gun").transform.position, transform.Find("look at player").transform.Find("rifle gun").transform.rotation);
            
            bulletScript bulletScript = bullet.GetComponent<bulletScript>();

            bulletScript.SetDamage(damage);
            rifle.GetComponent<gunINfo>().bullets--;

            

            

            canAttack = false;
            yield return new WaitForSeconds(1 / fireRate);
            canAttack = true;
        }
        

        canAttack = false;

        StartCoroutine(reload());

        



    }

    IEnumerator FireShotgun()
    {
        GameObject shotgun = transform.Find("look at player").transform.Find("shotgun").gameObject;
        while (shotgun.GetComponent<gunINfo>().bullets > 0)
        {
            int numberOfPellets = 8;
            float spreadAngle = 20f;

            for (int i = 0; i < numberOfPellets; i++)
            {

                Quaternion rotation = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0);

                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);


                Vector3 spreadDirection = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0) * transform.forward;


                bullet.transform.forward = spreadDirection;


                bulletScript bulletScript = bullet.GetComponent<bulletScript>();
                bulletScript.SetDamage(damage);
                bulletScript.SetLifeTime(bulletTimer);
                

            }
            shotgun.GetComponent<gunINfo>().bullets--;

            yield return new WaitForSeconds(.5f);
        }

        StartCoroutine(reload());

    }


    IEnumerator reload()
    {

        if (manType.Contains("gun"))
        {
            GameObject baby = transform.Find("look at player")
    .GetComponentsInChildren<Transform>(true)
    .FirstOrDefault(t => t.name.Contains("gun")).gameObject;

            if (baby != null)
            {

                baby.GetComponent<animacao>().reloadplssss(baby.GetComponent<gunINfo>().amountReloaded);

                while (baby.GetComponent<animacao>().isReloading)
                {
                    yield return null;

                }
                canAttack = true;

            }




        }
        else
        {




            canAttack = false;
            yield return new WaitForSeconds(reloadTime);



            canAttack = true;
        }

    }

}
