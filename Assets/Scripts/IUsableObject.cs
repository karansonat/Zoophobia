using UnityEngine;
using System.Collections;

public interface IUsableObject {
    void MakeAction();
    void Equip(GameObject activePlayer);
}
