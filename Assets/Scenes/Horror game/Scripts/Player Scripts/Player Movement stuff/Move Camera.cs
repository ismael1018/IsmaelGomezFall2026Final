using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    void Start()
    {

    }

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}