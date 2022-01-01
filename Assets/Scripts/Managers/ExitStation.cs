using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStation : MonoBehaviour
{

    //game States
    private bool canPress;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPress)
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.INTER_PLAYER)
        {
            canPress = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.INTER_PLAYER)
        {
            canPress = false;
        }
    }
}

