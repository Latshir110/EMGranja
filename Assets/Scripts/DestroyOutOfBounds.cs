using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float topBound = 30;
    public float lowerBound = -10;

    void Update()
    {
        if (transform.position.z > topBound || transform.position.z < lowerBound)
        {
            gameObject.SetActive(false);
        }
    }
}