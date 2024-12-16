using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IDrivable
{
    
    NavMeshAgent agent;
    [SerializeField] Transform initialWaypoint; // destionazione dell'agent allo start


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
        initialWaypoint.parent = null; // rilascia il transform iniziale dalla hierarchy
        agent.SetDestination(initialWaypoint.position);
    }
       

    public void SetNextWaypoint(Transform nextDestionation) // per settare il prossimo transform di destinazione
    {
        agent.SetDestination(nextDestionation.position);
    }

    public void StopAgent(bool value) // per fermare o rilasciare l'agent
    {
        agent.isStopped = value;
    }

    void EndAgent()
    {
        agent.isStopped = true;
        Debug.Log("AGENTE FERMATO");
    }

   

}
