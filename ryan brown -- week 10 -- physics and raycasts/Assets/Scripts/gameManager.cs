using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    int enemyCount;
    public GameObject trainingDroid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<floatingTarget>().Length;

        if (enemyCount == 0)
        {
            Instantiate(trainingDroid, new Vector3(Random.Range(-9f, 9f), 2f, Random.Range(-9f, 9f)), Quaternion.Euler(0,0,0), transform);
        }
    }
}
