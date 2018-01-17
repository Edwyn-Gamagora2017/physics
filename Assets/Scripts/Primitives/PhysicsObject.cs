using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : AbstractObject {

	Vector3 startPosition;

	Vector3 position;		// m
	Vector3 speed;			// m/s
	Vector3 acceleration;	// m/s2

	float mass;

	public static Vector3 gravityAcc = new Vector3(0f,-9.8f,0f);

	public PhysicsObject( float mass, Vector3 startPosition )
	: base( startPosition ){
		this.mass = mass;

		this.position = startPosition;
		this.speed = new Vector3(0,0,0);
		this.acceleration = new Vector3(0,0,0);
	}

	public void evaluate( List<Vector3> forces, float deltaTSeconds ){
		// calculate Acceleration
		this.acceleration = new Vector3(0,0,0);
		foreach( Vector3 force in forces ){
			this.acceleration += force;
		}
		this.acceleration /= mass;
		// calculate Speed
		this.speed += this.acceleration*deltaTSeconds;
		// calculate Position
		this.position += this.speed*deltaTSeconds;
	}

	public Vector3 getPosition(){
		return this.position;
	}
	public Vector3 getSpeed(){
		return this.speed;
	}
	public Vector3 getAcceleration(){
		return this.acceleration;
	}
}
