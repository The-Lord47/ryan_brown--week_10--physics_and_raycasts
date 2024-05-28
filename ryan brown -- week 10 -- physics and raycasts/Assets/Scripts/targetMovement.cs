using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMovement : MonoBehaviour
{

    Rigidbody _rb;
    Transform player;
    public float speed = 10;
    float waitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if (Time.time > waitTimer)
        {
            waitTimer = Time.time + 2;
            StartCoroutine(randomWalk());
        }
    }

    IEnumerator randomWalk()
    {
        _rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f) * 0.1f, Random.Range(-1f, 1f) * 0.5f).normalized * speed, ForceMode.Impulse);

        yield return new WaitForSeconds(1);

        _rb.velocity = new Vector3(0, 0, 0);
        _rb.angularVelocity = new Vector3(0, 0, 0);
    }
}
