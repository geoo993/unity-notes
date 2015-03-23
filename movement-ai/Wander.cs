﻿using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {
	
	/* The forward offset of the wander square */
	public float wanderOffset = 1.5f;
	
	/* The radius of the wander square */
	public float wanderRadius = 4;
	
	/* The rate at which the wander orientation can change */
	public float wanderRate = 0.4f;
	
	private float wanderOrientation = 0;
	
	private GameObject debugRing;
	
	private SteeringUtils steeringUtils;
	
	
	public float rayDistance = 2;
	public float sameDecisionDuration = 3;
	
	private float lastDecisionTime = Mathf.NegativeInfinity;
	private int decision;

	private int levelBoundaryMask;

	private Enemy enemy;

	void Start() {
		//		DebugDraw debugDraw = gameObject.GetComponent<DebugDraw> ();
		//		debugRing = debugDraw.createRing (Vector3.zero, wanderRadius);
		
		steeringUtils = gameObject.GetComponent<SteeringUtils> ();

		levelBoundaryMask = (1 << LayerMask.NameToLayer("LevelBoundary"));

		enemy = gameObject.GetComponent<Enemy> ();
	}
	
	void FixedUpdate () {

		if(enemy.stunTill > Time.time) {
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			return;
		}
		
		float characterOrientation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		
		/* Update the wander orientation */
		updateWanderOrientation (characterOrientation);
		
		/* Calculate the combined target orientation */
		float targetOrientation = wanderOrientation + characterOrientation;
		
		/* Calculate the center of the wander circle */
		Vector3 targetPosition = transform.position + (orientationToVector (characterOrientation) * wanderOffset);
		
		//		debugRing.transform.position = targetPosition;
		
		/* Calculate the target position */
		targetPosition = targetPosition + (orientationToVector(targetOrientation) * wanderRadius);
		
		//		Debug.DrawLine (transform.position, targetPosition);
		
		
		Vector2 acceleration = steeringUtils.seek (targetPosition);
		
		steeringUtils.steer (acceleration);
		
		steeringUtils.lookWhereYoureGoing ();
		
	}
	
	private void updateWanderOrientation(float characterOrientation) {
		Vector3 dir = orientationToVector (characterOrientation);
		
		//		Vector3 end = transform.position + dir * rayDistance;
		//		Debug.DrawLine (transform.position, end, Color.green);
		
		RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayDistance, levelBoundaryMask);
		/* If we collide with a wall then move away from it */
		if (hit.collider != null){
			/* If we have not already decided which direction we will move away from the 
			 * wall then decide it now */
			if(lastDecisionTime + sameDecisionDuration < Time.time) {
				decision = (int)(Random.value * 2);
				lastDecisionTime = Time.time;
			}
			/* Set the wander orientation to the decided direction */
			else {
				wanderOrientation = Mathf.PI/2;
				if(decision == 0) {
					wanderOrientation *= -1;
				}
			}
			
			//Debug.Log (wanderOrientation);
			//Debug.Log (hit.transform.name);
		}
		/* Else update the wander orienation randomly like normal */
		else {
			wanderOrientation += randomBinomial() * wanderRate;
		}
	}
	
	/* Returns a random number between -1 and 1. Values around zero are more likely. */
	float randomBinomial() {
		return Random.value - Random.value;
	}
	
	/* Returns the orientation as a unit vector */
	Vector3 orientationToVector(float orientation) {
		return new Vector3(Mathf.Cos(orientation), Mathf.Sin(orientation), 0);
	}

}
