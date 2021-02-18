using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;

    [SerializeField] Rigidbody[] ragDollRigidBodys;

    [SerializeField] GameObject _player;

    [SerializeField] float _distanceToAttack;

    bool _isDead;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _distanceToAttack && !_isDead)
        {
            _animator.SetBool("isMoving", true);
            _agent.SetDestination(_player.transform.position);
        }
    }

    public void ActivateRagDoll()
    {
        _isDead = true;
        _animator.enabled = false;
        _agent.enabled = false;
        foreach (Rigidbody rb in ragDollRigidBodys)
        {
            rb.gameObject.tag = "Untagged";
            rb.isKinematic = false;
        }
    }
}
