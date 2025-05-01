using UnityEngine;
using System.Collections;

public class FlashlightController : MonoBehaviour
{
    [Tooltip("The spotlight component on this GameObject")]
    public Light flashlight;
    [Tooltip("Key to toggle flashlight")]
    public KeyCode toggleKey = KeyCode.F;
    [Tooltip("Chance per second to randomly flicker off")]
    public float flickerChancePerSecond = 0.1f;
    [Tooltip("How long the flashlight stays off when it flickers")]
    public float flickerDuration = 5f;

    bool isOn = false;

    void Start()
    {
        if (flashlight == null) flashlight = GetComponent<Light>();
        flashlight.enabled = isOn;
        StartCoroutine(RandomFlicker());
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }

    IEnumerator RandomFlicker()
    {
        while (true)
        {
            if (isOn && Random.value < flickerChancePerSecond * Time.deltaTime)
            {
                // flicker off
                flashlight.enabled = false;
                yield return new WaitForSeconds(flickerDuration);
                // restore if still toggled on
                if (isOn) flashlight.enabled = true;
            }
            yield return null;
        }
    }
}
