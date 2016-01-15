using UnityEngine;
using System.Collections;
using System;

public class ThrowableObject : MonoBehaviour, IUsableObject {
    GameController _gameCon;
    public GameObject aimIndicatorPrefab;
    private bool _isItemReadyToUse = true;
    private int _actionState = -1;
    private GameObject _aimIndicator;
    private GameObject _activePlayer;
    private bool _isObjectThrown = false;
	// Use this for initialization
	void Start () {
	   _gameCon = GameObject.Find("Game").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void MakeAction(){
        switch (_actionState){
            case (int)THROW_ACTION.EQUIPPED:
                Aim();
                break;
            case (int)THROW_ACTION.READY:
                Throw();
                break;
        }
    }
    public void Equip(GameObject activePlayer){
        Debug.Log("Equip");
        _activePlayer = activePlayer;
        //TODO: objecyi aktif et playerin uzerine konumlandir
        gameObject.transform.parent = _activePlayer.transform;
        _actionState = (int)THROW_ACTION.EQUIPPED;
        //croshair icin konum belirle.
        _aimIndicator = GameObject.Instantiate(aimIndicatorPrefab, _activePlayer.transform.position, aimIndicatorPrefab.transform.rotation) as GameObject;
        _aimIndicator.transform.parent = _activePlayer.transform;
        _isItemReadyToUse = true;
        Debug.Log("Equip::end");
    }
    
    private void Aim(){
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow)){
            _aimIndicator.transform.Rotate(new Vector3(0,0,5));
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
            _aimIndicator.transform.Rotate(new Vector3(0,0,-5));
        }
        if(Input.GetKeyDown(KeyCode.F)){
            _actionState = (int)THROW_ACTION.READY;
        }
    }
    private void Throw(){
        Debug.Log("PickableObject::Throw::start");
        if(_isItemReadyToUse){
            GameObject clone = GameObject.Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
            clone.transform.position = _activePlayer.transform.position;
            clone.SetActive(true);
            Debug.Log("PickableObject::Throw");
            Vector3 direction = getDirectionBetweenTwoPoint(_activePlayer.transform.position, _aimIndicator.transform.GetChild(0).transform.position);
            clone.GetComponent<Rigidbody2D>().AddForce(direction*500);   
            _isItemReadyToUse = false;
            Destroy(_aimIndicator);
            clone.transform.parent = null;
            _actionState = -1;
        } 
    }
    private Vector3 getDirectionBetweenTwoPoint(Vector3 position1, Vector3 position2){
        Vector3 direction;
        var heading = position2 - position1;
        var distance = heading.magnitude;
        direction = heading/distance;
        return direction;
    }
    
    public void DestroyAimIndicator(){
        Destroy(_aimIndicator);
    }

    public void Cancel(){
        DestroyAimIndicator();
    }
    
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "LevelObstacles" && !_isObjectThrown)
            MakeSound();
    }
    
    private void MakeSound(){
        _gameCon.onSoundHeard(transform.position);
        _isObjectThrown = true;
        DestroySelfWithDelay();
    }
    
    private void DestroySelfWithDelay(){
        Destroy(gameObject, 3.0f);
    }
}
