using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject bottomConnection;
    public GameObject topConnection;

    // Start is called before the first frame update
    private void Start()
    {
        topConnection = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment topSegment = topConnection.GetComponent<RopeSegment>();
        if (topSegment != null)
        {
            topSegment.bottomConnection = gameObject;
            float spriteBottom = topConnection.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }
}