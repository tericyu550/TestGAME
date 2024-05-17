using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    private float second;
    [SerializeField] GameObject[] FloorPrefabs;
    private void Update()
    {
        second += Time.deltaTime;
        if (second >= 1.2f)
        {
            SpawnFloor();
            second = 0;
        }
    }
    public void SpawnFloor()
    {
        int r = Random.Range(0, FloorPrefabs.Length);
        GameObject floor= Instantiate(FloorPrefabs[r],transform);
        floor.transform.position = new Vector3(Random.Range(-3.8f, 3.8f), -6f, 0f);
    }
}
