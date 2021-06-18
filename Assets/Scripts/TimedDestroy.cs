using UnityEngine;

public class TimedDestroy : MonoBehaviour {
    public  float   timeRemaining;

    void Update () {
        timeRemaining -= Time.deltaTime;
        if( timeRemaining < 0 ) {
            gameObject.SetActive ( false );
		}
    }
}
