using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : AbstractObject {

	Vector3 startPosition;

	Vector3 position;		// m
	Vector3 speed;			// m/s
	Vector3 acceleration;	// m/s2

	float mass;

	CubeStaticObject boundingBox;

	public static Vector3 gravityAcc = new Vector3(0f,-9.8f,0f);

	public PhysicsObject( float mass, Vector3 startPosition, CubeStaticObject boundingBox )
	: base( startPosition ){
		this.mass = mass;

		this.position = startPosition;
		this.speed = new Vector3(0,0,0);
		this.acceleration = new Vector3(0,0,0);

		this.boundingBox = boundingBox;
	}

	public void evaluate( List<Vector3> forces, float deltaTSeconds, List<AbstractObject> world ){
		// calculate Acceleration
		Vector3 newAcceleration = new Vector3(0,0,0);
		foreach( Vector3 force in forces ){
			newAcceleration += force;
		}
		newAcceleration /= mass;
		// calculate Speed
		Vector3 newSpeed = this.speed + newAcceleration*deltaTSeconds;
		// calculate Position
		Vector3 newPosition = this.position + newSpeed*deltaTSeconds;

		// Calculate Collision
		bool collisionTrigger = false;
		CubeStaticObject futureBoundingBox = new CubeStaticObject( newPosition, this.boundingBox.Width, this.boundingBox.Height, this.boundingBox.Depth );
		foreach( AbstractObject obj in world ){
			// Same object
			if( obj != this ){
				bool col = futureBoundingBox.collision( obj.getBoundingBox() );
				if( col ){
					collisionTrigger = true;
				}
			}
		}
		if( !collisionTrigger ){
			// Update info
			this.acceleration = newAcceleration;
			this.speed = newSpeed;
			this.position = newPosition;
			this.boundingBox.Position = newPosition;
		}
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

	#region implemented abstract members of AbstractObject

	public override CubeStaticObject getBoundingBox ()
	{
		return this.boundingBox;
	}

	#endregion
}
