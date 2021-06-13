using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREDIT https://github.com/dci05049/Verlet-Rope-Unity
public class Rope : MonoBehaviour
{
    public GameObject ConnectionInput;
    public GameObject ConnectionOutput;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float ropeSegLen = 0.25f;
    public int segmentLength = 35;
    public float lineWidth = 0.1f;
    public float scalar = 1f;

    [Header("Rope end")]
    public SpriteRenderer[] ropeEnds;
    public Sprite[] ropeEndSprites;

    // Use this for initialization
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = ConnectionInput.transform.position;

        for (int i = 0; i < segmentLength; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        this.DrawRope();
        if (ropeEnds[0] != null)
        {
            ropeEnds[0].transform.position = ConnectionInput.transform.position;
            if (ConnectionInput.GetComponent<Nodes.NodeConnection>().node.IsPowered())
                ropeEnds[0].sprite = ropeEndSprites[0];
            else
                ropeEnds[0].sprite = ropeEndSprites[1];


        }
        if (ropeEnds[1] != null)
        {
            ropeEnds[1].transform.position = ConnectionOutput.transform.position;
            if (ConnectionOutput.GetComponent<Nodes.NodeConnection>().node.IsPowered())
                ropeEnds[1].sprite = ropeEndSprites[0];
            else
                ropeEnds[1].sprite = ropeEndSprites[1];
        }
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        float dist = Vector3.Distance(ConnectionInput.transform.position, ConnectionOutput.transform.position);
        dist /= (float)segmentLength;
        dist *= scalar;
        ropeSegLen = dist;
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = this.ConnectionInput.transform.position;
        this.ropeSegments[0] = firstSegment;

        //Constrant to Second Point
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = this.ConnectionOutput.transform.position;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        if (ConnectionInput.GetComponent<Nodes.NodeConnection>().node.IsPowered())
        {
            lineRenderer.material.SetInt("_POWERED", 1);
        }
        else
        {
            lineRenderer.material.SetInt("_POWERED", 0);
        }

        lineRenderer.startWidth = this.lineWidth;
        lineRenderer.endWidth = this.lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}