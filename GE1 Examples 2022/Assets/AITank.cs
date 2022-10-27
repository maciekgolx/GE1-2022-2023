using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : MonoBehaviour
{
    public List<Vector3> waypoints;
    public int count = 5;
    public float radius = 5;
    public Transform player;   
    public float speed;


    void SetUpWaypoints()
    {
        waypoints = new List<Vector3>();
        waypoints.Clear();
        float theta = (Mathf.PI * 2.0f) / (float) count;

        for(int i = 0 ; i < count ; i ++)
        {
            float angle = i * theta;
            Vector3 p = new Vector3
                (
                    Mathf.Sin(angle) * radius, 
                    0,
                    Mathf.Cos(angle) * radius
                );
            p = transform.TransformPoint(p);
            waypoints.Add(p);

        }
    }

    void OnDrawGizmos()
    {
        SetUpWaypoints();
        foreach(Vector3 v in waypoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(v, 0.5f);
        }
    }
    void Awake () {
        // Task 2
        // Put code here to calculate the waypoints in a loop and 
        // Add them to the waypoints List
        float theta = (Mathf.PI * 2.0f) / count;
        for(int i = 0 ; i < count ; i ++)
        {
            float angle = theta * i;
            Vector3 ps = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
            ps = transform.TransformPoint(ps);
            waypoints.Add(ps); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetUpWaypoints();

    }

    int current = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 ps = transform.position;
        Vector3 Next = waypoints[current] - ps;
        float dist = Vector3.Distance(transform.position, waypoints[current]);
        if (dist < 1.0f)
        {
            current = (current + 1) % waypoints.Count;
        }
        transform.LookAt(waypoints[current]);
        transform.Translate(0, 0, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Next, Vector3.up), 270 * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, waypoints[current], Time.deltaTime);
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
