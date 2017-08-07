using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float attackRange;

    private NavMeshAgent playerAgent;
    Enum.PlayerState state;

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        state = Enum.PlayerState.Normal;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }

        if (state == Enum.PlayerState.Attack)
        {
            if(playerAgent.remainingDistance <= attackRange)
            {
                playerAgent.isStopped = true;
                Debug.Log("Attack");
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
                state = Enum.PlayerState.Normal;
            }

            playerAgent.destination = interactionInfo.point;
        }
    }
}
