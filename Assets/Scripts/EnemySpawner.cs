using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab; // 클론으로 생성할 목표물의 프리팹

    Enemy enemy; // 현재 Spawner가 보유한 목표물

    public void Spawn()
    { 
        enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    //public void Spawn()
    //{
    //    if (enemy == null)
    //    {
    //        // enemy 오브젝트를 가지고 있지않으면 스폰
    //        enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
    //    }
    //}
}
