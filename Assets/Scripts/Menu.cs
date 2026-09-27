using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{

    public GameObject container;

    void Start()
    {
        container.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    { 
        if (Keyboard.current != null)
        {
           if (Keyboard.current.escapeKey.isPressed)
            {
                container.SetActive(true);
                Time.timeScale = 0;
            } 
        }
    }

    public void resumeButton ()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

      public void restartButton ()
    {
        Time.timeScale = 1;
        Player.Restart();
    }
}
