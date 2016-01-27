using UnityEngine;
using System.Collections;
using System;



public class DigArea : MonoBehaviour, IActionObject, IUsableObject {    
    private MeshRenderer _infoTextMeshRenderer;
    GameObject _game;
    Flow _flow;
    public GameObject ExitArea; 
    private bool _isInputAcceptable = false;
    public bool _isRoadDigged = false;
    private bool _isActionDescriptorShowing = false;
    
    public string infoText = "ex: Press E for action";
	// Use this for initialization
	void Start () {
        
	   _game = GameObject.Find("Game");
       _flow = _game.GetComponent<Flow>();
       
       _infoTextMeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
       _infoTextMeshRenderer.sortingLayerID = SortingLayer.layers[SortingLayer.layers.Length-1].id;
       
       _infoTextMeshRenderer.gameObject.GetComponent<TextMesh>().text = infoText;
       
       HideActionDescription();
	}
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetKeyDown(KeyCode.O)){
           completeDiggingAction();
       }
       if(_isActionDescriptorShowing){
            if(Input.GetKeyDown(KeyCode.E)){
                // StartCoroutine(MakeActionRoutine());
                MakeAction();
            }   
        }
	}
    IEnumerator MakeActionRoutine(){
        ExitArea.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ExitArea.SetActive(true);
    }
    
    void OnTriggerStay2D(Collider2D col){
        Debug.Log("------DigArea::OnTriggerEnter " + col.gameObject.name + " " + _flow.getActiveCharacterObject().name);
        if(col.gameObject.tag == "Player"){
            if(col.gameObject == _flow.getActiveCharacterObject()){
                Debug.Log("PickableObject::OnTriggerEnter");
                if(!_isActionDescriptorShowing){
                    ShowActionDescription();
                    _isActionDescriptorShowing = true;   
                }
            }   
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject == _flow.getActiveCharacterObject()){
            Debug.Log("PickableObject::OnTriggerExit");
            if(_isActionDescriptorShowing){
                HideActionDescription();
                _isActionDescriptorShowing = false;
            }   
        }
    }

    public void ShowActionDescription(){
        _isInputAcceptable = true;
        _infoTextMeshRenderer.enabled = true;
    }

    public void HideActionDescription(){
        _isInputAcceptable = false;
        _infoTextMeshRenderer.enabled = false;
    }

    public void MakeAction(){
        Debug.Log("DigArea::MakeAction");
        if(_isRoadDigged){
            Debug.Log("DigArea::MakeAction::GameObject.name = " +gameObject.name);
            _flow.getActiveCharacterObject().transform.position = ExitArea.transform.position;
        }else{
            //TODO: kazma
            Debug.Log("DigArea::crawl");
        }
    }
    public void completeDiggingAction(){
        _isRoadDigged = true;
        infoText = "Press E for crawl";
        setRoad();
        _infoTextMeshRenderer.gameObject.GetComponent<TextMesh>().text = infoText;
    }

    public void Equip(GameObject activePlayer){
    }

    public void Cancel(){
    }
    
    private void setRoad(){
        ExitArea.GetComponent<DigArea2>()._isRoadDigged = true;
        ExitArea.GetComponent<DigArea2>().infoText = infoText;
        ExitArea.GetComponentInChildren<TextMesh>().text = infoText;
    }
}
