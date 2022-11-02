using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("Collision");
            ExplodeMyParts();
        }
    }

    private void ExplodeMyParts()
    {
        foreach (Transform t in this.GetComponentsInChildren<Transform>())
        {
            Rigidbody rt = t.gameObject.GetComponent<Rigidbody>();
            if (rt == null)
            {
                rt = t.gameObject.AddComponent<Rigidbody>();
            }
            rt.useGravity = true; 
            rt.isKinematic = false;
            Vector3 v = new Vector3(
                Random.Range(-5, 5)
                , Random.Range(5, 10)
                , Random.Range(-5, 5)
                );
            rt.velocity = v;
        }
        Invoke("Sink", 4);
        Destroy(this.gameObject, 7);
        Destroy(transform.GetChild(0), 7); 
    }

    void Sink()
    {
        GetComponent<Collider>().enabled = false;
        transform.GetChild(0).GetComponent<Collider>().enabled = false;
    }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}