using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public float helth;
    
    public float iFrames;
    public float current;
    public progressbar bar;
    public Text healthtext;
    public GameObject ded;

    // Start is called before the first frame update
    void Start()
    {
       
        SceneManager.sceneLoaded += OnSceneLoaded;

        
        healthtext = GameObject.Find("healthText").GetComponent<Text>();
        current = helth;
        ded = GameObject.Find("ded");
        
        bar = GameObject.Find("healthbar").GetComponent<progressbar>();
        ded.SetActive(false);
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        helth = 100;
        ded = GameObject.Find("ded");
        healthtext = GameObject.Find("healthText").GetComponent<Text>();
        current = helth;
        bar = GameObject.Find("healthbar").GetComponent<progressbar>();
        ded.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(current > 100)
        {
            current = 100;
        }


        bar.current = current;
        healthtext.text = Mathf.Round(current).ToString() + "/100";

        if (current < helth)
        {
            helth = current;
        }

        if (helth < 1)
        {
            ded.SetActive(true);
            gameObject.GetComponent<movement>().revived = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }

   
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
