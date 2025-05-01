using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    private int totalGenerators = 3;
    private int activatedGenerators = 0;

    public GameObject finalDoor; // optional but we can add a door or light for the exit (choosing one of the doors)

    public void GeneratorActivated()
    {
        activatedGenerators++;
        Debug.Log($"Generator Activated! Total: {activatedGenerators} / {totalGenerators}");

        if( activatedGenerators >= totalGenerators)
        {
            Debug.Log("All generators online");
        }
        if (finalDoor != null) 
        { 
            finalDoor.SetActive(true); //or enable light/door!
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
