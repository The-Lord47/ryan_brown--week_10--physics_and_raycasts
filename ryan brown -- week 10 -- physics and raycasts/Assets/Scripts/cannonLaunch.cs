using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cannonLaunch : MonoBehaviour
{

    public GameObject cannonBall;
    public Transform cannonBallsParent;
    public float launchForce = 0f;
    public float forceIncreaseRate = 100f;
    public float turnRate = 10;
    public Slider cannonForceUI;
    public GameObject cannonForceUI_go;
    public GameObject verticalAngleAxis;
    public TMP_Text verticalAngleText;
    public GameObject horizontalAngleAxis;
    public TMP_Text horizontalAngleText;
    public TMP_Text pigsRemainingText;
    public ParticleSystem explosionFX;
    public AudioSource explosionSoundSource;
    public AudioClip explosionSFX;

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CannonBallLaunch();
        CannonMovement();
        CannonForceUI();
        otherUI();
    }

    void CannonBallLaunch()
    {
        if (Input.GetMouseButton(0))
        {
            launchForce = Mathf.Clamp(launchForce + forceIncreaseRate * Time.timeScale, 0, 1000);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameObject _cannonBall = Instantiate(cannonBall, transform.position, transform.rotation, cannonBallsParent);
            _cannonBall.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchForce, 0));
            Destroy(_cannonBall, 5f);
            launchForce = 0f;
            explosionFX.Play();
            explosionSoundSource.PlayOneShot(explosionSFX);
        }
    }

    void CannonMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.parent.transform.parent.transform.Rotate(0, horizontalInput * turnRate * Time.deltaTime, 0);
        transform.parent.transform.Rotate(-1 * verticalInput * turnRate * Time.deltaTime, 0, 0);
    }

    void CannonForceUI()
    {
        if (launchForce == 0f)
        {
            cannonForceUI_go.SetActive(false);
        }
        else
        {
            cannonForceUI_go.SetActive(true);
            cannonForceUI.value = launchForce / 1000f;
        }

        verticalAngleAxis.transform.localRotation = Quaternion.Euler(0, 0, transform.parent.transform.rotation.eulerAngles.x + 90);
        verticalAngleText.text = $"{Mathf.RoundToInt(transform.parent.transform.rotation.eulerAngles.x -270)}°";

        horizontalAngleAxis.transform.localRotation = Quaternion.Euler(0, 0, -(transform.parent.transform.parent.transform.rotation.eulerAngles.y) - 90);
        horizontalAngleText.text = $"{Mathf.RoundToInt(transform.parent.transform.parent.transform.rotation.eulerAngles.y - 180)}°";
    }

    void otherUI()
    {
        pigsRemainingText.text = $"Pigs Remaining: {GameObject.FindGameObjectsWithTag("Pig").Length}";
    }
}
