using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAI : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Player's Transform here")]
    [SerializeField] private Transform _player;

    [Tooltip("Drag your GameManager GameObject here")]
    [SerializeField] private GameManager _gameManager;

    [Header("Chase Settings")]
    [Tooltip("Within this distance the monster will chase the player")]
    [SerializeField] private float _followDistance = 10f;

    [Header("Wander Settings")]
    [Tooltip("Radius around its current position to pick random waypoints")]
    [SerializeField] private float _wanderRadius = 20f;
    [Tooltip("Time (in seconds) between picking new random waypoints")]
    [SerializeField] private float _wanderTimer = 5f;

    private NavMeshAgent _agent;
    private float _timer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = _wanderTimer;
    }

    private void Update()
    {
        if (!_agent.isOnNavMesh) return;

        float dist = Vector3.Distance(transform.position, _player.position);
        if (dist <= _followDistance)
        {
            // chase the player
            _agent.SetDestination(_player.position);
        }
        else
        {
            // wander randomly
            _timer += Time.deltaTime;
            if (_timer >= _wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius);
                _agent.SetDestination(newPos);
                _timer = 0f;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDir = Random.insideUnitSphere * distance + origin;
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDir, out navHit, distance, NavMesh.AllAreas))
            return navHit.position;
        return origin; // fallback
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // stop moving
            _agent.isStopped = true;

            // invoke your GameManager’s EndGame
            _gameManager.EndGame();
        }
    }
}
