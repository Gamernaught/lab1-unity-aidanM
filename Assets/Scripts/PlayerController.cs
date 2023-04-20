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
    public AudioClip crashSound;
    public ParticleSystem explosionParticle;

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
    //Collision with objects.
    private void OnCollisionEnter(Collision collision)
    {
        //Collision with enemies
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }
}
