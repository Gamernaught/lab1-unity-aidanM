using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource playerAudio;
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 22.0f;
    public float zRange = 9.0f;
    public bool gameOver = false;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public AudioClip crashSound;
    public ParticleSystem explosionParticle;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // Collision with objects.
    private void OnCollisionEnter(Collision collision)
    {
        // Collision with enemies
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup == false)
        {
            Debug.Log("Game Over!");
            gameOver = true;
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
        //collision with enemies when player has powerup
        else if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    // Moves the player
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    // Picking up powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power up"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
           //powerupIndicator.gameObject.SetActive(true);
        }
    }
    // Timer for the power up
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        //powerupIndicator.gameObject.SetActive(false);
    }
}
