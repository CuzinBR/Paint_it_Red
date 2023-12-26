using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class bloodSplatter : MonoBehaviour
{
    public GameObject blood;
    public ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(blood, other.transform);
    }
}
