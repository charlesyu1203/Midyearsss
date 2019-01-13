using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class followPlayer : MonoBehaviour {

    [SerializeField] Transform playerPos;
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindWithTag("FPC").transform;
    }
    // Update is called once per frame
    void Update () {
        Vector3 target = playerPos.transform.position;
        navMeshAgent.SetDestination(target);
	}
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("FPC"))
        {
            SceneManager.LoadScene("Maze");
        }
    }
}
