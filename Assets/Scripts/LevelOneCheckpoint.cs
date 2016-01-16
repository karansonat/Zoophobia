using UnityEngine;
using System.Collections;

public class LevelOneCheckpoint : MonoBehaviour {
    Flow _flow;
    public GameObject _guard;
	// Use this for initialization
	void Start () {
	   _flow = GameObject.Find("Game").GetComponent<Flow>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            _guard.GetComponent<GuardAI>()._isSlothOutOfHisCage = true;
            _flow._playerMonkey.GetComponent<Monkey>().Scream();
            Destroy(gameObject);    
        }
    }
}
