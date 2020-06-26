using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float spawnInterval = 3.0f; // 목표물 생성 시간

    EnemySpawner[] spawners;

    float timer = 0f;
    void Start()
    {
        spawners = GetComponentsInChildren<EnemySpawner>(); // 자식 게임오브젝트가 가진 EnemySpawner 컴포넌트를 리스트 형태로 리턴
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if(spawnInterval < timer)
        {
            var index = Random.Range(0, spawners.Length);
            spawners[index].Spawn();    // Spawner를 랜덤으로 선택해 Spawn
            timer = 0f;
            if(spawnInterval >= 1.0f)
                spawnInterval -= 0.1f;
        }
    }
}
