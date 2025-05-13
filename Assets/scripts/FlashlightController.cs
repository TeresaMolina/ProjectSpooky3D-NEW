using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("Settings")]
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    public float freezeRange = 25f;
    public LayerMask freezeLayers; // should include "Monster"

    private bool isOn = false;
    private GameObject currentlyFrozenStatue;

    void Start()
    {
        if (flashlight == null) flashlight = GetComponent<Light>();
        flashlight.enabled = isOn;
        // Flickering removed for now
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }

        if (isOn)
        {
            PerformStatueFreezeCheck();
        }
        else
        {
            UnfreezeAllStatues(); // Flashlight is off — allow movement
        }
    }


    void PerformStatueFreezeCheck()
    {
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * freezeRange, Color.yellow, 0.1f);

        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, freezeRange, freezeLayers))
        {
            GameObject root = hit.collider.transform.root.gameObject;

            if (root.CompareTag("Statue"))
            {
                var ai = root.GetComponent<MonsterAI>();
                if (ai != null)
                {
                    // 👁️ Freeze it
                    ai.SetFrozen(true);
                    currentlyFrozenStatue = root;
                    return; // Skip unfreezing below
                }
            }
        }

        // ❌ Not looking at any statue → Unfreeze all
        UnfreezeAllStatues();
    }

    void UnfreezeAllStatues()
    {
        var statues = GameObject.FindGameObjectsWithTag("Statue");
        foreach (var statue in statues)
        {
            var ai = statue.GetComponent<MonsterAI>();
            if (ai != null)
                ai.SetFrozen(false);
        }

        currentlyFrozenStatue = null;
    }

}




//IEnumerator RandomFlicker()
//{
//    while (true)
//    {
//        if (isOn && Random.value < flickerChancePerSecond * Time.deltaTime)
//        {
//            flashlight.enabled = false;
//            yield return new WaitForSeconds(flickerDuration);
//            if (isOn) flashlight.enabled = true;
//        }
//        yield return null;
//    }
//}

























//using UnityEngine;
//using System.Collections;

//public class FlashlightController : MonoBehaviour
//{
//    [Header("Flashlight Settings")]
//    public Light flashlight;
//    public KeyCode toggleKey = KeyCode.F;
//    public float freezeRange = 10f;

//    [Header("Flicker Settings")]
//    public float flickerChancePerSecond = 0.1f;
//    public float flickerDuration = 5f;

//    [Header("Raycast Layer Filter")]
//    public LayerMask freezeLayers = ~0;

//    private bool isOn = false;


//    void Start()
//    {
//        flashlight.transform.localPosition = new Vector3(0, 0, 0.5f); //temp
//        if (flashlight == null)
//            flashlight = GetComponent<Light>();

//        flashlight.enabled = isOn;
//        StartCoroutine(RandomFlicker());
//    }

//    void Update()
//    {
//        HandleToggle();

//        if (isOn)
//            PerformStatueFreezeCheck();
//        else
//            UnfreezeAllStatues(); // flashlight is off, release them
//    }

//    private void HandleToggle()
//    {
//        if (Input.GetKeyDown(toggleKey))
//        {
//            isOn = !isOn;
//            flashlight.enabled = isOn;
//        }
//    }

//    private void PerformStatueFreezeCheck()
//    {


//        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
//        Debug.DrawRay(ray.origin, ray.direction * freezeRange, Color.red, 0.5f);
//        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, freezeRange, ~0))
//        {
//            Debug.Log($"🎯 Sphere hit: {hit.collider.name}");
//        }
//        else
//        {
//            Debug.Log("❌ Still missed. Even SphereCast missed.");
//        }


//    }

//    private void UnfreezeAllStatues()
//    {
//        foreach (var statue in GameObject.FindGameObjectsWithTag("Statue"))
//        {
//            var ai = statue.GetComponent<MonsterAI>();
//            if (ai != null)
//                ai.SetFrozen(false);
//        }
//    }

//    private IEnumerator RandomFlicker()
//    {
//        while (true)
//        {
//            if (isOn && Random.value < flickerChancePerSecond * Time.deltaTime)
//            {
//                float endTime = Time.time + flickerDuration;
//                while (Time.time < endTime)
//                {
//                    flashlight.enabled = !flashlight.enabled;
//                    yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
//                }
//                flashlight.enabled = true;
//            }
//            yield return null;
//        }
//    }
//}

































////using UnityEngine;
////using System.Collections;

////public class FlashlightController : MonoBehaviour
////{
////    //public float freezeRange = 10f;  // how far the beam can affect the statue

////    //[Tooltip("The spotlight component on this GameObject")]
////    //[Tooltip("Key to toggle flashlight")]
////    //[Tooltip("Chance per second to randomly flicker off")]
////    //[Tooltip("How long the flashlight stays off when it flickers")]

////    [Header("Flashlight Settings")]
////    public Light flashlight;                        // Assign your Spotlight here
////    public KeyCode toggleKey = KeyCode.F;
////    public float freezeRange = 10f;

////    [Header("Flicker Settings")]
////    public float flickerChancePerSecond = 0.1f;
////    public float flickerDuration = 5f;

////    [Header("Raycast Layer Filter (optional)")]
////    public LayerMask freezeLayers = ~0;             // Default: Everything

////    private bool isOn = false;

////    void Start()
////    {
////        if (flashlight == null)
////            flashlight = GetComponent<Light>();

////        flashlight.enabled = isOn;
////        StartCoroutine(RandomFlicker());
////    }

////    private void Update()
////    {
////        HandleToggle();

////        if (isOn)
////            PerformStatueFreezeCheck();
////    }

////    private void HandleToggle()
////    {
////        if (Input.GetKeyDown(toggleKey))
////        {
////            isOn = !isOn;
////            flashlight.enabled = isOn;
////        }
////    }

////    private void PerformStatueFreezeCheck()
////    {
////        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

////        //Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
////        if (Physics.Raycast(ray, out RaycastHit hit, freezeRange, freezeLayers))
////        {
////            Debug.DrawRay(ray.origin, ray.direction * freezeRange, Color.red);
////            Debug.Log($"[Flashlight] Ray hit: {hit.collider.name} on layer {LayerMask.LayerToName(hit.collider.gameObject.layer)}");


////            if (hit.collider.CompareTag("Statue"))
////            {
////                Debug.Log("[Flashlight] Statue detected — freezing!");

////                var ai = hit.collider.GetComponent<MonsterAI>() ?? hit.collider.GetComponentInParent<MonsterAI>();
////                if (ai != null)
////                {
////                    ai.SetFrozen(true);
////                }

////            }
////        }
////        else
////        {
////            // No hit or not a statue — unfreeze all statues in scene
////            foreach (var statue in GameObject.FindGameObjectsWithTag("Statue"))
////            {
////                var ai = statue.GetComponent<MonsterAI>();
////                if (ai != null)
////                    ai.SetFrozen(false);
////            }
////        }



////    }




////    IEnumerator RandomFlicker()
////    {
////        while (true)
////        {
////            if (isOn && Random.value < flickerChancePerSecond * Time.deltaTime)
////            {
////                float endTime = Time.time + flickerDuration;
////                while (Time.time < endTime)
////                {
////                    flashlight.enabled = !flashlight.enabled;
////                    yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
////                }
////                flashlight.enabled = true;
////            }
////            yield return null;
////        }

////    }
////}


////        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

////        ////Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
////        //if (Physics.Raycast(ray, out RaycastHit hit, freezeRange, freezeLayers))
////        //{
////        //    Debug.DrawRay(ray.origin, ray.direction * freezeRange, Color.red);
////        //    Debug.Log($"[Flashlight] Ray hit: {hit.collider.name} on layer {LayerMask.LayerToName(hit.collider.gameObject.layer)}");


////        //    if (hit.collider.CompareTag("Statue"))
////        //    {
////        //        Debug.Log("[Flashlight] Statue detected — freezing!");

////        //        var ai = hit.collider.GetComponent<MonsterAI>() ?? hit.collider.GetComponentInParent<MonsterAI>();
////        //        ai?.Freeze();
////        //    }
////        //}
////  //void Update()
////    //{
////    //    if (Input.GetKeyDown(toggleKey))
////    //    {
////    //        isOn = !isOn;
////    //        flashlight.enabled = isOn;
////    //    }
////    //    if (isOn)
////    //    {
////    //        Ray ray = new Ray(transform.position, transform.forward);
////    //        RaycastHit hit;


////    //    if (Physics.Raycast(ray, out hit, freezeRange, freezeLayers))

////    //        {
////    //            if (hit.collider.CompareTag("Statue"))
////    //            {
////    //                var ai = hit.collider.GetComponent<MonsterAI>();
////    //                if (ai != null)
////    //                {
////    //                    ai.Freeze(); // we'll write this method next
////    //                }
////    //            }
////    //        }
////    //    }
////    //}
