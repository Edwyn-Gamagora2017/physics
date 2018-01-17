using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectGraphics : MonoBehaviour {

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
		physicsObj = new PhysicsObject( 1, this.transform.position, generateBoundingBox() );
		forces = new List<Vector3>();
	}

	// Use this for initialization
	void Start () {
	}

	void FixedUpdate () {
		forces.Clear();
		forces.Add( PhysicsObject.gravityAcc );

		if( world != null ){
			physicsObj.evaluate( forces, Time.deltaTime, world.Objects );
		}

		this.transform.position = physicsObj.getPosition();
	}
}
