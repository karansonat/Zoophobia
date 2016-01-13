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
        Destroy(GetComponent<BoxCollider2D>());
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
