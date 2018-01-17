using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectGraphics : ObjectGraphics {

	public float mass = 1;

	PhysicsObject physicsObj;
	List<Vector3> forces;
	WorldManager world;

	public PhysicsObject PhysicsObj {
		get {
			return physicsObj;
		}
	}

	public WorldManager World {
		set {
			world = value;
		}
	}

	public CubeStaticObject generateBoundingBox(){
		return new CubeStaticObject( this.transform.position, this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
	}

	void Awake(){
		physicsObj = new PhysicsObject( mass, this.transform.position, generateBoundingBox() );
		forces = new List<Vector3>();
	}

	// Use this for initialization
	void Start () {
	}

	public override void ApplyPhysics () {
		forces.Clear();
		forces.Add( PhysicsObject.gravityAcc*mass );

		if( world != null ){
			physicsObj.evaluate( forces, Time.deltaTime, world );
		}

		this.transform.position = physicsObj.getPosition();
	}
}
