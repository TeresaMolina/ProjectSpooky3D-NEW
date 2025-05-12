using UnityEngine;
using System.Collections;

public class FlashlightController : MonoBehaviour
{
    //public float freezeRange = 10f;  // how far the beam can affect the statue

    //[Tooltip("The spotlight component on this GameObject")]
    //[Tooltip("Key to toggle flashlight")]
    //[Tooltip("Chance per second to randomly flicker off")]
    //[Tooltip("How long the flashlight stays off when it flickers")]

    [Header("Flashlight Settings")]
    public Light flashlight;                        // Assign your Spotlight here
    public KeyCode toggleKey = KeyCode.F;
    public float freezeRange = 10f;

    [Header("Flicker Settings")]
    public float flickerChancePerSecond = 0.1f;
    public float flickerDuration = 5f;

    [Header("Raycast Layer Filter (optional)")]
    public LayerMask freezeLayers = ~0;             // Default: Everything

    private bool isOn = false;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light>();

        flashlight.enabled = isOn;
        StartCoroutine(RandomFlicker());
    }

    private void Update()
    {
        HandleToggle();

        if (isOn)
            PerformStatueFreezeCheck();
    }

    private void HandleToggle()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }

    private void PerformStatueFreezeCheck()
    {
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, freezeRange, freezeLayers))
        {
            Debug.DrawRay(ray.origin, ray.direction * freezeRange, Color.red);

            if (hit.collider.CompareTag("Statue"))
            {
                var ai = hit.collider.GetComponent<MonsterAI>() ?? hit.collider.GetComponentInParent<MonsterAI>();
                ai?.Freeze();
            }
        }
    }


    //void Update()
    //{
    //    if (Input.GetKeyDown(toggleKey))
    //    {
    //        isOn = !isOn;
    //        flashlight.enabled = isOn;
    //    }
    //    if (isOn)
    //    {
    //        Ray ray = new Ray(transform.position, transform.forward);
    //        RaycastHit hit;


    //    if (Physics.Raycast(ray, out hit, freezeRange, freezeLayers))

    //        {
    //            if (hit.collider.CompareTag("Statue"))
    //            {
    //                var ai = hit.collider.GetComponent<MonsterAI>();
    //                if (ai != null)
    //                {
    //                    ai.Freeze(); // we'll write this method next
    //                }
    //            }
    //        }
    //    }
    //}


    IEnumerator RandomFlicker()
    {
        while (true)
        {
            if (isOn && Random.value < flickerChancePerSecond * Time.deltaTime)
            {
                float endTime = Time.time + flickerDuration;
                while (Time.time < endTime)
                {
                    flashlight.enabled = !flashlight.enabled;
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
                }
                flashlight.enabled = true;
            }
            yield return null;
        }

    }
}