using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public bool isChasing = false;
    public AudioClip chaseVoice;

    Transform player;
    NavMeshAgent agent;
    AudioSource audioSource;
    bool hasSpoken = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!isChasing) return;

        if (!hasSpoken)
        {
            hasSpoken = true;
            Invoke(nameof(Speak), Random.Range(0f, 1.5f));
        }

        agent.SetDestination(player.position);
    }

    void Speak()
    {
        if (chaseVoice != null && audioSource != null)
            audioSource.PlayOneShot(chaseVoice);
    }
}
