using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Circle;
    private Vector3 difference;
    private Vector3 newVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newVector = transform.position;
        difference.x = transform.position.x - Circle.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        newVector.x = Circle.transform.position.x + difference.x;
        transform.position = newVector;
    }
}
