using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public enum States {
        PLAYERCONTROLLED,
        TOWARDTARGET,
        RANDOM,
        WORKING,
        STILL
    }
    [SerializeField] public States currentState;
    public float speed = 15;
    public float rotationSpeed;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Rigidbody2D rigidBody;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector2 movementDirection;  

    // Things for random movement
    private float timeLeft;
    public float accelerationTime = 2f;

    // Start is called before the first frame update
    private Vector2 target;
    private Vector2 testTarget = new Vector2(0f, 0f);
    private Machine currentMachine;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentState = States.PLAYERCONTROLLED;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == States.PLAYERCONTROLLED) {
            playerControlled();
        }
        else if (currentState == States.TOWARDTARGET) {
            moveTowards();
        }
        else if (currentState == States.RANDOM) {
            random();
        }
        else if (currentState == States.WORKING) {
            working();
        }
    }
    void FixedUpdate() {
        if (movementDirection != Vector2.zero) {
            // Moving rigid body
            int count = rigidBody.Cast(movementDirection, movementFilter, castCollisions, speed * Time.deltaTime + collisionOffset);
            rigidBody.MovePosition(rigidBody.position + movementDirection * speed * Time.deltaTime);

            // Quaternion is used specifically for rotations apparently
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void playerControlled() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector2(horizontalInput, verticalInput);
        // Magnitude
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        // Direction
        movementDirection.Normalize();
        // Move in terms of direction, magnitude, speed and delta time
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
    }
    void random() {
        timeLeft -= Time.deltaTime; 
        // Ensure the change only occures after the time has elapsed
        if (timeLeft <= 0) {
            movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            movementDirection.Normalize();
            transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
            timeLeft += accelerationTime;
        }
    }
    void moveTowards() {
        //float step = speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, target, step);

        //navMeshAgent.SetDestination(currentMachine.transform.position);
        //navMeshAgent.SetDestination(testTarget);

        /*if (Vector2.Distance(transform.position, target) < 0.001f)
        {
            print("Reached Destinations");
            currentState = States.WORKING;
        }*/
    }
    void working() {

    }
    public void changeMachineTarget(Machine machine) {
        currentMachine = machine;
        target = machine.transform.position;
    }
    public void changeState(int stateNumber) {
        if (stateNumber == 1) {
            currentState = States.PLAYERCONTROLLED;
        }
        else if (stateNumber == 2) {
            currentState = States.TOWARDTARGET;
        }
        else if (stateNumber == 3) {
            currentState = States.RANDOM;
        }
        else if (stateNumber == 4) {
            currentState = States.WORKING;
        }
        else if (stateNumber == 5) {
            currentState = States.STILL;
        }
    }
}
