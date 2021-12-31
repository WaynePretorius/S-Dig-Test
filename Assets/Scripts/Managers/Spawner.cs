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

    private Transform GetRandomPosition()
    {
        int randomlocation = Random.Range(0, spawnLocation.Length);

        return spawnLocation[randomlocation];
    }

    private void SpawnCharacter(Transform pos)
    {
        Instantiate(player, pos.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnCharacter(GetRandomPosition());
        }
    }
}
