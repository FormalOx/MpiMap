using UnityEngine;

public class NameLeeter : MonoBehaviour {
	public  GameObject[] layer1;
	public  GameObject[] layer2;
	public float    l2b;
	public int status = 0;

	private float time = 0.25f;
	private float deltaTime;

	private void FixedUpdate () {
		deltaTime += Time.fixedDeltaTime;
		if ( status == 1 && deltaTime > time ) {
			deltaTime = 0;
			for ( int i = 0; i < layer2.Length; i++ ) {
				if ( ( layer2 [ i ].transform.position - transform.position ).sqrMagnitude < l2b * l2b ) {
					layer2 [ i ].SetActive ( true );
				} else {
					layer2 [ i ].SetActive ( false );
				}
			}
		} 
	}

	public void MoveLayers ( int a1 ) {
		status += a1;
		if ( status < 0 ) { status = 0; }
		if ( status == 0 ) {
			for( int i = 0; i < layer2.Length; i++ ) {
				layer2 [ i ].SetActive ( false );
			}
			for ( int i = 0; i < layer1.Length; i++ ) {
				layer1 [ i ].SetActive ( true );
			}
		}
		if ( status >= 1 ) {
			for ( int i = 0; i < layer1.Length; i++ ) {
				layer1 [ i ].SetActive ( false );
			}
		}
	}
}
