using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopPlayer : MonoBehaviour
{
    public GameObject[] enemiesToKill;
    // Start is called before the first frame update
    void Start()
    {
        CollectEnemiesToKill();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(AreAllEnemiesNull())
        {
            Destroy(gameObject);
        }
    }
    private bool AreAllEnemiesNull()
    {
        
        foreach (GameObject enemy in enemiesToKill)
        {
            
            if (enemy != null)
            {
                return false;
            }
        }

        
        return true;
    }
    private void CollectEnemiesToKill()
    {
        // Get the parent GameObject
        Transform parentTransform = transform.parent;

        // Check if there is a parent
        if (parentTransform != null)
        {
            // Collect all children of the parent
            int childCount = parentTransform.childCount;
            List<GameObject> enemiesList = new List<GameObject>();

            for (int i = 0; i < childCount; i++)
            {
                Transform child = parentTransform.GetChild(i);

                // Exclude itself from the list
                if (child.gameObject != gameObject)
                {
                    enemiesList.Add(child.gameObject);
                }
            }

            // Assign the array
            enemiesToKill = enemiesList.ToArray();
        }
    }
}

