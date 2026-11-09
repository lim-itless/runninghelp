using UnityEngine;
using UnityEngine.Events;

public class EnemyStateController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Chase,
        Combat
    }

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    [SerializeField, Min(0f)] private float moveSpeed = 3f;
    [SerializeField, Min(0f)] private float attackRange = 2f;
    [SerializeField, Min(0f)] private float detectionRange = 8f;
    [SerializeField, Min(0.01f)] private float interval = 1.5f;

    [SerializeField] private UnityEvent onAttack = new UnityEvent();

    private State currentState;
    private float waitRemaining;

    private void Start()
    {
        if (player == null || animator == null)
        {
            Debug.LogError("Player와 Animator를 연결해주세요.", this);
            enabled = false;
            return;
        }

        EnterState(State.Idle);
    }

    private void Update()
    {
        if (currentState == State.Combat)
        {
            waitRemaining -= Time.deltaTime;

            if (waitRemaining <= 0f)
            {
                EnterState(ChooseState());
            }

            return;
        }

        State nextState = ChooseState();

        if (nextState != currentState)
        {
            EnterState(nextState);
        }

        if (currentState == State.Chase)
        {
            ChasePlayer();
        }
    }

    private State ChooseState()
    {
        float distance = DistanceToPlayer();

        if (distance <= attackRange)
        {
            return State.Combat;
        }

        if (distance <= detectionRange)
        {
            return State.Chase;
        }

        return State.Idle;
    }

    private void EnterState(State nextState)
    {
        currentState = nextState;
        Debug.Log($"[적 상태] {currentState}", this);

        switch (currentState)
        {
            case State.Idle:
                animator.CrossFadeInFixedTime("Base Layer.Idle", 0.1f);
                break;

            case State.Chase:
                animator.CrossFadeInFixedTime("Base Layer.Chase", 0.1f);
                break;

            case State.Combat:
                animator.CrossFadeInFixedTime("Base Layer.Combat", 0.1f, 0, 0f);
                waitRemaining = interval;
                Attack();
                break;
        }
    }

    private void Attack()
    {
        Debug.Log("[공격] Combat 진입 즉시 1회 공격!", this);
        onAttack.Invoke();
    }

    private void ChasePlayer()
    {
        Vector3 target = player.position;
        target.y = transform.position.y;

        Vector3 direction = target - transform.position;

        if (direction.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    private float DistanceToPlayer()
    {
        Vector3 difference = player.position - transform.position;
        difference.y = 0f;
        return difference.magnitude;
    }
}