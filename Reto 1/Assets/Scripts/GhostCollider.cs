using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class GhostCollider : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento del fantasma
    public float destinationTolerance = 0.1f; // Tolerancia para considerar que el agente ha llegado a su destino

    private NavMeshAgent agent;
    private bool isMovingRandomly = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        while (true)
        {
            if (isMovingRandomly)
            {
                // Mover el fantasma de forma aleatoria
                Vector3 randomDestination = GetRandomNavMeshPoint();
                agent.SetDestination(randomDestination);
            }
            else
            {
                // Detener el movimiento del fantasma
                agent.ResetPath();
            }

            // Esperar a que el agente alcance su destino actual
            while (isMovingRandomly && (agent.pathPending || agent.remainingDistance > destinationTolerance))
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
        else
        {
            // Si el fantasma colisiona con algún otro objeto, detener el movimiento aleatorio
            isMovingRandomly = false;
        }
    }
}


