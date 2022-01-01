using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //first function when the game starts
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
