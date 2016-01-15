using UnityEngine;

public class GuardAI : MonoBehaviour {
    public GameObject _monkey;
    public GameObject _sloth;
    public GameObject _monkeyCarrySloth;
    public GameObject _slothCarryMonkey;
   
    [HideInInspector]
    public bool _isSlothOutOfHisCage = false;
    private bool _moveToTargetReached = false;
    public float speed = 10f;
    private float _noticedTimeCounter = 0;
    private Vector3 _initialPosition;
    private Vector3 _lastSoundPosition;
    private GUARD_AI _guardFlow = GUARD_AI.SLEEP;
    
    public enum GUARD_AI{
        SLEEP,
        BACK_TO_DESK,
        NOTICED,
        ON_ALERT,
        ACTION,
    }

	// Use this for initialization
	void Start () {
        _initialPosition = transform.position;
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
            case GUARD_AI.BACK_TO_DESK:
                BackToDesk();
                break;
            case GUARD_AI.NOTICED:
                Noticed();
                break;
            case GUARD_AI.ON_ALERT:
                onAlert();
                break;
            case GUARD_AI.ACTION:
                Noticed();
                break;
            
        }
    }
    private void Sleep(){
        Debug.Log("GuardAI::Sleep");
    }
    private void BackToDesk(){
        Debug.Log("GuardAI::BackToDesk");
        if(transform.position != _initialPosition){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_initialPosition.x, transform.position.y, _initialPosition.z), step);    
        }else{
            goToSleep();
        }
    }
    private void onAlert(){
        Debug.Log("GuardAI::onAlert");
        if(_isSlothOutOfHisCage){
            Debug.Log("GuardAI::_isSlothOutOfHisCage::true");
            MoveTo(_sloth.transform.position);
        }else{
            Debug.Log("GuardAI::_isSlothOutOfHisCage::false");
            if(_moveToTargetReached){
                Debug.Log("GuardAI::_moveToTargetReached::true");
                _moveToTargetReached = false;
                _guardFlow = GUARD_AI.BACK_TO_DESK;
            }else{
                Debug.Log("GuardAI::_moveToTargetReached::false");
                MoveTo(_lastSoundPosition);    
            }
            
        }
    }
    private void Noticed(){
        Debug.Log("GuardAI::Noticed");
        _noticedTimeCounter += Time.deltaTime;
        if(_isSlothOutOfHisCage){
            _guardFlow = GUARD_AI.ON_ALERT;
            _noticedTimeCounter = 0;
            _isSlothOutOfHisCage = false;
        }
        else if(_noticedTimeCounter >= 5.0f){
            goToSleep();
            _noticedTimeCounter = 0;
        }
    }
    
    private void MoveTo(Vector3 position){
        if(Mathf.Abs(transform.position.x - position.x) > 0.5f){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x, transform.position.y, position.z), step);    
        }else{
            _moveToTargetReached = true;
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
