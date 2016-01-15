using UnityEngine;
using System.Collections;

public delegate void SoundHeardEventHandler(Vector3 soundPosition);
public class GameController : MonoBehaviour {
    public static event SoundHeardEventHandler SoundHeard;
    // private GameObject[] players = new GameObject[3];
    private GameObject _activePlayer;
	// Use this for initialization
	void Start () {
        // players[0] = GameObject.Find("Sloth");
        // players[0] = GameObject.Find("Monkey");
        // players[0] = GameObject.Find("SlothCarriesMonkey");
        // players[0] = GameObject.Find("MonkeyCarriesSloth");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void setActivePlayer(GameObject activePlayer){
        // foreach (GameObject player in players){
        //     player.transform.tag = "Player";
        // }
        _activePlayer = activePlayer;
        Debug.Log("GameController::setActivePlayer::setted"); 
    }
    
    public GameObject getActivePlayer(){
        Debug.Log("GameController::getActivePlayer::getted");
        return _activePlayer;
    }
    public virtual void onSoundHeard( Vector3 soundPosition){
        if(SoundHeard != null){
            SoundHeard(soundPosition);
        }
    }
}
