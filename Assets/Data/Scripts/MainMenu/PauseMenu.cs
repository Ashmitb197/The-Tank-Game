using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool Paused;
    public GameObject PauseMenuCanvas;
    public GameObject OptionMenuCanvas;
    public GameObject MapCanvas;
    public GameObject HUD;


    void Awake()
    {
        // PauseMenuCanvas = this.transform.Find("PauseCanvas").gameObject;
        // OptionMenuCanvas = this.transform.Find("PauseOption").gameObject;
        // MapCanvas = this.transform.Find("MAPCanvas").gameObject;
        HUD = GameObject.Find("HUD");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        PauseMenuCanvas.SetActive(false);
        OptionMenuCanvas.SetActive(false);
        MapCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if (PauseMenuCanvas.activeSelf)
            {
                Play();
            }
            if(!Paused /*&& !PauseMenuCanvas.activeSelf && !OptionMenuCanvas.activeSelf*/)
            {
                Stop();
            }

            if(Paused && OptionMenuCanvas.activeSelf && !PauseMenuCanvas.activeSelf)
            {
                CloseOptionMenu();
            }
            
        }

        HUD.SetActive(!Paused);
        Cursor.visible = Paused;

    }


    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

     public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Game Restarted");
    }

    public void OpenOptionMenu()
    {
        PauseMenuCanvas.SetActive(false);
        OptionMenuCanvas.SetActive(true);
        Paused = true;
    }
    public void CloseOptionMenu()
    {
        PauseMenuCanvas.SetActive(true);
        OptionMenuCanvas.SetActive(false);
        Paused = true;
    }

    public void OpenMAP()
    {
        PauseMenuCanvas.SetActive(false);
        MapCanvas.SetActive(true);
        Paused = true;
    }
    public void CloseMap()
    {
        PauseMenuCanvas.SetActive(true);
        MapCanvas.SetActive(false);
        Paused = true;
    }
}
