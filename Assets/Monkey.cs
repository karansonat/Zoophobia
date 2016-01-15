using UnityEngine;
using System.Collections;

public class Monkey : MonoBehaviour {
    GameController _gameCon;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void Scream(){
        _gameCon = GameObject.Find("Game").GetComponent<GameController>();
        //TODO(sonat): Play monkey sound here.
        _gameCon.onSoundHeard(transform.position);
    }
}
