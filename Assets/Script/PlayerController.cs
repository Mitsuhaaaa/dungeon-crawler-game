using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    Idle = 0,
    Walking,
    Chasing,
    Attacking,
    Casting,

    Undefined = 422,
}


public class PlayerController : MonoBehaviour
{
    [SerializeField] public VirtualController vCont;
    [SerializeField] private int hp = 10;
    [SerializeField] private HPbarUIScript hpBarUI;
    [SerializeField] private float attackInterval = 1, attackRange = 2;
    [SerializeField] private int damage = 2;

    private VirtualController targetVCont;
    private PlayerState state;
    private float counter = 0;
    private bool canAttack = true;

    #region Singleton
    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        if (_instance == this)
        {
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private void Start()
    {
        InputSystem.OnMouseLeftClickHold += PlayerClickedLeft;
        vCont.OnDamageTaken += DamageTaken;
        state = PlayerState.Idle;
    }

    private void Update()
    {
        UpdateState();
        UpdateTimer();
    }

    private void UpdateState()
    {
        switch (state)
        {
            case PlayerState.Chasing:
                Chase();
                break;

            case PlayerState.Attacking:
                Attack();
                break;

            default:
                break;
        }
    }

    private void Attack()
    {
        vCont.Attack(damage, targetVCont);
        canAttack = false;
    }

    private void Chase()
    {
        if (Vector3.Distance(vCont.Position, targetVCont.Position) <= attackRange)
        {
            if (canAttack)
            {
                Attack();
            }
        }
    }

    private void PlayerClickedLeft()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {

            if (hitInfo.collider.tag == "Enemy")
            {
                targetVCont = hitInfo.collider.GetComponent<VirtualController>();
                state = PlayerState.Chasing;
            }
            else
            {
                state = PlayerState.Walking;
                targetVCont = null;
            }
            vCont.MoveTo(hitInfo.point);
        }
    }

    private void DamageTaken(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
           // Debug.Log(hp);
            hpBarUI.updateHpBarUI(-damage);
            if (hp <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        this.gameObject.SetActive(false);
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
}
