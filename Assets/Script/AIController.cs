using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    Idle = 0,
    Chasing,
    Attacking,

    Undefined = 422,
}

public class AIController : MonoBehaviour
{

    [SerializeField] private float detectRange = 10, attackInterval = 1, attackRange = 2;
    [SerializeField] private int damage = 1, hp = 5;
    [SerializeField] public VirtualController vCont;
    [SerializeField] private HPbarUIScript hpBarUI;

    private float counter = 0;
    private bool canAttack = true;
    private AIState state = AIState.Idle;

    private VirtualController playerVCont;

    void Start()
    {
        vCont.OnDamageTaken += DamageTaken;
        playerVCont = PlayerController.Instance.vCont;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateTimer();
    }

    private void UpdateState()
    {
        SenseForPlayer();
        switch (state)
        {
            case AIState.Idle:
                break;
            case AIState.Chasing:
                ChasePlayer();
                break;

            default:
                break;
        }
    }

    private void SenseForPlayer()
    {
        if (Vector3.Distance(vCont.Position, playerVCont.Position) <= detectRange)
        {
            state = AIState.Chasing;
        }
        else
        {
            state = AIState.Idle;
        }
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(vCont.Position, playerVCont.Position) >= attackRange)
        {
            vCont.MoveTo(playerVCont.Position);
        }
        else
        {
            if (canAttack)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        vCont.Attack(damage, playerVCont);
        canAttack = false;
    }

    private void UpdateTimer()
    {
        if (!canAttack)
        {
            counter += Time.deltaTime;

            if (counter >= attackInterval)
            {
                canAttack = true;
                counter = 0;
            }
        }
    }

    private void DamageTaken(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            Debug.Log(hp);
            hpBarUI.updateHpBarUI(-damage);
            if (hp <= 0)
            {
                Destroy(this);
                //GameOver();
            }
        }
    }

}
