using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FollowWP2 : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    public GameObject wpManager;
    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoint;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];

       // Invoke("GoToRuin", 2);
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[12]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[4]);
        currentWP = 0;
    }
    public void GoToFactory()
    {
        g.AStar(currentNode, wps[8]);
            currentWP = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (g.pathList.Count == 0 || currentWP == g.pathList.Count)
            return;

        if (Vector3.Distance(g.pathList[currentWP].getID().transform.position, 
                             this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWP].getID();
            currentWP++;
        }
        
        if(currentWP < g.pathList.Count)
        {
            goal = g.pathList[currentWP].getID().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,
                                                  this.transform.position.y,
                                             goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                       Quaternion.LookRotation(direction),
                                                       Time.deltaTime * rotSpeed);
            this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
                                                       
        }

    }
}
