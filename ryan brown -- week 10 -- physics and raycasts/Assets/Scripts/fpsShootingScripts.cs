using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsShootingScripts : MonoBehaviour
{
    //-------------------PUBLIC VARIABLES-------------------
    [Header("Inspector Variables")]
    public float gunDamage = 1;
    public float fireRate = 0.5f;
    public float range = 50f;
    public float hitForce = 15f;
    public Transform gunEnd;
    public float waitTime = 0.3f;

    //-------------------PRIVATE VARIABLES-------------------
    private Camera fpsCam;
    private AudioSource _as;
    private LineRenderer _lr;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        _lr = GetComponent<LineRenderer>();
        fpsCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(shootingEffects());

            //raycasting
            raycast();
        }
    }

    IEnumerator shootingEffects()
    {
        _as.Play();
        _lr.enabled = true;

        yield return new WaitForSeconds(waitTime);

        _lr.enabled = false;
    }

    public void raycast()
    {
        //gets the centre of the camera relative to the viewport
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        //creates a raycast hit data point called hit
        RaycastHit hit;

        _lr.SetPosition(0, gunEnd.position);

        //fires the ray from the ray origin towards the forward of the fpsCam inside a particular range
        if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
        {
            _lr.SetPosition(1, hit.point);

            floatingTarget enemy = hit.transform.GetComponentInChildren<floatingTarget>();
            if(enemy != null)
            {
                enemy.Damage(gunDamage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);
            }
        }
        else
        {
            _lr.SetPosition(1, fpsCam.transform.forward * range);
        }
    }
}
