using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunINfo : MonoBehaviour
{
    public int bullets;
    public int maxbullets;
    public float seconds;
    public float amountReloaded;
    // Start is called before the first frame update
    void Start()
    {
        maxbullets = bullets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
