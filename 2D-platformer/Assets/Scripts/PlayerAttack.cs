using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float castCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        playerMovement= GetComponent<PMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownTimer < castCooldown && playerMovement.canAttack())
        {
            Cast();
        }

        if (Input.GetMouseButtonDown(0) && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer = Time.deltaTime;

    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    private void Cast()
    {
        anim.SetTrigger("cast");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int FindFireball()
    {
        for (int i = 0; i<fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy) return i;
        }
        return 0;
    }

}
