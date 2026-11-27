using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction fireAction;
    public Vector2 moveInput;
    public float speed = 10.0f;
    public float XRange = 10.0f;
    public GameObject projectilePrefab;
    public Vector3 offset;
    public int poolSize = 20;
    private List<GameObject> projectiles = new List<GameObject>();

    void Start()
    {
        moveAction.Enable();
        fireAction.Enable();

        // Pool: se crean los proyectiles al inicio y quedan apagados
        for (int i = 0; i < poolSize; i++)
        {
            GameObject p = Instantiate(projectilePrefab);
            p.SetActive(false);
            projectiles.Add(p);
        }
    }

    void Update()
    {
        // Movimiento horizontal con el Input System
        moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * speed);

        // Limita al jugador dentro de XRange
        if (transform.position.x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        }

        // Disparo: tecla modificable
        if (fireAction.triggered)
        {
            foreach (GameObject p in projectiles)
            {
                if (!p.activeInHierarchy) // busca un proyectil libre
                {
                    p.transform.position = transform.position + offset;
                    p.transform.rotation = projectilePrefab.transform.rotation;
                    p.SetActive(true); // lo reutiliza
                    break;
                }
            }
        }
    }
}