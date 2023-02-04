using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    private Vector3 target;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        /*agent.updateRotation = false;
        agent.updateUpAxis = false;*/
    }

    // Update is called once per frame
    void Update()
    {
        GoToPlayer();
    }

    void GoToPlayer()
    {
        agent.SetDestination(target);
    }
}
