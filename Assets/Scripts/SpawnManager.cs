using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int widht, height;
    public GameObject[] prefabs;
    public Vector3 spawnPos;

    public Transform parent;
    
    
    void Start()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < widht; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int index = Random.Range(0, 3);
                spawnPos = new Vector3(transform.position.x + i, .6f, transform.position.z + j);
                Instantiate(prefabs[index], spawnPos, Quaternion.identity, parent.transform);
                
            }
        }
    }

    
}
