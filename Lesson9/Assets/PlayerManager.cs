using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private Animator anm;
    // Start is called before the first frame update
    void Start()
    {
        anm = anm.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        //transform the input direction from the player's local space to world space
        Vector3 worldInputMovement = transform.TransformDirection(movementDirection.normalized);
        
        controller.Move(worldInputMovement * speed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            anm.SetFloat("speed", 1);
        }
        else if  (movementDirection == Vector3.zero) 
        {
            anm.SetFloat("speed", 0);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            anm.SetBool("isRun", true);
        } else
        {
            anm.SetBool("isRun", false);
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            anm.SetTrigger("isJump");
        }

        // Get mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X");
        // Rotate the player based on mouse input
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);

    }
}
