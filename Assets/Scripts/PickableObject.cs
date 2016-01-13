using UnityEngine;
using System.Collections;

public enum THROW_ACTION{
    EQUIPPED,
    READY,
}
public class PickableObject : MonoBehaviour, IActionObject {
    GameController _gameController;
    Inventory _inventory;
    GameObject _game;
    public GameObject inventoryObjectPrefab;
    public string infoText = "ex: Press E for action";
    public bool destroyAfterPickup = false;
    private MeshRenderer _infoText;
    private bool _isInputAcceptable = false;
    private bool _isActionDescriptorShowing = false;
    
    
	void Start () {
	   _game = GameObject.Find("Game");
       _gameController = _game.GetComponent<GameController>();
       _inventory = _game.GetComponent<Inventory>();
       _infoText = gameObject.GetComponentInChildren<MeshRenderer>();
       _infoText.gameObject.GetComponent<TextMesh>().text = infoText;
       _infoText.enabled = false;
	}
    
	void Update () {
        if(_isInputAcceptable){
            if(Input.GetKeyDown(KeyCode.E)){
                AddToPlayerInventory();
            }
        }
    }
    private void AddToPlayerInventory(){
        if(_inventory.hasEmptySlot()){
            GameObject item = GameObject.Instantiate(inventoryObjectPrefab, new Vector3(0,0,0), gameObject.transform.rotation) as GameObject;
            item.SetActive(false);
            _game.GetComponent<Inventory>().AddItemToInventory(item);    
        }
        if(destroyAfterPickup){
            DestroyObjectAfterPickedUp();
        }
    }
    public void ShowActionDescription(){
        _isInputAcceptable = true;
        _infoText.enabled = true;
    }
    public void HideActionDescription(){
        _isInputAcceptable = false;
        _infoText.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("PickableObject::OnTriggerEnter");
            if(!_isActionDescriptorShowing){
                ShowActionDescription();
                _isActionDescriptorShowing = true;   
            }   
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("PickableObject::OnTriggerExit");
            if(_isActionDescriptorShowing){
                HideActionDescription();
                _isActionDescriptorShowing = false;
            }   
        }
    }
    
    private void DestroyObjectAfterPickedUp(){
        Destroy(gameObject);
    }
}
