using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject shotPrefab;
    // public float distanceFromPlayer = 20.0f;
    public float aggroDistance = 20.0f;
    public float timeBetweenShots = 2.0f;
    public int attackDamage = 10;
    public float rotationTrackingSpeed = 15f;
    public float predictionFuzzFactor = 0.1f;
    public float predictionCorrectionCoefficient = 0.9f;
    public float predictionYOffset = 0.5f;
    public AudioSource audioSource;

    private int playerMask;
    private GameObject playerObject;
    private Transform player;
    private CharacterController playerController;
    private Rigidbody playerRigidbody;
    private float shotTimer = 0.0f;
    private Vector3 target;
    private float randomShotDelay;
    private float randomShotFactor;
    private Transform shotOrigin;
    private float shots = 0.0f;
    private float hits = 0.0f;
    private float shotSpeed;

    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            // playerController = playerObject.GetComponent<CharacterController>();
            playerRigidbody = playerObject.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Could not find Player GameObject by tag");
        }

        shotOrigin = transform.Find("Shot Origin");
        if (shotOrigin == null)
        {
            Debug.LogError("Could not find Shot Origin transform. " +
                           "Does this enemy have a Shot Origin child object?");
        }
        randomShotFactor = 0.25f * timeBetweenShots;
        randomShotDelay = timeBetweenShots + Random.Range(-randomShotFactor, randomShotFactor);

        if (shotPrefab == null)
        {
            Debug.LogError("Need an EnemyShot prefab reference bruh. Drag it into the EnemyRanged component pls.");
        }
        else
        {
            shotSpeed = shotPrefab.GetComponent<EnemyShot>().speed;
        }

        playerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float playerSquaredDistance = (player.position - transform.position).sqrMagnitude;

        // If the player is nearby, aggro
        if (playerSquaredDistance < aggroDistance * aggroDistance)
        {
            if (shotTimer > randomShotDelay)
            {
                Shoot();
                randomShotDelay = timeBetweenShots + Random.Range(-randomShotFactor, randomShotFactor);
                shotTimer = 0.0f;
            }

            Quaternion towardPlayerOrientation =
                Quaternion.LookRotation(player.position - transform.position, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                          towardPlayerOrientation,
                                                          rotationTrackingSpeed * Time.deltaTime);
        }
        shotTimer += Time.deltaTime;
    }

    void Shoot()
    {
        float timeGuess = (player.position - transform.position).magnitude / shotSpeed;
        timeGuess *= predictionCorrectionCoefficient;
        timeGuess = timeGuess + Random.Range(timeGuess - predictionFuzzFactor * timeGuess,
                                             timeGuess + predictionFuzzFactor * timeGuess);
        shots++;
        Debug.Log("Ranged enemy shot");

        Vector3 targetPosition = new Vector3(player.position.x,
            player.position.y + predictionYOffset,
            player.position.z)
            + playerRigidbody.velocity * timeGuess;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - shotOrigin.position,
                                                            Vector3.up);

        shotOrigin.rotation = targetRotation;

        GameObject shotObject = Instantiate(shotPrefab, shotOrigin.position, shotOrigin.rotation)
            as GameObject;
        EnemyShot shotComponent = shotObject.GetComponent<EnemyShot>();
        audioSource.Play();
        shotComponent.shooter = this;
    }

    public void RegisterHit()
    {
        hits++;
        Debug.Log("Ranged Enemy Accuracy: " + (hits / shots).ToString());
    }
}
