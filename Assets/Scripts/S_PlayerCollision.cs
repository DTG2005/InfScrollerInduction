using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PlayerCollision : MonoBehaviour
{
    public GameObject gm;
    public float raycastDistance = 10.0f;
    float timeOut = 3;
    public S_PlayerMovement movement;
    private float timer = 0.0f;
    private bool isActionInProgress = false;

    void Update(){
        if (isActionInProgress)
        {
            // Increment the timer by the elapsed time since the last frame
            timer += Time.deltaTime;

            // Check if the action duration has been reached
            if (timer >= 3)
            {
                // Perform actions when the action duration is reached
                Debug.Log("Fell off, restarting.");
                // Reset the timer and mark the action as complete
                ResetTimer();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);

            }
        }
            // Cast a ray downward to check for ground
            if (!isActionInProgress && !Physics.Raycast(transform.position, Vector3.down, raycastDistance))
            {
                isActionInProgress = true;
            } else if (isActionInProgress&& Physics.Raycast(transform.position, Vector3.down, raycastDistance)){
                ResetTimer();
        }
    }
    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.gameObject.tag == "Obstacle"){
            Debug.Log("We hit an obstacle");
            movement.enabled = false;
            FindObjectOfType<S_GameManager>().EndGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
    }

    void ResetTimer()
    {
        // Reset the timer and mark the action as not in progress
        timer = 0.0f;
        isActionInProgress = false;
    }

}
