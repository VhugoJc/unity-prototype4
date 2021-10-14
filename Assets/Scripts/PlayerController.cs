using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    private float powerUpStrength = 15.0f;
    
    void Start()
    {
         //obtener rb
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocusPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        //playerRb.AddForce(Vector3.forward * speed * forwardInput);
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

    }

      private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp")){
            hasPowerUp=true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        Debug.Log("PowerUp finished");
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy") && hasPowerUp){
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
  
  
}
