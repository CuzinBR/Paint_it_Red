using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    
    Image image;
    public int thing =1 ;

    public Sprite[] tuturial;
    
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        


        
    }

    // Update is called once per frame
    void Update()
    {
        if (thing < tuturial.Length)
        {
            image.sprite = tuturial[thing];
        }
        if(thing >= tuturial.Length)
        {
            gameObject.SetActive(false);
        }
    }
    public void dothething()
    {
        thing++;
        

    }
    public void dotheothathing()
    {
        gameObject.SetActive(true);
        thing = 0;
    }
}
