using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pigCOM : MonoBehaviour
{
    Rigidbody _rb;
    float health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.centerOfMass = transform.GetChild(5).localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }*/


        if (collision.gameObject.CompareTag("CannonBall"))
        {
            health -= collision.transform.GetComponent<Rigidbody>().velocity.magnitude * 0.3f;
        }
        else
        {
            health -= _rb.velocity.magnitude * 4;
        }

        transform.GetChild(6).transform.GetChild(0).GetComponent<Slider>().value = health / 100f;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
