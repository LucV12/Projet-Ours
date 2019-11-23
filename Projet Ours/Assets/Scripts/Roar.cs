﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : MonoBehaviour
{

    float roarRange = 1f;
    float roarDelay = 2f;
    public LayerMask whatIsEnemies;
    float attackRange = 0.5f;
    Transform roarPos;
    GameObject nounours;
    public LayerMask whatIsBullets;

    private void Start()
    {
        nounours = GameObject.FindGameObjectWithTag("Player");
        roarPos = nounours.transform;
    }

    public void RoarActivable()  //Fonction du Rugissement
    {
        Collider2D[] bulletsToDestroy = Physics2D.OverlapCircleAll(roarPos.position, roarRange, whatIsBullets);
        for (int y = 0; y < bulletsToDestroy.Length; y++)
        {
            bulletsToDestroy[y].GetComponent<Projectile>().DestroyProjectile();
        }

        Collider2D[] enemyToPush = Physics2D.OverlapCircleAll(roarPos.position, roarRange, whatIsEnemies);
        for (int i = 0; i < enemyToPush.Length; i++)
        {
            StartCoroutine(enemyToPush[i].GetComponent<Enemy>().Repulsed());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(GetComponent<PlayerController>().transform.position, roarRange);
    }
}