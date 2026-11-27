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

        // Pool: se crean todos los animales al inicio y quedan apagados
        foreach (GameObject prefab in animalPrefabs)
        {
            for (int i = 0; i < animalsPerType; i++)
            {
                GameObject a = Instantiate(prefab);
                a.SetActive(false);
                animals.Add(a);
            }
        }

        // Aparece un animal cada spawnInterval segundos
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void Update()
    {
        // Permite aparecer un animal manualmente con una tecla
        if (spawnAction.triggered)
        {
            SpawnRandomAnimal();
        }
    }

    void SpawnRandomAnimal()
    {
        // Busca los animales apagados (libres para reutilizar)
        List<GameObject> libres = animals.FindAll(a => !a.activeInHierarchy);
        if (libres.Count == 0) return; // si están todos en uso, no aparece ninguno

        // Elige uno al azar, lo ubica en una X aleatoria y lo activa
        GameObject animal = libres[Random.Range(0, libres.Count)];
        animal.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        animal.SetActive(true);
    }
}