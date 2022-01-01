using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //Cached References
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnLocation;

    //first method called
    private void Awake()
    {
        SpawnCharacter(GetRandomPosition());   
    }

    //Get a random transform for the spawnlocations and return it
    private Transform GetRandomPosition()
    {
        int randomlocation = Random.Range(0, spawnLocation.Length);

        return spawnLocation[randomlocation];
    }

    //spawn the player at the desired spawnloacation
    private void SpawnCharacter(Transform pos)
    {
        Instantiate(player, pos.transform.position, Quaternion.identity);
    }

}
