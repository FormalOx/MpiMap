using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Transform   alpha;
    private Aim         am;
    public UIManager    um;

    public  Vector3     mxAcc;
    public  Vector3     mxV;

    private Vector3     delta1;

    private Vector3     delta2;
    private Vector3     delta3;

    private float       maxDistance = 200;
    private bool        intState;

    public bool         locked;

    private void Start () {
        am = GetComponent<Aim> ();
        alpha = transform;
        delta1 = Vector3.zero;
        delta2 = Vector3.zero;
        delta3 = Vector3.zero;
        intState = Cursor.visible;
    }

	void Update () {
        //Debug.Log ( delta1 + " " + delta2 + " " + delta3 );
        if ( Increasing ( delta2.x, Input.GetAxis ( "Horizontal" ) ) ) {
            delta1.x = F ( Input.GetAxis ( "Horizontal" ), 0.1f, mxAcc.x, mxV.x, delta3.x );
        } else {
            if ( delta3.x != 0 ) { delta1.x = -delta3.x / ( 2 * Time.deltaTime ); } else { delta1.x = 0; }
        }

        if ( Increasing ( delta2.z, Input.GetAxis ( "Vertical" ) ) ) {
            delta1.z = F ( Input.GetAxis ( "Vertical" ), 0.1f, mxAcc.z, mxV.z, delta3.z );
        } else {
            if ( delta3.z != 0 ) { delta1.z = -delta3.z / ( 2 * Time.deltaTime ); } else { delta1.z = 0; }
        }

        delta3.y = Input.GetAxis ( "Jump" ) * mxAcc.y * Time.deltaTime;
        
        delta3 += delta1 * Time.deltaTime;

        if ( delta3.sqrMagnitude > 0.1f ) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            am.locked = false;
            intState = false;
        } else {
            if ( Input.GetKeyDown ( KeyCode.Escape ) ) {
                intState = !( Cursor.lockState == CursorLockMode.Locked ? false : true );
                Cursor.visible = intState;
                Cursor.lockState = intState ? CursorLockMode.None : CursorLockMode.Locked;
                am.locked = intState;
                if ( !intState ) {
                    um.BigControl ( false );
                } 
            }
        }
        if ( !locked ) {
            alpha.Translate ( delta3 );
        }
    }

	private void FixedUpdate () {
		if( transform.position.sqrMagnitude > maxDistance * maxDistance ) {
            delta3 = Vector3.zero;
            transform.position = transform.position.normalized * maxDistance;
		}
	}

	float F( float a1, float aL, float aM, float aMV, float aV ) {
        aV = aV < 0 ? -aV : aV;
        if( aV <= 0.1f ) {
            return a1 * aL;
		}
        if ( aV > aMV ) {
            return aM * Sign ( a1 );
        } else {
            return a1 * ( aL + ( aM - aL ) * ( aMV / aV ) );
		}
	}

    int Sign ( float a1 ) {
        if ( a1 < 0 ) return -1;
        return 1;
    }

    bool Increasing ( float a1, float a2 ) {
        a1 = a1 < 0 ? -a1 : a1;
        a2 = a2 < 0 ? -a2 : a2;
        if ( a2 - a1 >= 0 && a2 != 0 ) return true;
        return false;
    }
}
