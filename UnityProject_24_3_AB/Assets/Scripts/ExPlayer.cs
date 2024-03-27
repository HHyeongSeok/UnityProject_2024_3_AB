using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExPlayer : MonoBehaviour
{
    private int health = 100;       //플레이어 체력 

    public void TakeDamage(int damage)
    {
        //플레이어의 체력 감소
        health -= damage;
        Debug.Log("플레이어 체력 : " + health);
        //체력이 0이하로 떨어졌을 때 사망처리
        if (health <= 0)
        {
            Die();       
        }
    }
    private void Die()
    {
        Debug.Log("플레이어 사망");
        //사망 로직 추가
    }
}
