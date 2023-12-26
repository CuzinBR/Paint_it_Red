using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEditor.PlayerSettings;

public class look : MonoBehaviour
{
    public GameObject Camera;

    public float Sensitivity = 100f;
    float verticalRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {
        float MouseX = UnityEngine.Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = UnityEngine.Input.GetAxis("Mouse Y") * Sensitivity;
        transform.Rotate(0, MouseX, 0);


        verticalRotation -= MouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        Camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
