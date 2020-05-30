using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    [SerializeField] private float detectRange = 10, attackInterval = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] public VirtualController vCont;
    private float counter = 0;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SenseForPlayer();
        UpdateTimer();
    }

    private void SenseForPlayer()
    {
        if (Vector3.Distance(this.gameObject.transform.position, PlayerController.Instance.gameObject.transform.position) <= detectRange)
        {
            if (canAttack)
            {
                vCont.Attack(damage, PlayerController.Instance.vCont);
                canAttack = false;
            }
        }
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
