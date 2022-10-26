using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AITank : MonoBehaviour {

    public float radius = 10;
    public int numWaypoints = 5;
    public int current = 0;
    List<Vector3> waypoints = new List<Vector3>();
    public float speed = 10;
    public Transform player;    

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
           float theta = (Mathf.PI * 2.0f) / numWaypoints;
            for(int i = 0 ; i < numWaypoints ; i ++)
            {
                float angle = theta * i;
                Vector3 ps = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
                ps = transform.TransformPoint(ps);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(ps, 1); 
            }
        }
    }

    // Use this for initialization
    void Awake () {
        // Task 2
        // Put code here to calculate the waypoints in a loop and 
        // Add them to the waypoints List
        float theta = (Mathf.PI * 2.0f) / numWaypoints;
        for(int i = 0 ; i < numWaypoints ; i ++)
        {
            float angle = theta * i;
            Vector3 ps = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
            ps = transform.TransformPoint(ps);
            waypoints.Add(ps); 
        }
    }

    // Update is called once per frame
    void Update () {
        // Task 3
        // Put code here to move the tank towards the next waypoint
        // When the tank reaches a waypoint you should advance to the next one
        Vector3 ps = transform.position;
        Vector3 Next = waypoints[current] - ps;
        float dist = Next.magnitude;
        if (dist < 1)
        {
            current = (current + 1) % waypoints.Count;
        }
        Vector3 direction = Next / dist;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Next, Vector3.up), 270 * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, waypoints[current], Time.deltaTime);
    

        // Task 4
        // Put code here to check if the player is in front of or behine the tank
         Vector3 Player = player.position - transform.position;
        float dot = Vector3.Dot(transform.forward,Player.normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        
        if (dot < 0.10)
        {
            GameManager.Log("Tank is behind");       
        }
        else
        {
            GameManager.Log("Tank is in the front"); 
        }
        // Task 5
        // Put code here to calculate if the player is inside the field of view and in range
        // You can print stuff to the screen using:

        if (angle < 0.45)
        {
            GameManager.Log("Tank is in the fov");       
        }
        else
        {
            GameManager.Log("Tank is not in fov"); 
        }

    }
}
