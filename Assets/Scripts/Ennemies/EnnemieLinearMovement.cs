using UnityEngine;

public class EnnemieLinearMovement : MonoBehaviour
{

    private Vector3[] waypoints;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform go = transform.GetChild(i);
            if (go.gameObject.CompareTag("Waypoint"))
            {
                //waypoints[i] = go.position;
            }
        }

    }

}
