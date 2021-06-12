using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hookPoint;
    public GameObject segment;
    public int numberOfLinks = 5;

    // Start is called before the first frame update
    private void Start()
    {
        GenerateRope();
    }

    public void GenerateRope()
    {
        Rigidbody2D prevBody = hookPoint;
        for (int i = 0; i < numberOfLinks; i++)
        {
            GameObject newSegment = Instantiate(segment);
            newSegment.transform.parent = transform;
            newSegment.transform.position = transform.position;
            HingeJoint2D connectionPoint = newSegment.GetComponent<HingeJoint2D>();
            connectionPoint.connectedBody = prevBody;
            prevBody = newSegment.GetComponent<Rigidbody2D>();
        }
    }
}