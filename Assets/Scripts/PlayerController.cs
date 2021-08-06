using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public bool doubleSpeed = false;

    public float jumpForce;
    public float gravityModifier;

    private int jumpCount = 0;
    public bool gameOver = false;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatter;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private float volume = 1.0f;

    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCount < 2) {
            jumpCount += 1;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(jumpSound, volume);
        }

        if (Input.GetKey(KeyCode.D)) {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        } else if (doubleSpeed) {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver) {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag("Ground")) {
            jumpCount = 0;
            dirtSplatter.Play();

        } else if (collision.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(crashSound, volume);
            Debug.Log("Game Over!");
        }
    }
}
