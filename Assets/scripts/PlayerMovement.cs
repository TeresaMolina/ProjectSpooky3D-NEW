using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float moveSpeed = 4f;
    public float turnSpeed = 200f;

    private CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!cc.enabled) return;

        // Rotate left/right
        float h = Input.GetAxis("Horizontal");  // A/D or ←/→
        transform.Rotate(0, h * turnSpeed * Time.deltaTime, 0);

        // Move forward/backward
        float v = Input.GetAxis("Vertical");    // W/S or ↑/↓
        Vector3 move = transform.forward * v;

        cc.SimpleMove(move * moveSpeed);
    }
}
