using UnityEngine;

public class TeleportDoor : MonoBehaviour
{

    public Transform teleportTaregt; //dragging the TeleportTarget here
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTaregt.position;

            //other if we need to

            /*
             other.transform.rotation = teleportDestination.rotation
             */
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
