using UnityEngine;

public class FollowThePath : MonoBehaviour {

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    public float moveSpeed;
    public float moveSpeedCopy;
    // Index of current waypoint from which Enemy walks
    // to the next one
    public int waypointIndex;

    public bool startImmediately;
    public float startTimer;
    private float countTimer;
    public int seeInt;
	// Use this for initialization
	private void Start () {

        // Set position of Enemy as position of the first waypoint
        // transform.position = waypoints[waypointIndex].transform.position;
        moveSpeedCopy = moveSpeed;
        waypointIndex = 0;
        seeInt = waypoints.Length;
    }
	
	// Update is called once per frame
	private void Update () {
        countTimer += Time.deltaTime;
        // Move Enemy
        if (countTimer >= startTimer || startImmediately)
        {
            Move();
        }
	}

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        //if (waypointIndex <= waypoints.Length - 1)
        if (waypointIndex < waypoints.Length)
        {
            //sDebug.Log(waypointIndex);
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            if (waypoints[waypointIndex].GetComponent<WaypointScript>().moveSpeed != -1)
            {
                moveSpeed = waypoints[waypointIndex].GetComponent<WaypointScript>().moveSpeed;
                //Debug.Log(moveSpeed);
            }
            else
            {
                moveSpeed = moveSpeedCopy;
 
            }
            transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);
            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            Debug.Log(waypoints[waypointIndex].transform.position);
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}
