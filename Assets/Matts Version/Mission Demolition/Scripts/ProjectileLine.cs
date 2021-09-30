using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; //Singleton

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    public LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    // Start is called before the first frame update
    void Awake()
    {
        S = this; //set singleton
        line = GetComponent<LineRenderer>();
        line.enabled = false; //disable until needed
        points = new List<Vector3>();

    }

    public GameObject poi
    {
        get { return (_poi); }
        set
        {
            _poi = value;

            if (_poi != null) //reset everything if _poi is set to something not null
                line.enabled = false;
            points = new List<Vector3>();
            AddPoint();
        }
    }

    //Clear line directly
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    //Add Point
    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;

        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        { return; } //if point isnt far enough from last point, return out.


        //Starting the line:
        if (points.Count == 0)  //if launch point
        { 
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            //set first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;//Enables the lineRenderer
        }
        else //Normal behavior of adding a point
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint
    {
        get
        {
            if(points == null)
            { return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }

    private void FixedUpdate()
    {
        if (poi == null) //no poi? search for one
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                { return; } //if nothing was found
            }
            else
            { return; } //if nothing was found

        }
        AddPoint(); // if POI then loc added every fixed update

        if(FollowCam.POI == null)
        {
            //Once FollowCam.POI is null, make the local poi null too
            poi = null;
        }
    }

}