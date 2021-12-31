using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == Tags.INTER_PLAYER)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentScene + 1);
            }

        }
    }
}
