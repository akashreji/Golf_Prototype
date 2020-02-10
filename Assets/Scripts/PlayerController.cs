using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    SceneManager sceneHandler;
    int i, enemyCount = 2, heartCount = 2;
    //float speed;
    public Camera camera;
    public GameObject shieldInBall;
    public GameObject block;
    public GameObject bridge;
    public Transform launcherCoordinates;
    public GameObject[] hearts;

    public GameObject gameOverText;
    public GameObject levelIndicator;
    public GameObject instruction1;
    public GameObject instruction2;
    public GameObject shieldInstruction;

    public static bool hasTriggeredEnemy;
    public bool shieldActive;
    public bool ballLoadedUp;
    GameObject[] enemies;


    public int sliderCount;
    public Slider powerBarSlider;
    public Camera mainCamera;
    public float speed;
    public float force;
    public float speedNew;
    Vector3 pointToLook;
    Vector3 direction;
    Vector3 point;
    Quaternion targetRotation;
    bool ballSelected;

    int y;

    // level 1 instructions 
    public GameObject level1;
    public GameObject howto;
    public GameObject howto2;

    //Vector3 pointToLook;
    Vector3 finalBlockLoc;

    Rigidbody rb;

    void Start()
    {
         y = SceneManager.GetActiveScene().buildIndex;
        switch (y)
        {
            case 0:
                howto.SetActive(false);
                howto2.SetActive(false);
                StartCoroutine("InstructionsQueue", "level");
                CamFollow.gameStarted = true;
                break;
            case 1:
                shieldInBall.SetActive(false);
                gameOverText.SetActive(false);
                instruction1.SetActive(false);
                instruction2.SetActive(false);
                shieldInstruction.SetActive(false);
                StartCoroutine("LevelStart");
                CamFollow.gameStarted = true;
                shieldActive = false;
                hasTriggeredEnemy = false;
                ballLoadedUp = false;
                if (enemies == null)
                    enemies = GameObject.FindGameObjectsWithTag("enemy");
                rb = GetComponent<Rigidbody>();
                finalBlockLoc = new Vector3(-7.73f, -1.66f, 21.93f);

                speed = 4;
                for (i = 0; i < enemyCount; i++)
                {
                    enemies[i].GetComponent<Animator>().SetBool("Triggered", true);
                }
                break;
        }
        print(y);
        
       
    }

    private void OnCollisionEnter(Collision objectCollided)
    {
        if (objectCollided.gameObject.CompareTag("Bullet") && !shieldActive)
        {
            hearts[--heartCount].SetActive(false);
            if (heartCount == 0)
                StartCoroutine("GameOver");
        }
        if (objectCollided.gameObject.CompareTag("Launcher"))
        {
            //this.gameObject.transform.position = launcherCoordinates.transform.position;
            //ballLoadedUp = true;
        }
        if (objectCollided.gameObject.CompareTag("Flag"))
        {
            if(y==0)
            SceneManager.LoadScene(1);
            StartCoroutine("GameOverFinal");

        }
    }

    IEnumerator GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
       // Time.timeScale = 0;
        SceneManager.LoadScene(1);
    }
    IEnumerator GameOverFinal()
    {
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
       Time.timeScale = 0;
       // SceneManager.LoadScene(1);
    }

    IEnumerator LevelStart()
    {
        levelIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        levelIndicator.gameObject.SetActive(false);
        shieldInstruction.SetActive(true);
        yield return new WaitForSeconds(2f);
        shieldInstruction.SetActive(false);
    }
    private void OnTriggerEnter(Collider objectTriggered)
    {
        if (objectTriggered.gameObject.tag == "shield")
        {
            StartCoroutine("ShieldBufferTime", objectTriggered);
        }
        if (objectTriggered.gameObject.CompareTag("LogTrigger"))
        {
            //Debug.Log(block.transform.position);
            //Debug.Log(finalBlockLoc);
            //block.GetComponent<Animator>().SetTrigger("triggered");

            StartCoroutine("DelayFunc");
        }
        if (objectTriggered.gameObject.CompareTag("EnemyTrigger"))
        {
            hasTriggeredEnemy = true;
            for (i = 0; i < enemyCount; i++)
            {
                enemies[i].GetComponent<Animator>().SetBool("Triggered", false);
                enemies[i].GetComponent<Animator>().enabled = false;
                enemies[i].GetComponent<EnemyController>().StartCoroutine("BulletFiring");
            }
        }
        if (objectTriggered.gameObject.CompareTag("EnemyTriggerEnd"))
        {
            hasTriggeredEnemy = false;
            StartCoroutine("Instructions", 1);
            for (i = 0; i < enemyCount; i++)
            {
                enemies[i].GetComponent<Animator>().SetBool("Triggered", true);
                enemies[i].GetComponent<Animator>().enabled = true;
            }
        }

        if (objectTriggered.gameObject.CompareTag("JumperTrigger"))
        {
            StartCoroutine("Instructions", 2);
        }

        if (objectTriggered.gameObject.CompareTag("Launcher"))
        {
            rb.AddForce(new Vector3(0f, 750f, 300f));
        }

        if (objectTriggered.gameObject.CompareTag("Ramp"))
        {
            rb.AddForce(new Vector3(0f, 0f, 800f));
        }

        if(objectTriggered.gameObject.name== "CamTrigger1")
        {
            camera.GetComponent<Animator>().SetBool("CamLoc1", true);
            StartCoroutine("CamDelay");
        }
        if(objectTriggered.gameObject.name== "CamTrigger2")
        {
            CamFollow.gameStarted = true;
            CamFollow.offset1 = new Vector3(0f, 3f, 3f);
        } 
        if(objectTriggered.gameObject.name== "Boundary")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator ShieldBufferTime(Collider objectTriggered)
    {
        objectTriggered.gameObject.SetActive(false);
        shieldInBall.gameObject.SetActive(true);
        shieldActive = true;
        yield return new WaitForSeconds(5f);
        objectTriggered.gameObject.SetActive(true);
        shieldInBall.gameObject.SetActive(false);
        shieldActive = false;
    }
    IEnumerator InstructionsQueue(string instructionCode)
    {
        switch (instructionCode)
        {
            case "level":
                //int y = SceneManager.GetActiveScene().buildIndex;
                //instructionText.text = "LEVEL";
                //instructionText.text = (y+1).ToString();
                level1.SetActive(true);
                yield return new WaitForSeconds(2f);
                level1.SetActive(false);
                howto.SetActive(true);
                yield return new WaitForSeconds(3f);
                howto.SetActive(false);
                howto2.SetActive(true);
                yield return new WaitForSeconds(2f);
                howto2.SetActive(false);
                break;
            case "introduction":
                break;
            case "howto":
                break;
        }
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Instructions(int insNum)
    {
        switch (insNum)
        {
            case 1:
                instruction1.SetActive(true);
                yield return new WaitForSeconds(2f);
                instruction1.SetActive(false);
                break;
            case 2:
                instruction2.SetActive(true);
                yield return new WaitForSeconds(2f);
                instruction2.SetActive(false);
                break;
        }

    }

    IEnumerator CamDelay()
    {
        yield return new WaitForSeconds(2f);
        CamFollow.gameStarted = false;
    }
    // keep the ienumerator function alive; might be useful for camera positioning for lerp
    IEnumerator DelayFunc()
    {
        CamFollow.gameStarted = false;
        camera.GetComponent<Animator>().SetBool("Pan", true);
        yield return new WaitForSeconds(2.5f);
        bridge.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2f);
        camera.GetComponent<Animator>().SetBool("Pan", false);
        yield return new WaitForSeconds(2f);
        CamFollow.gameStarted = true;
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (force < 500)
                force += 10;
            powerBarSlider.value = force;
            print(force);
        }
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-direction * force);
            
            force = 0;
            powerBarSlider.value = force;
        }
    }

    void FixedUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            var heading = transform.position - targetPoint;
            var distance = heading.magnitude;
            direction = heading / distance;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedNew * Time.deltaTime);
        }
    }
}
