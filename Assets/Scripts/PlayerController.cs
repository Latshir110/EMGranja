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

        for (int i = 0; i < poolSize; i++)
        {
            GameObject p = Instantiate(projectilePrefab);
            p.SetActive(false);
            projectiles.Add(p);
        }
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * speed);

        if (transform.position.x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        }

        if (fireAction.triggered)
        {
            foreach (GameObject p in projectiles)
            {
                if (!p.activeInHierarchy)
                {
                    p.transform.position = transform.position + offset;
                    p.transform.rotation = projectilePrefab.transform.rotation;
                    p.SetActive(true);
                    break;
                }
            }
        }
    }
}