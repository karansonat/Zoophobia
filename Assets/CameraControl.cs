using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public bool checkPointReached = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(checkPointReached){
            setCameraSize();
        }
	
	}
    
    private void setCameraSize(){
        if(Camera.main.orthographicSize < 4.2f){
            Camera.main.orthographicSize += 0.01f;
        }else{
            checkPointReached = false;
        }
    }
}
