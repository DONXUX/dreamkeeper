using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] float spawnInterval = 3f; // 목표물 생성 시간

    EnemySpawner[] spawners;

    float timer = 0f;
    void Start()
    {
        spawners = GetComponentsInChildren<EnemySpawner>(); // 자식 게임오브젝트가 가진 EnemySpawner 컴포넌트를 리스트 형태로 리턴
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(spawnInterval < timer)
        {
            var index = Random.Range(0, spawners.Length);
            spawners[index].Spawn();    // Spawner를 랜덤으로 선택해 Spawn
            timer = 0f;
        }
    }
}
