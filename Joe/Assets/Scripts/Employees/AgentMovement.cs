using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public enum States {
        TOWARDTARGET,
        WANDER,
        WORKING,
    }
    [SerializeField] public States currentState;
    public float wanderRadius;
    public float wanderTimer;
    private float timer;
    private Vector3 target = new Vector3(0f, 0f, 0);
    private Machine currentMachine;
    NavMeshAgent agent;
    Employee employee;
    private int maxHappinessValue;
    private int delayAmount = 5;
    private float happniessTimer;
    private int wanderingHappinessGain = 1;
    public HappinessSystem happinessSystem;
    public HappinessBar happinessBar;
    public GameObject deathParticle;
    GlobalVars globals;
    void Awake() {
        GameObject manager = GameObject.Find("MainManager");
        globals = manager.GetComponent<GlobalVars>();

        employee = GetComponent<Employee>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        maxHappinessValue = 100;

        happinessSystem = new HappinessSystem(maxHappinessValue);
        happinessBar.setUp(happinessSystem);

        timer = wanderTimer;
        currentState = States.WANDER;
    }
    void Update()
    {
        if (happinessSystem.getHappiness() <= 0 ) {
            globals.emplyoeesDied += 1;
            globals.happinessLost += 20;
            currentMachine.employeesAssigned.Remove(employee);
            Instantiate(deathParticle, agent.transform.position, agent.transform.rotation);
            GameObject.Destroy(this.gameObject);
        }

        if (currentState == States.TOWARDTARGET) {
            MovingToTarget();
        }
        else if (currentState == States.WANDER) {
            Wandering();
        }
        else if (currentState == States.WORKING) {
            Working();
        }
    }
    void MovingToTarget() {
        SetAgentPosition();
    }
    void Wandering() {
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        happniessTimer += Time.deltaTime;

        if (happniessTimer >= delayAmount) {
            happniessTimer = 0;
            happinessSystem.increaseHappiness(wanderingHappinessGain);
        }
    }
    void Working() {
        if (currentMachine.gameObject.tag != "Slide") {
            happniessTimer += Time.deltaTime;

            if (happniessTimer >= delayAmount) {
                happniessTimer = 0;
                happinessSystem.decreaseHappiness(globals.happinessLost);
            }
        }
    }
    void SetAgentPosition() {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        if (Vector2.Distance(transform.position, target) < 10f)
        {
            currentState = States.WORKING;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
    public void changeMachineTarget(Machine machine) {
        currentMachine = machine;
        target = machine.transform.position;
    }
    public void changeState(int stateNumber) {
        if (stateNumber == 1) {
            currentState = States.TOWARDTARGET;
        }
        else if (stateNumber == 2) {
            currentState = States.WANDER;
        }
        else if (stateNumber == 3) {
            currentState = States.WORKING;
        }
    }
}
