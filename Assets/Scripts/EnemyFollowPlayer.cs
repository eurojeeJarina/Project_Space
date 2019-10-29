using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    private Transform target;
    bool playerDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            moveToPlayer();
        }
    }

    // if player enter our collider
    private void OnTriggerEnter(Collider other)
    {
        //print("Detected");
        if (other.tag == "Player")
        {
            playerDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerDetected = false;
    }

    void moveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
