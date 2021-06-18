using UnityEngine;

public class Autoaimer : MonoBehaviour {
    public Transform[]  labels;

    void Update () {
        for ( int i = 0; i < labels.Length; i++ ) {
            labels [ i ].LookAt ( transform );
        }
    }
}
