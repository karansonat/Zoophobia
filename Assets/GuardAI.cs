using UnityEngine;
using System.Collections;

public class GuardAI : MonoBehaviour {
    public float speed = 5f;
    private float _noticedTimeCounter = 0;
    private Vector3 _lastSoundPosition;
    private GUARD_AI _guardFlow = GUARD_AI.SLEEP;
    
    public enum GUARD_AI{
        SLEEP,
        IDLE,
        ON_ALERT,
        NOTICED,
    }

	// Use this for initialization
	void Start () {
	   GameController.SoundHeard += onSoundHeard;
	}
	
	// Update is called once per frame
	void Update () {
	   GuardActinFlow();
	}
    
    private void GuardActinFlow(){
        switch (_guardFlow){
            case GUARD_AI.SLEEP:
                Sleep();
                break;
            case GUARD_AI.IDLE:
                Idle();
                break;
            case GUARD_AI.ON_ALERT:
                onAlert();
                break;
            case GUARD_AI.NOTICED:
                Noticed();
                break;
        }
    }
    private void Sleep(){
        Debug.Log("GuardAI::Sleep");
    }
    private void Idle(){
        Debug.Log("GuardAI::Idle");
    }
    private void onAlert(){
        Debug.Log("GuardAI::onAlert");
        MoveToSoundPosition();
    }
    private void Noticed(){
        Debug.Log("GuardAI::Noticed");
        _noticedTimeCounter += Time.deltaTime;
        if(_noticedTimeCounter >= 5.0f){
            goToSleep();
            _noticedTimeCounter = 0;
        }
    }
    
    private void MoveToSoundPosition(){
        if(Vector3.Distance(transform.position, _lastSoundPosition) > 0.1f){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_lastSoundPosition.x, transform.position.y, _lastSoundPosition.z), step);    
        }
    }
    
    private void onSoundHeard(Vector3 soundPosition){
        Debug.Log("GuardAI::SoundHeard");
        _lastSoundPosition = soundPosition;
        if(_guardFlow == GUARD_AI.SLEEP){_guardFlow = GUARD_AI.NOTICED;}
        else if(_guardFlow == GUARD_AI.NOTICED){_guardFlow = GUARD_AI.ON_ALERT;}
    }
    private void goToSleep(){
        _noticedTimeCounter = 0;
        _guardFlow = GUARD_AI.SLEEP;
    }
}
