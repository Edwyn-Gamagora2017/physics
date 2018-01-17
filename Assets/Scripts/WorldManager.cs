using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	[SerializeField]
	PhysicsObjectGraphics[] physicsObjects;
	[SerializeField]
	CubeGraphics[] staticObjects;

	List<AbstractObject> objects;
	BoundingBoxTree bbTree;

	public List<AbstractObject> Objects {
		get {
			return objects;
		}
	}
	public BoundingBoxTree BbTree {
		get {
			return bbTree;
		}
	}

	// Use this for initialization
	void Start () {
		objects = new List<AbstractObject>();
		foreach( PhysicsObjectGraphics obj in physicsObjects ){
			objects.Add( obj.PhysicsObj );
			obj.World = this;
		}
		foreach( CubeGraphics obj in staticObjects ){
			objects.Add( obj.StaticObj );
		}
	}

	void FixedUpdate () {
		bbTree = new BoundingBoxTree( objects, BoundingBoxTree.interieurOrderAxe.axeX );

		foreach( PhysicsObjectGraphics obj in physicsObjects ){
			obj.ApplyPhysics();
		}
		foreach( CubeGraphics obj in staticObjects ){
			obj.ApplyPhysics();
		}
	}
}
