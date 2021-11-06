using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 15f;
    public float jumpVelocity = 5f;

    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject blast;
    public float blastSpeed = 100f;
    
    private float fbInput;
    private float lrInput;
    
    private Rigidbody _rb;

    private SphereCollider _col;

    private GameBehavior _gameManager;
    
    void Start()
    {
       _rb = GetComponent<Rigidbody>();
       _col = GetComponent<SphereCollider>();
       _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        fbInput = Input.GetAxis("Vertical") * moveSpeed;
        lrInput = Input.GetAxis("Horizontal") * rotateSpeed;
    }
    
    void FixedUpdate()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        Vector3 rotation = Vector3.up * lrInput;
        
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * fbInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBlast = Instantiate(blast,
            this.transform.position + new Vector3(1, 0, 0),
            this.transform.rotation) as GameObject;
            // 4
            Rigidbody blastRB =
            newBlast.GetComponent<Rigidbody>();
            // 5
            blastRB.velocity = this.transform.forward *
            blastSpeed;
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
        _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
        capsuleBottom, distanceToGround, groundLayer,
        QueryTriggerInteraction.Ignore);
        return grounded;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hellooooooooo");
        Debug.Log(collision.gameObject.name);
        string[] obstacles = new string[6] 
        {"Obstacle_1", "Obstacle_2","Obstacle_3","Obstacle_4", "X Mover", "Z Mover"};
        if(Array.IndexOf(obstacles, collision.gameObject.name) != -1)
        {
            Debug.Log("OBSTACLEEEEEE");
            _gameManager.Health -= 1;
        }
    }
}
