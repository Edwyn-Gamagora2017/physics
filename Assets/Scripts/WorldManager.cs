using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	[SerializeField]
	PhysicsObjectGraphics[] physicsObjects;
	[SerializeField]
	CubeGraphics[] staticObjects;

	[SerializeField]
	GameObject PhysicsObjPrefab;
	[SerializeField]
	int createObjects = 0;

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

		foreach( CubeGraphics obj in staticObjects ){
			objects.Add( obj.StaticObj );
		}

		if( createObjects <= 0 ){
			// Keep
			foreach( PhysicsObjectGraphics obj in physicsObjects ){
				objects.Add( obj.PhysicsObj );
				obj.World = this;
			}
		}
		else{
			// Destroy old ones
			foreach( PhysicsObjectGraphics obj in physicsObjects ){
				Destroy( obj.gameObject );
			}
			// Create random
			physicsObjects = new PhysicsObjectGraphics[ createObjects ];
			for(int i=0;i<createObjects;i++){
				GameObject g = Instantiate( PhysicsObjPrefab );
				g.transform.position = new Vector3( Random.Range( -6, 6 ), 10, Random.Range( -6, 6 ) );
				physicsObjects[i] = g.GetComponent<PhysicsObjectGraphics>();
				physicsObjects[i].PhysicsObj.setPosition( g.transform.position );
			}
			foreach( PhysicsObjectGraphics obj in physicsObjects ){
				objects.Add( obj.PhysicsObj );
				obj.World = this;
			}
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
