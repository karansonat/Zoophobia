using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public Sprite rockIcon;
	public Sprite keyIcon;
	public Sprite potionIcon;
//	public Image Slot1_inside;
//	public Image Slot2_inside;
//	public Image Slot3_inside;
	public Image[] Slot_inside= new Image[3];
	public Image[] slots = new Image[3];
	
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

				string temp = "Slot" + inventory.IndexOf(obj);

				for (int i = 0; i < slots.Length; i++) {
					if(slots[i].gameObject.name == temp){
						if(item.name == "CageKeyUsable(Clone)"){
							Slot_inside[i].sprite = keyIcon;
							Slot_inside[i].color = new Color(Slot_inside[i].color.r,Slot_inside[i].color.g,Slot_inside[i].color.b,
							                                 1.0f);
							
						}else if (item.name == "ThrowableRock(Clone)"){
							Slot_inside[i].sprite = rockIcon;
							Slot_inside[i].color = new Color(Slot_inside[i].color.r,Slot_inside[i].color.g,Slot_inside[i].color.b,
							                                 1.0f);
						}
					}
				}

				inventory[inventory.IndexOf(obj)] = item;
                return;
            }
        }
    }


	public void remove(int slotNumber){
		Slot_inside[slotNumber].sprite = null;
		Slot_inside[slotNumber].color = new Color(Slot_inside[slotNumber].color.r,Slot_inside[slotNumber].color.g
		                                          ,Slot_inside[slotNumber].color.b,0.0f);
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
