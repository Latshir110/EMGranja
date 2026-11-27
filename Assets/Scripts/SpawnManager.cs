using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public InputAction spawnAction;

    public float spawnRangeX = 20;
    public float spawnPosZ = 20;
    public float startDelay = 2;
    private float spawnInterval = 1.5f;
    public int animalsPerType = 5;
    private List<GameObject> animals = new List<GameObject>();

    void Start()
    {
        spawnAction.Enable();

        foreach (GameObject prefab in animalPrefabs)
        {
            for (int i = 0; i < animalsPerType; i++)
            {
                GameObject a = Instantiate(prefab);
                a.SetActive(false);
                animals.Add(a);
            }
        }

        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void Update()
    {
        if (spawnAction.triggered)
        {
            SpawnRandomAnimal();
        }
    }

    void SpawnRandomAnimal()
    {
        List<GameObject> libres = animals.FindAll(a => !a.activeInHierarchy);
        if (libres.Count == 0) return;

        GameObject animal = libres[Random.Range(0, libres.Count)];
        animal.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        animal.SetActive(true);
    }
}