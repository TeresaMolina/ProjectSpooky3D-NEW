using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MonsterDamage : MonoBehaviour
{
    [Tooltip("Drag your GameManager GameObject here")]
    [SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // only react when the Player enters
        if (other.CompareTag("Player"))
        {
            _gameManager.EndGame();
        }
    }
}
