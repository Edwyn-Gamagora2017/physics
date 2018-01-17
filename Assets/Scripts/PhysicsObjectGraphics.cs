using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectGraphics : MonoBehaviour {

	PhysicsObject physicsObj;

	List<Vector3> forces;

	// Use this for initialization
	void Start () {
		physicsObj = new PhysicsObject( 1, this.transform.position );
		forces = new List<Vector3>();
	}

	void FixedUpdate () {
		forces.Clear();
		forces.Add( PhysicsObject.gravityAcc );

		physicsObj.evaluate( forces, Time.deltaTime );

		this.transform.position = physicsObj.getPosition();
	}
}
