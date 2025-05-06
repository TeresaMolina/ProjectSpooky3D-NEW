using UnityEngine;
using System.Collections;

public class GeneratorManager : MonoBehaviour
{
    private int totalGenerators = 3;
    private int activatedGenerators = 0;
    public Transform[] spawnPoints;
    public GameObject player;
    public ScreenFader screenFader;


    public GameObject RespawnPoint; // optional but we can add a door or light for the exit (choosing one of the doors)

    public void GeneratorActivated()
    {
        activatedGenerators++;
        Debug.Log($"Generator Activated! Total: {activatedGenerators} / {totalGenerators}");

        if (activatedGenerators < spawnPoints.Length)
        {
            if (activatedGenerators <= spawnPoints.Length)
            {
                TeleportPlayerTo(spawnPoints[activatedGenerators - 1]);
            }
        }
        if (activatedGenerators == totalGenerators)
        {
            Debug.Log("All Generators activated");
        }
    }

    private void TeleportPlayerTo(Transform target)
    {
        StartCoroutine(FadeAndTeleport(target));
    }

    private IEnumerator FadeAndTeleport(Transform target)
    {
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        player.transform.position = target.position;
        player.transform.rotation = target.rotation;

        yield return new WaitForSeconds(0.1f); // brief delay

        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());
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
