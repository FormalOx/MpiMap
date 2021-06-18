using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UII : MonoBehaviour {
    public  Transform       rigX;
    public NameLeeter nl;

    public  UIManager       um;
    public  Animator        txtA;
    public  Text            txtT;
    public  InputField      txtD;

    List<Unfold>    ufv;

    public  float   viewAngle;
    public  float   deltaAngle;

    int skip    = 10;
    int deltaS  = 0;

    public      Camera          topCamera;
    private     CameraMovement  cm;
    private     Aim             am;

    [System.Serializable]
	public struct Sphere {
        public Vector3     center;
        public float       sqMag;
	    public Sphere( Vector3 a1, float a2 ) {
            center = a1;
            sqMag = a2;
		}   
    }

    public     List<Sphere>   tTgts;
    private    float           magSq;
    public     float           sizeTgt;
    private    float           sizeDelta;

    private    bool             inMap;

    void Start () {
        deltaAngle = 180;

        ufv = new List<Unfold> ( 8 );
        tTgts = new List<Sphere> ();

        am = GetComponent<Aim> ();
        cm = GetComponent<CameraMovement> ();

        sizeTgt = topCamera.orthographicSize;
        sizeDelta = sizeTgt;
    }

	private void Update () {
        if ( Input.GetKeyDown ( KeyCode.M ) ) {
            if ( !inMap ) {
                if ( !inMap ) {
                    topCamera.gameObject.SetActive ( true );
                    am.enabled = false;
                }
                inMap = true;
            } else {
                if ( inMap ) {
                    topCamera.gameObject.SetActive ( false );
                    am.enabled = true;
                }
                inMap = false;
            }
        }
	}

	private void FixedUpdate () {
        deltaS++;
        if( deltaS > skip ) {
            RecheckDelta ();
            deltaS = 0;
		}
        if( inMap ) {
            UpdateOrthoSize ();
            topCamera.transform.position = transform.position + Vector3.up * 100;
		}
    }

    private void UpdateOrthoSize () {
        if ( tTgts.Count == 0 ) { topCamera.orthographicSize = sizeTgt; return; }
        topCamera.orthographicSize = sizeTgt + ( sizeDelta - sizeTgt ) / ( tTgts [ 0 ].sqMag / ( transform.position - tTgts [ 0 ].center ).sqrMagnitude );
    }

    public void Group( Vector3 a1, int a2 ) {
        UpdateOrthoSize ();
        if ( a2 == -1 ) {
            tTgts.RemoveAt ( 0 );
            sizeTgt *= 2;
            sizeDelta = topCamera.orthographicSize;
        } else {
            tTgts.Insert ( 0, new Sphere ( a1, ( transform.position - a1 ).sqrMagnitude ) );
            sizeTgt /= 2;
            sizeDelta = topCamera.orthographicSize;
		}
	}

	public  void Add( Unfold a1 ) {
        if( ufv.Count < 8 && !ufv.Contains( a1 ) ) {
            ufv.Add ( a1 );
            RecheckDelta ();
		}
	}

    public  void Remove( Unfold a1 ) {
        if( ufv.Count > 0 && ufv.Contains( a1 ) ) {
            ufv.Remove ( a1 );
            RecheckDelta ();
        }
	}

    void RecheckDelta () {
        deltaAngle = 180;
        float   delta1;
        int     delta2 = 0;
        for( int i = 0; i < ufv.Count; i++ ) {
            delta1 = Vector3.Angle ( rigX.forward, ufv [ i ].transform.position - transform.position );
            if ( delta1 < deltaAngle ) {
                deltaAngle = delta1;
                delta2 = i;
            }
		}
        if( deltaAngle <= viewAngle ) {
            txtT.text = ufv [ delta2 ].nme;
            txtD.text = newLineReplace( ufv [ delta2 ].desc );
            um.SetText ( ufv [ delta2 ].nme, newLineReplace( ufv [ delta2 ].desc ) );
            txtA.SetBool ( "Ease", true );
        } else {
            txtA.SetBool ( "Ease", false );
		}
    }
     private string newLineReplace(string InText)
     {
         bool newLinesRemaining = true;
         
         while(newLinesRemaining)
         {
             int CIndex = InText.IndexOf("\\n"); // Gets the Index of "\n" 
             
             
             if(CIndex == -1)
             {
                 newLinesRemaining = false;
             }
             else
             {
                 InText = InText.Remove(CIndex, 2); // Removes "\n from original String"
                 InText = InText.Insert(CIndex, "\n"); // Adds the actual New Line symbol
             }
         }
         
         return InText;
     }
}
