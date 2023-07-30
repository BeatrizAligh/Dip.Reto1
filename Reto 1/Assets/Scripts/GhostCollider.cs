using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class GhostCollider : MonoBehaviour
{
    public Transform target; // El transform del jugador a seguir
    public float chaseDistance = 10f; // Distancia máxima para comenzar a perseguir al jugador
    public float moveSpeed = 2f; // Velocidad de movimiento del fantasma
    public float destinationTolerance = 0.1f; // Tolerancia para considerar que el agente ha llegado a su destino

    private NavMeshAgent agent;
    public bool isChasing; // Variable para controlar si el fantasma está persiguiendo al jugador

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isChasing = false;
        StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        while (true)
        {
            // Verificar si el jugador está dentro del rango de persecución
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= chaseDistance)
            {
                // Comenzar a perseguir al jugador
                isChasing = true;
                agent.SetDestination(target.position);
            }
            else
            {
                // Detener la persecución y moverse de forma aleatoria
                isChasing = false;
                Vector3 randomDestination = GetRandomNavMeshPoint();
                agent.SetDestination(randomDestination);
            }

            // Esperar a que el agente alcance su destino actual
            while (agent.pathPending || agent.remainingDistance > destinationTolerance)
            {
                yield return null;
            }
        }
    }

    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        NavMeshHit hit;

        // Obtener un punto aleatorio en la malla de navegación
        if (NavMesh.SamplePosition(Random.insideUnitSphere * 10f + transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            randomPoint = hit.position;
        }

        return randomPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Si el fantasma colisiona con el jugador, cambiar a la escena de perder
            SceneManager.LoadScene("Lose");
        }
    }

}

