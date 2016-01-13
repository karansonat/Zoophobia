using UnityEngine;
using System.Collections;
using System;

public class Key : MonoBehaviour, IUsableObject{
    private bool _isEquipped = false;
    private GameObject _activePlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void MakeAction(){
        if(Input.GetKeyDown(KeyCode.F)){
            if(_isEquipped){
                GameObject interactObject = _activePlayer.GetComponent<PlayerActionController>().getInteractionObject();
                if(interactObject && interactObject.GetComponent<Door>()){
                    interactObject.GetComponent<Door>().Open();
                    _activePlayer.GetComponent<PlayerActionController>().unEquip();
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Equip(GameObject activePlayer){
        Debug.Log("Key::Equip");
        _isEquipped = true;
        _activePlayer = activePlayer;
    }

    public void Cancel(){
        _activePlayer = null;
        _isEquipped = false;
    }
}
