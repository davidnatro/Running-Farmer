using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private float speed = 30.0f;
    private float leftBound = -10.0f;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if (playerControllerScript.gameOver == false) {
            if (playerControllerScript.doubleSpeed) {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            } else {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }            
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle")) {
            Destroy(gameObject);
        }
    }
}
