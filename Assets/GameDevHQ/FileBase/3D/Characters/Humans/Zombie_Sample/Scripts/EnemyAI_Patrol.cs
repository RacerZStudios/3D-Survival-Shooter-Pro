using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Patrol : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent; 
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private float _health = 100f;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _agent.speed = _speed;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_agent.enabled == true && _target != null)
        {
            _agent.SetDestination(_target.position);
            if(_agent.transform.position.z > 2)
            {
                if(Vector3.Distance(transform.position, _agent.transform.position) > 3)
                {
                    Vector3.MoveTowards(_agent.transform.position, _target.transform.position, 5); 
                }
            }
            return; 
        }
    }

    public void Damage(int amount)
    {
        _health -= amount;
        _agent.speed = 0;
        _anim.SetTrigger("Hit");

        if (_health < 1)
        {
            _agent.speed = 0;
            _anim.SetTrigger("Dead");
            _agent.enabled = false;
        }
        else
        {
            StartCoroutine(HitRoutine());
        }
    }

    IEnumerator HitRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        _agent.speed = 1.5f;
    }
}
