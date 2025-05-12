using UnityEngine;
using System.Collections;

public class GeneratorManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public CharacterController playerController;
    public ScreenFader screenFader;

    [Header("Teleport Targets")]
    public Transform spawnNearGen2;
    public Transform spawnNearGen3;
    public Transform exitSpawnPoint;

    private int activatedGenerators = 0;
    private const int totalGenerators = 3;

    public void GeneratorActivated()
    {
        activatedGenerators++;
        Debug.Log($"Generator Activated! Total: {activatedGenerators} / {totalGenerators}");

        if (activatedGenerators == 1)
        {
            TeleportTo(spawnNearGen2);
        }
        else if (activatedGenerators == 2)
        {
            TeleportTo(spawnNearGen3);
        }
        else if (activatedGenerators == 3)
        {
            TeleportTo(exitSpawnPoint);
            Debug.Log("All generators complete — teleporting to exit.");
        }
    }

    private void TeleportTo(Transform target)
    {
        StartCoroutine(FadeAndTeleport(target));
    }

    private IEnumerator FadeAndTeleport(Transform target)
    {
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        if (playerController == null)
            playerController = player.GetComponent<CharacterController>();

        if (playerController != null)
            playerController.enabled = false;

        player.transform.position = target.position;
        player.transform.rotation = target.rotation;

        yield return new WaitForSeconds(0.1f);

        if (playerController != null)
            playerController.enabled = true;

        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());
    }

}
