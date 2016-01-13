using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public List<GameObject> inventory = new List<GameObject>();
    public List<GameObject> getInventory(){
        return inventory;
    }
    public GameObject getItemFromInventory(int index){
        return inventory[index];
    }

	// Use this for initialization
	void Start () {
	   for(int i = 0; i < 3; i++){
           inventory.Add(null);
       }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void AddItemToInventory(GameObject item){
        Debug.Log("Inventory::AddItemToInventory::Started");
        foreach (GameObject obj in inventory){
            if(!obj){
                Debug.Log("Inventory::AddItemToInventory::SlotFounded");
                inventory[inventory.IndexOf(obj)] = item;
                return;
            }
        }
    }
    public void DeleteItemFromInventory(GameObject item){
        inventory.Remove(item);
    }
    public bool hasEmptySlot(){
        foreach (GameObject obj in inventory){
            if(!obj){
                return true;
            }
        }
        return false;
    }
}
