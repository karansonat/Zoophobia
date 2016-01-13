using UnityEngine;
using System.Collections;

public enum THROW_ACTION{
    EQUIPPED,
    READY,
}
public class PickableObject : MonoBehaviour, IActionObject {
    GameController _gameController;
    PlayerActionController _actionController;
    Inventory _inventory;
    private GameObject _game;
    public GameObject aimIndicatorPrefab;
    public GameObject throwableObjectPrefab;
    private GameObject _aimIndicator;
    private GameObject _activePlayer;
    private bool _isInputAcceptable = false;
    private bool _isActionDescriptorShowing = false;
    private bool _isActionStarted = false;
    private bool _isItemReadyToUse = true;
    private int _actionState = -1;
    
    
	void Start () {
	   _game = GameObject.Find("Game");
       _gameController = _game.GetComponent<GameController>();
       _inventory = _game.GetComponent<Inventory>();
    //    aimIndicatorPrefab = Resources.Load("Prefabs/aimIndicator") as GameObject;
	}
    
	void Update () {
        if(_isInputAcceptable){
            if(Input.GetKeyDown(KeyCode.E)){
                AddToPlayerInventory();
            }
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
        _actionController = _activePlayer.GetComponent<PlayerActionController>();
        _isItemReadyToUse = true;
        Debug.Log("Equip::end");
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
    private void AddToPlayerInventory(){
        if(_inventory.hasEmptySlot()){
            GameObject item = GameObject.Instantiate(throwableObjectPrefab, new Vector3(0,0,0), gameObject.transform.rotation) as GameObject;
            item.SetActive(false);
            _game.GetComponent<Inventory>().AddItemToInventory(item);    
        }
        
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
            clone.GetComponent<Rigidbody2D>().AddForce(direction*1000);   
            
            _isItemReadyToUse = false;
            Destroy(_aimIndicator);
            clone.transform.parent = null;
            _actionState = -1;
        } 
    }
    public void ShowActionDescription(){
        _isInputAcceptable = true;
    }
    public void HideActionDescription(){
        _isInputAcceptable = false;
    }
    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("PickableObject::OnTriggerEnter");
        if(!_isActionDescriptorShowing){
            ShowActionDescription();
            _isActionDescriptorShowing = true;   
        }
        
    }
    void OnTriggerExit2D(Collider2D col){
        Debug.Log("PickableObject::OnTriggerExit");
        if(_isActionDescriptorShowing){
            HideActionDescription();
            _isActionDescriptorShowing = false;
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
}
