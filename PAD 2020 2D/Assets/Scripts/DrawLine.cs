﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    private static float currentCoefficient = 0;
    public Vector3 dir;
    public Vector2 Middle = new Vector2(0, 0);
    public float offsetSource = 2.982594f;

    private LineRenderer lineRenderer;


    void Awake() {
         lineRenderer = GetComponent<LineRenderer>(); // get the component from unity
         lineRenderer.positionCount = 2; // set amount of positions
         lineRenderer.alignment = LineAlignment.Local;

         if (gameObject.name.Equals("LineDrawer")) {
            lineRenderer.SetPosition(0, Objectives.objectives[0].position);
            lineRenderer.SetPosition(1, Waypoints.pipes[0].position);
            //renderer.SetPosition(1, new Vector3(Objectives.objectives[0].position.x + 1, Objectives.objectives[0].position.y - 1, 0f));
        } else if (gameObject.name.Equals("Waypoints1T2")) {
            lineRenderer.SetPosition(0, Waypoints.pipes[0].position);
            lineRenderer.SetPosition(1, new Vector3(Waypoints.pipes[0].position.x + 1, Waypoints.pipes[0].position.y - 1, 0f));
        } else if (gameObject.name.Equals("Waypoints2T3")) {
            lineRenderer.SetPosition(0, Waypoints.pipes[1].position);
            lineRenderer.SetPosition(1, new Vector3(Waypoints.pipes[1].position.x + 1, Waypoints.pipes[1].position.y - 1, 0f));
        } else if (gameObject.name.Equals("GoalLine")) {
            lineRenderer.SetPosition(0, Waypoints.pipes[2].position);
            lineRenderer.SetPosition(1, new Vector3(Waypoints.pipes[2].position.x + 1, Waypoints.pipes[2].position.y - 1, 0f));
        }
    }

    private void FixedUpdate()
    {
        StopLineOnCol(GetComponent<LineRenderer>().GetPosition(1));
    }

    private void StopLineOnCol(Vector3 points)
    {
        LineRenderer renderer = GetComponent<LineRenderer>();
        //here you need to find the direction of the line and set it so you can allign the line and ray 

        Vector3 TO = new Vector3(points.x, points.y) - Objectives.objectives[0].position;
        float distance = TO.magnitude;
        Vector3 Dir = TO / distance;

        if (gameObject.name.Equals("LineDrawer"))
        {
            //this sets the ray to the directions for 100 units you may want to change that but it wil only hit the first object unless you want it to hit multiple at one time this needs to be changed
            RaycastHit2D hitWaypoint1 = Physics2D.Linecast(Objectives.objectives[0].position, Dir);
            //debug for visual reason
            //RED is just ray directions of the firsty Objective only using the dirctions of the line render not the calculations less taxing on a pc. this calls every Physics frame so you may want this changed aswell
            Debug.DrawRay(Objectives.objectives[0].position, Dir, Color.red);
            //this tests on hit of the colider and will do on ANY Colision you may want to change this so it only colides with one or more objects.
            ;
            //here is where you see what the LINE or RAYCAST hit make sure to use this for scaling.
            if (hitWaypoint1.collider.CompareTag("Pipe"))
            {
                //debug that should only show if you hit a object with a colider ANY colider will do if you want a name or tag you can use the if statment above to get any varible you want compared
                Debug.Log("point values " + new Vector3(points.x, points.y) + "point values " + renderer.GetPosition(1) + "hit name is:" + hitWaypoint1.transform.name);
            }
        }
        
    }

    public static string GetFormulaFromVector(Vector2 startPos, Vector2 endPos) {
        string formule;
        // algebra
        // y = ax + b, where a = dy / dx
        if (startPos != endPos) {
            float dy = endPos.y - startPos.y;
            float dx = endPos.x - startPos.x;
            float coefficient = dy / dx;
            currentCoefficient = coefficient;
            // use mousePos for y & x, multiply coefficient by x and subtract result from y, that way you have b.
            float startPoint;
            float ax = coefficient * endPos.x; // coefficient calculation
            startPoint = endPos.y - ax; // calculation of b (b = y - ax)
            string mathOperator = (startPoint > 0 ? " + " : " - "); // get the math operator for the equation depending on the value of startpoint (b)
            float roundedStart = Mathf.Ceil(startPoint); // get rid of redundant '-' when b is negative
            string start = Mathf.Ceil(startPoint).ToString();
            if (roundedStart <= 0) {
                start = start.Substring(1).Trim(); // remove the redundant minus
            }
            formule = roundedStart != 0 ? coefficient.ToString("F1") + "x" + mathOperator + start : coefficient.ToString("F1") + "x";
        } else {
            formule = "Geen formule";
        }
        return formule;
    }

    public static string GetFormulaFromVector(Vector3 startPos, Vector3 endPos) {
        return GetFormulaFromVector(new Vector2(startPos.x, startPos.y), new Vector2(endPos.x, endPos.y));
    }

    public static float getCurrentCoefficient() {
        return currentCoefficient;
    }
}
