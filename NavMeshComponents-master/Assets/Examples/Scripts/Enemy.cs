using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public ThirdPersonCharacter enemyCharacter;
    public GameObject player;
    
    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        enemyAgent.SetDestination(player.transform.position);
        
        if (enemyAgent.remainingDistance > enemyAgent.stoppingDistance)
        {
            enemyCharacter.Move(enemyAgent.desiredVelocity, false, false);
        }
        else
        {
            enemyCharacter.Move(Vector3.zero, false, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            Debug.Log("Attacked Player");
        }
    }
}
