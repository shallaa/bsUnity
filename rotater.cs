using UnityEngine;
using System.Collections;

public class rotater : MonoBehaviour {
	
	private Vector3 angle = new Vector3( 0, 80, 80 );

	void Update(){
		transform.Rotate( Time.deltaTime * angle );
	}
}
