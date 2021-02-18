using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnHandler : MonoBehaviour
{
   public void HandleButtonClick(GameObject button)
    {
        switch(button.name)
        {
            case "Button_Restart":
                SceneManager.LoadScene(0);
                break;
                // ...
        }
    }
}
