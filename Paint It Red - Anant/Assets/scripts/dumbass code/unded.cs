using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class unded : MonoBehaviour
{

    int currentScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        
    }
    public void Tryagain()
    {
        
        


        SceneManager.LoadScene(currentScene);
        
        
    }
    public void Sad()
    {
        SceneManager.LoadScene(0);
        
    }
    
}

