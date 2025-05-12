using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private GameManager _gameManager;

    [Header("Chase Settings")]
    [SerializeField] private float _followDistance = 10f;

    [Header("Wander Settings")]
    [SerializeField] private float _wanderRadius = 20f;
    [SerializeField] private float _wanderTimer = 5f;

    [Header("Whisper Audio")]
    public AudioSource whisperSource;
    public AudioClip whisperClip;
    public float whisperRange = 8f;
    public float whisperFadeSpeed = 2f;

    private NavMeshAgent _agent;
    private float _timer;
    private bool _isFrozen = false;
    private float _freezeCooldown = 0.2f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = _wanderTimer;

        if (whisperSource != null)
        {
            whisperSource.clip = whisperClip;
            whisperSource.loop = true;
            whisperSource.playOnAwake = false;
            whisperSource.volume = 0f;
            whisperSource.Play();
        }
    }

    private void Update()
    {
        if (!_agent.isOnNavMesh) return;

        HandleWhispers();

        if (_isFrozen)
        {
            FreezeUpdate();
            return;
        }

        float dist = Vector3.Distance(transform.position, _player.position);

        if (dist <= _followDistance)
        {
            _agent.SetDestination(_player.position);
        }
        else
        {
            WanderUpdate();
        }
    }

    private void HandleWhispers()
    {
        if (whisperSource == null) return;

        float dist = Vector3.Distance(transform.position, _player.position);
        float targetVol = dist <= whisperRange ? 1f : 0f;

        whisperSource.volume = Mathf.MoveTowards(
            whisperSource.volume,
            targetVol,
            whisperFadeSpeed * Time.deltaTime
        );
    }

    public void Freeze()
    {
        _isFrozen = true;
        _freezeCooldown = 0.2f;
        _agent.isStopped = true;
    }

    private void FreezeUpdate()
    {
        _freezeCooldown -= Time.deltaTime;
        if (_freezeCooldown <= 0f)
        {
            _isFrozen = false;
            _agent.isStopped = false;
        }
    }

    private void WanderUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius);
            _agent.SetDestination(newPos);
            _timer = 0f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randDir = Random.insideUnitSphere * distance + origin;

        if (NavMesh.SamplePosition(randDir, out NavMeshHit navHit, distance, NavMesh.AllAreas))
            return navHit.position;

        return origin;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _agent.isStopped = true;
            _gameManager.EndGame();
        }
    }
}


//private bool isFrozen = false;
//private float freezeCooldown = 0.2f; // brief pause after being looked at



//[Tooltip("Drag your GameManager GameObject here")]
//[SerializeField] private GameManager _gameManager;


//private NavMeshAgent _agent;
//private float _timer;

//float dist = Vector3.Distance(transform.position, _player.position);
//float targetVolume = dist <= whisperRange ? 1f : 0f;
//whisperSource.volume = Mathf.MoveTowards(
//    whisperSource.volume,
//    targetVolume,
//    whisperFadeSpeed * Time.deltaTime

//if (isFrozen)
//{
//    freezeCooldown -= Time.deltaTime;
//    if (freezeCooldown <= 0)
//    {
//        isFrozen = false;
//    }
//    return; // stop all movement/logic while frozen
//}

//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("Player"))
//    {
//        // stop moving
//        _agent.isStopped = true;

//        // invoke your GameManager’s EndGame
//        _gameManager.EndGame();
//    }
//}

//public void Freeze()
//{
//    isFrozen = true;
//    freezeCooldown = 0.2f; // resets cooldown every time flashlight hits
//}