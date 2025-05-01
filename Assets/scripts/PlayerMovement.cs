using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float moveSpeed = 4f;
    public float turnSpeed = 200f;

    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // read WASD / arrow keys
        float h = Input.GetAxis("Horizontal");  // A/D or ←/→
        float v = Input.GetAxis("Vertical");    // W/S or ↑/↓

        // build a movement vector relative to player forward
        Vector3 move = transform.forward * v;

        // actually move the controller
        cc.SimpleMove(move * moveSpeed);

        // rotate on the Y axis
        transform.Rotate(0, h * turnSpeed * Time.deltaTime, 0);
    }
}
