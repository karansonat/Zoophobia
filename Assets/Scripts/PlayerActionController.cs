using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerActionController : MonoBehaviour {
    GameObject _game;
    Inventory _inventory;
    GameController _gameController;
    Flow _flow;
	public GameObject selectedIcon1;
	public GameObject selectedIcon2;
	public GameObject selectedIcon3;
    public GameObject _equippedItem;
    public GameObject _actionObject;
    private GameObject _interactionObject;
    
    [HideInInspector]
    public bool isReadyForAction = false;
    
	// Use this for initialization
	void Start () {
	   _game = GameObject.Find("Game");
       _gameController = _game.GetComponent<GameController>();
       _inventory = _game.GetComponent<Inventory>();
       _flow = _game.GetComponent<Flow>();
	}
	
	// Update is called once per frame
	void Update () {
       EquipFromInventory();
       Interact();
    //    if(isReadyForAction){
    //        Debug.Log("isReadyForAction = true");
           
    //    }
	}
    
    private void Interact(){
        if(_equippedItem){
            _equippedItem.GetComponent<IUsableObject>().MakeAction();    
        }
        // if(_actionObject){
        //     _actionObject.GetComponent<IActionObject>().MakeAction();
        // }
    }
    
    private void EquipFromInventory(){
        try{
            if(Input.GetKeyDown(KeyCode.Alpha1)){
				selectedIcon1.SetActive(true);
				selectedIcon2.SetActive(false);
				selectedIcon3.SetActive(false);
                Debug.Log("EquipFromInventory::Slot1");
                if(_equippedItem){_equippedItem.GetComponent<IUsableObject>().Cancel();}
                _equippedItem = _inventory.getItemFromInventory(0);
                _equippedItem.SetActive(true);
                if(_equippedItem.GetComponent<ThrowableObject>() != null ? _equippedItem && gameObject.name == "Sloth"  : _equippedItem){
                    _gameController.setActivePlayer(gameObject);
                    _equippedItem.GetComponent<IUsableObject>().Equip(gameObject);
                    _equippedItem.SetActive(false);
                }else{
                    _equippedItem.SetActive(false);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2)){
				selectedIcon1.SetActive(false);
				selectedIcon2.SetActive(true);
				selectedIcon3.SetActive(false);
				Debug.Log("EquipFromInventory::Slot2");
                if(_equippedItem){_equippedItem.GetComponent<IUsableObject>().Cancel();}
                _equippedItem = _inventory.getItemFromInventory(1);
                if(_equippedItem){
                    _gameController.setActivePlayer(gameObject);
                    _equippedItem.GetComponent<IUsableObject>().Equip(gameObject);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3)){
				selectedIcon1.SetActive(false);
				selectedIcon2.SetActive(false);
				selectedIcon3.SetActive(true);
				if(_equippedItem){_equippedItem.GetComponent<IUsableObject>().Cancel();}
                Debug.Log("EquipFromInventory::Slot3");
                _equippedItem = _inventory.getItemFromInventory(2);
                if(_equippedItem){
                    _gameController.setActivePlayer(gameObject);
                    _equippedItem.GetComponent<IUsableObject>().Equip(gameObject);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Escape)){
				selectedIcon1.SetActive(false);
				selectedIcon2.SetActive(false);
				selectedIcon3.SetActive(false);
				Debug.Log("EquipFromInventory::Cancel");
                _equippedItem.GetComponent<IUsableObject>().Cancel();
                _equippedItem = null;
            }
            else if(Input.GetKeyDown(KeyCode.Backspace)){
                Debug.Log("EquipFromInventory::unEquip");
                unEquip();
            }
        }
        catch (System.Exception){
            _equippedItem = null;
        }
    }
    public void unEquip(){
        Debug.Log("PlayerActionController::unEquip");
		_inventory.remove(_inventory.getInventory().IndexOf(_equippedItem));
        _inventory.getInventory()[_inventory.getInventory().IndexOf(_equippedItem)] = null;

        _equippedItem.transform.parent = null;
        _equippedItem.GetComponent<IUsableObject>().Cancel();
        _equippedItem = null;
        
    }
    
    public GameObject getInteractionObject(){
        return _interactionObject;
    }
    public void setInteractionObject(GameObject interactionObj){
        _interactionObject = interactionObj;
    }
    
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            _flow.isCharactersCloseToEachOther = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            _flow.isCharactersCloseToEachOther = false;
        }
    }
    
    public void digAnimFinished(){
        GameObject[] array = GameObject.FindGameObjectsWithTag("DiggingArea");
        foreach (GameObject item in array){
            DigArea da = item.GetComponent<DigArea>();
            DigArea2 da2 = item.GetComponent<DigArea2>();
            if(da){
                da.completeDiggingAction();
            }else{
                da2.completeDiggingAction();
            }
        }            

    }
}
