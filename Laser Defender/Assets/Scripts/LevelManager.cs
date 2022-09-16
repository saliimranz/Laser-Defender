using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void LoadLevel(string name)
    {
        Debug.Log("Load level is called for: " + name);
        Application.LoadLevel(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit the game ");
        Application.Quit();
    }

    public void Return(string name)
    {
        Debug.Log("Return To " + name);
        Application.LoadLevel(name);
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void BrickDestroyed()
    {   
            LoadNextLevel();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
