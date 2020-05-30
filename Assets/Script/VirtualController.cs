using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirtualController : MonoBehaviour
{
    private NavMeshAgent agent;

    public event System.Action<int> OnDamageTaken = delegate { };

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    public void Attack(int damage, VirtualController target)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        OnDamageTaken(damage);
    }
}
