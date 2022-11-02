using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour {

    public int targetCount = 5;
    public float radius = 10;
    public GameObject enemyPrefab;

    System.Collections.IEnumerator SpawnTanks()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < targetCount)
            {
                GameObject enemy = GameObject.Instantiate(enemyPrefab);
                Vector2 c = Random.insideUnitCircle * radius;
                enemy.transform.position = new Vector3
                    (c.x
                    , 5
                    , c.y
                );
            }
            yield return new WaitForSeconds(1);
        }
    }
	void Start () {
        StartCoroutine(SpawnTanks());

    }
	void Update () {

	}
}