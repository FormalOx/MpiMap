using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject bigg;
    public GameObject smol;
    public GameObject   pnt;
    public Text title;
    public Text body;

    public Aim am;
    public CameraMovement mv;

	private void Start () {
        SetText ( "TEST", "TEST NAMBA 2 " );
	}

	public void SetText ( string a1, string a2 ) {
        title.text = a1;
        body.text = a2;
    }

    public void BigControl ( bool a1 ) {
        am.locked = a1; 
        mv.locked = a1;
        bigg.SetActive ( a1 );
        smol.SetActive ( !a1 );
        pnt.SetActive ( !a1 );
        if ( !a1 ) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
