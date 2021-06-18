using UnityEngine;
/* PROJECT NOSEDIVE */

public class Aim : MonoBehaviour {
    public  Transform   aRigY;
    public  Transform   aRigZ;
    public  Transform   aRigX;

    public  float       pFollowStrength;
    private Vector3     dVar;

    float t1;

    public  bool        locked = false;

    void Update () {
        if ( locked ) { return; }

        dVar.x += Input.GetAxis ( "Mouse Y" );
        dVar.y += Input.GetAxis ( "Mouse X" );

        t1 = GetTrueXAngle ( aRigX.localRotation.eulerAngles );
        //Debug.Log ( t1 + " " + dVar.x + " " + Mod ( dVar.x + t1 ) );

        if ( Mod ( -dVar.x + t1 ) > 90 ) {
            dVar.x = -Sign ( dVar.x ) * ( Mod ( t1 ) - 90 );
        }

        aRigY.Rotate ( 0, dVar.y * pFollowStrength, 0, Space.Self );
        aRigZ.Rotate ( 0, 0, dVar.z * pFollowStrength, Space.Self );
        aRigX.Rotate ( dVar.x * pFollowStrength, 0, 0, Space.Self );

        dVar -= dVar * pFollowStrength;
    }

    public void FSSetLean( float a1 ) {
        if ( a1 < GetTrueZAngle ( aRigZ.localRotation.eulerAngles ) ) {
            dVar.z = +Mod ( GetTrueZAngle ( aRigZ.localRotation.eulerAngles ) - a1 );
        } else {
            dVar.z = -Mod ( GetTrueZAngle ( aRigZ.localRotation.eulerAngles ) - a1 );
        }
    }

    float GetTrueZAngle( Vector3 a1 ) {
        float t1 = a1.x;
        a1.x = a1.z;
        a1.z = a1.x;
        return GetTrueXAngle ( a1 );
    }

    float GetTrueXAngle( Vector3 a1 ) {
        if ( a1.y == 180 ) {
            if ( a1.x <= 90 ) {
                a1.x = -180 + a1.x;
            } else {
                a1.x = a1.x - 180;
            }
        } else {
            if ( a1.x <= 90 ) {
                a1.x = -a1.x;
            } else {
                a1.x = 360 - a1.x;
            }
        }
        return a1.x;
    }
    int Sign( float a1 ) {
        if ( a1 < 0 )
            return -1;
        return 1;
    }
    float Mod( float a1 ) {
        if ( a1 < 0 ) { return -a1; }
        return a1;
    }
}
