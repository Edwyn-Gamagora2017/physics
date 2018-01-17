using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	[SerializeField]
	PhysicsObjectGraphics[] physicsObjects;
	[SerializeField]
	CubeGraphics[] staticObjects;

	List<AbstractObject> objects;

	public List<AbstractObject> Objects {
		get {
			return objects;
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
