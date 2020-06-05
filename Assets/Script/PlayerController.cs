using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask canClickLayer;
    [SerializeField] public VirtualController vCont;
    [SerializeField] private int hp = 10;
    [SerializeField] private HPbarUIScript hpBarUI;

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
    }
   
    private void PlayerClickedLeft()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, canClickLayer))
        {
            vCont.MoveTo(hitInfo.point);
        }
    }

    private void DamageTaken(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
        hpBarUI.updateHpBarUI(-damage);
        if (hp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(this);
    }
}
