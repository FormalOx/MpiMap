using UnityEngine;

public class Unfold : MonoBehaviour {
	Transform   delta;
	public  UII		alpha;

	public  string  nme;
	[TextArea]
	public  string  desc;

	public  bool    group;

    // Start is called before the first frame update
    void Start () {
		if( group ) {
			GetComponent<SphereCollider> ().radius = 0.5f;
		}
    }

	private void OnTriggerEnter ( Collider other ) {
		if( group ) {
			alpha.Group ( transform.position, 1 );
			return;
		}
		delta = other.transform;
		alpha.Add ( this );		
	}

	private void OnTriggerExit ( Collider other ) {
		if( group ) {
			alpha.Group ( transform.position, -1 );
			alpha.nl.MoveLayers ( -1 );
			return;
		}
		alpha.Remove ( this );
	}
}
