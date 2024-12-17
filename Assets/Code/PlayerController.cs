using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IDrivable
{
    
    NavMeshAgent agent;
    [SerializeField] Transform initialWaypoint; // the starting agent's destination


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        GameManager.OnGameEnd += EndAgent;
    }
    private void OnDisable()
    {
        GameManager.OnGameEnd -= EndAgent;
    }


    private void Start()
    {
        initialWaypoint.parent = null; // release the transform from the hierarchy
        agent.SetDestination(initialWaypoint.position);
    }
       

    public void SetNextWaypoint(Transform nextDestionation) // to set the next agent's destination
    {
        agent.SetDestination(nextDestionation.position);
    }

    public void StopAgent(bool value) // stop or play the agent
    {
        agent.isStopped = value;
    }

    void EndAgent() // stop the agent at the end of the game
    {
        agent.isStopped = true;
    }
        

   

}
