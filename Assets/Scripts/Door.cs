using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public void Open(){
        Debug.Log("Door::Open");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("DoorOpenTrigger");
    }
    public void Close(){
        Debug.Log("Door::Close");
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Animator>().SetTrigger("DoorCloseTrigger");
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Door::OnTriggerEnter2D");
        if(col.tag == "Player"){
            col.gameObject.GetComponent<PlayerActionController>().setInteractionObject(gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col){
        Debug.Log("Door::OnTriggerExit2D");
        if(col.tag == "Player"){
            col.gameObject.GetComponent<PlayerActionController>().setInteractionObject(null);
        }
    }
}
