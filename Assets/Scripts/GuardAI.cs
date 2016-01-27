using UnityEngine;

public class GuardAI : MonoBehaviour {
    Animator anim;
    private GameObject[] DiggingAreas;
    public Sprite SleepingGuard;
    public Sprite IdleGuard;
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
    private Vector3 _slothInitialPosition;
    private Vector3 _lastSoundPosition;
    private GUARD_AI _guardFlow = GUARD_AI.SLEEP;
    
    public enum GUARD_AI{
        SLEEP,
        BACK_TO_DESK,
        NOTICED,
        ON_ALERT,
        ACTION,
    }
    void Awake(){
        DiggingAreas = GameObject.FindGameObjectsWithTag("DiggingArea");
        foreach(GameObject go in DiggingAreas){
            go.SetActive(false);
        }
    }
	// Use this for initialization
	void Start () {
        _slothInitialPosition = _sloth.transform.position;
        _initialPosition = transform.position;
        anim = gameObject.GetComponent<Animator>();
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
            anim.SetTrigger("sleep");
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
        gameObject.GetComponent<SpriteRenderer>().sprite = IdleGuard;
        Debug.Log("GuardAI::Noticed");
        _noticedTimeCounter += Time.deltaTime;
        if(_isSlothOutOfHisCage){
            _guardFlow = GUARD_AI.ON_ALERT;
            _noticedTimeCounter = 0;
            anim.SetTrigger("walk");
        }
        else if(_noticedTimeCounter >= 5.0f){
            goToSleep();
            _noticedTimeCounter = 0;
            anim.SetTrigger("sleep");
        }
    }
    
    private void MoveTo(Vector3 pos){
        if(Mathf.Abs(transform.position.x - pos.x) > 0.5f){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, transform.position.y, pos.z), step);    
        }else{
            _isSlothOutOfHisCage = false;
            _moveToTargetReached = true;
        }
    }
    
    private void onSoundHeard(Vector3 soundPosition){
        Debug.Log("GuardAI::SoundHeard");
        _lastSoundPosition = soundPosition;
        if(_guardFlow == GUARD_AI.SLEEP){_guardFlow = GUARD_AI.NOTICED;anim.SetTrigger("wakeUp");}
        else if(_guardFlow == GUARD_AI.NOTICED){_guardFlow = GUARD_AI.ON_ALERT;anim.SetTrigger("walk");}
    }
    private void goToSleep(){
        _noticedTimeCounter = 0;
        _guardFlow = GUARD_AI.SLEEP;
        gameObject.GetComponent<SpriteRenderer>().sprite = SleepingGuard;
    }
    
    void OnTriggerEnter2D(Collider2D col){
        if(_guardFlow != GUARD_AI.SLEEP && _isSlothOutOfHisCage &&col.tag == "Player"){
            col.gameObject.transform.position = _slothInitialPosition;
            _isSlothOutOfHisCage = false;
            _guardFlow = GUARD_AI.BACK_TO_DESK;
            GameObject doorGO = GameObject.FindGameObjectWithTag("Door");
            Door door = doorGO.GetComponent<Door>();
            door.Close();
            Camera.main.gameObject.GetComponent<CameraControl>().checkPointReached = true;
            foreach(GameObject go in DiggingAreas){
                go.SetActive(true);
            }
        }
    }
    
}
