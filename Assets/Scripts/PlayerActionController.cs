using UnityEngine;
using System.Collections;

public class PlayerActionController : MonoBehaviour {
    GameObject _game;
    Inventory _inventory;
    GameController _gameController;
    public GameObject _equippedItem;
    private GameObject _interactionObject;
    
    [HideInInspector]
    public bool isReadyForAction = false;
    
	// Use this for initialization
	void Start () {
	   _game = GameObject.Find("Game");
       _gameController = _game.GetComponent<GameController>();
       _inventory = _game.GetComponent<Inventory>();
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
    }
    
    private void EquipFromInventory(){
        try{
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                Debug.Log("EquipFromInventory::Slot1");
                if(_equippedItem){_equippedItem.GetComponent<IUsableObject>().Cancel();}
                _equippedItem = _inventory.getItemFromInventory(0);
                if(_equippedItem){
                    _gameController.setActivePlayer(gameObject);
                    _equippedItem.GetComponent<IUsableObject>().Equip(gameObject);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2)){
                Debug.Log("EquipFromInventory::Slot2");
                if(_equippedItem){_equippedItem.GetComponent<IUsableObject>().Cancel();}
                _equippedItem = _inventory.getItemFromInventory(1);
                if(_equippedItem){
                    _gameController.setActivePlayer(gameObject);
                    _equippedItem.GetComponent<IUsableObject>().Equip(gameObject);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3)){
                if(_equippedItem){_equippedItem.GetComponent<IUsableObject>().Cancel();}
                Debug.Log("EquipFromInventory::Slot3");
                _equippedItem = _inventory.getItemFromInventory(2);
                if(_equippedItem){
                    _gameController.setActivePlayer(gameObject);
                    _equippedItem.GetComponent<IUsableObject>().Equip(gameObject);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Escape)){
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
}
