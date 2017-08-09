using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float attackRange;
    public GameObject enemy;
    public GameObject fireball;
    public float amountOfFireballs = 3;

    private NavMeshAgent playerAgent;
    private Enum.PlayerState state;
    private List<GameObject> fireballs;
    private bool attacking;

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        state = Enum.PlayerState.Normal;

        fireballs = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }

        if (state == Enum.PlayerState.Attack)
        {
            playerAgent.destination = enemy.transform.position;

            if (playerAgent.remainingDistance <= attackRange)
            {
                playerAgent.isStopped = true;
                Debug.Log("Attack");

                if (!attacking)
                {
                    StartCoroutine(FireAttack());
                }
            }
        }
    }

    private void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            if (interactionInfo.transform.gameObject.CompareTag("Enemy"))
            {
                state = Enum.PlayerState.Attack;
            }

            else
            {
                if(playerAgent.isStopped)
                {
                    playerAgent.isStopped = false;
                }

                state = Enum.PlayerState.Normal;
                playerAgent.destination = interactionInfo.point;
            }

        }
    }

    private IEnumerator FireAttack()
    {
        while(fireballs.Count != amountOfFireballs)
        {
            attacking = true;

            Vector3 spawnpoint = transform.position + (enemy.transform.position - transform.position) * (1f / (amountOfFireballs - fireballs.Count));
            GameObject createdFireball = Instantiate(fireball, spawnpoint, fireball.transform.rotation);
            fireballs.Add(createdFireball);

            yield return new WaitForSecondsRealtime(0.5f);
        }

        foreach(GameObject fireball in fireballs)
        {
            Destroy(fireball);
        }

        fireballs.Clear();

        attacking = false;

        yield return null;
    }
}
