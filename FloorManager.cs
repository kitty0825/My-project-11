using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs;

    public void SpawnFloor()
    {
        int r=Random.Range(0,floorPrefabs.Length);

        GameObject floor  =Instantiate(floorPrefabs[r],transform);
        floor.transform.position = new Vector3(Random.Range(-5.5f, 2.9f), -6f, 0f);
    }
}
