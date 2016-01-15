using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public enum CARRY_MODE{
	COMBINE,
	SEPERATE,
}
public enum ACTIVE_CHARACTER{
	SLOTH,
	MONKEY,
}
public class Flow : MonoBehaviour {
	
	//Character prefabs
	public GameObject _slothCarryMonkey;
	public GameObject _monkeyCarrySloth;

	public GameObject _playerCombine;
	public GameObject _playerSloth;
	public GameObject _playerMonkey;
	
	private int _carryMode;
	public int _activeCharacter;
	private bool isHoldingDone;
    [HideInInspector]
    public bool isCharactersCloseToEachOther = false;
	private CharachtersUIController UIController;
	
	private float _switchTimer = 0;
	
	// Use this for initialization
	void Start () {
		_carryMode = (int)CARRY_MODE.SEPERATE;
		_activeCharacter = (int)ACTIVE_CHARACTER.SLOTH;
		_playerCombine.SetActive(false);
		_playerMonkey.SetActive(true);
		_playerSloth.SetActive(true);
		_slothCarryMonkey.SetActive(false);
        _monkeyCarrySloth.SetActive(false);
		UIController = gameObject.GetComponent<CharachtersUIController>();
        _playerSloth.GetComponent<Platformer2DUserControl>().enabled = true;
        _playerMonkey.GetComponent<Platformer2DUserControl>().enabled = false;

		isHoldingDone = false;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterControlProcess();	

		if(_carryMode == (int)CARRY_MODE.COMBINE){
			if(_activeCharacter == (int)ACTIVE_CHARACTER.MONKEY){
				UIController.SetPositionsfForMonkeyCarries();
			}else if(_activeCharacter == (int)ACTIVE_CHARACTER.SLOTH){
				UIController.SetPositionsfForSlothCarries();
			}
		}else{
			UIController.SetSeperatedPos();


			
		}
		UIController.FadeControl((int)_carryMode,(int)_activeCharacter);
	}
	//TODO(sonat): Karakterlerin pozisyonlari set edilecek.
	private void SwitchCarryMode(){
		Debug.Log("Flow::SwitchCarryMode::Started");
		switch (_carryMode){
		case (int)CARRY_MODE.COMBINE:
			_carryMode = (int)CARRY_MODE.SEPERATE;
			_playerCombine.SetActive(false);
			_playerMonkey.SetActive(true);
			_playerSloth.SetActive(true);
            switch (_activeCharacter){
                case (int)ACTIVE_CHARACTER.MONKEY:
                    _playerMonkey.GetComponent<Platformer2DUserControl>().enabled = true;
                    _playerSloth.GetComponent<Platformer2DUserControl>().enabled = false;
                    break;
                case (int)ACTIVE_CHARACTER.SLOTH:
                    _playerSloth.GetComponent<Platformer2DUserControl>().enabled = true;
                    _playerMonkey.GetComponent<Platformer2DUserControl>().enabled = false;
                    break;
            }
			break;
		case (int)CARRY_MODE.SEPERATE:
            if(isCharactersCloseToEachOther){
                _carryMode = (int)CARRY_MODE.COMBINE;
                _playerCombine.SetActive(true);
                _playerMonkey.SetActive(false);
                _playerSloth.SetActive(false);
                break;
            }else{
                break;
            }   
		}
	}
	
	private void SwitchCharacterControl(){
		Debug.Log("Flow::SwitchCharacterControl::Started");
		switch (_carryMode){
		case (int)CARRY_MODE.COMBINE:
			switch (_activeCharacter){
			case (int)ACTIVE_CHARACTER.MONKEY:
				_activeCharacter = (int)ACTIVE_CHARACTER.SLOTH;
				_playerCombine = _slothCarryMonkey;
				_slothCarryMonkey.SetActive(true);
				_monkeyCarrySloth.SetActive(false);
				break;
			case (int)ACTIVE_CHARACTER.SLOTH:
				_activeCharacter = (int)ACTIVE_CHARACTER.MONKEY;
				_playerCombine = _monkeyCarrySloth;
				_slothCarryMonkey.SetActive(false);
				_monkeyCarrySloth.SetActive(true);
				break;
			}
			break;
		case (int)CARRY_MODE.SEPERATE:
			switch (_activeCharacter){
			case (int)ACTIVE_CHARACTER.MONKEY:
				_activeCharacter = (int)ACTIVE_CHARACTER.SLOTH;
                _playerCombine = _slothCarryMonkey;
				_playerMonkey.GetComponent<Platformer2DUserControl>().enabled = false;
				_playerSloth.GetComponent<Platformer2DUserControl>().enabled = true;
				break;
			case (int)ACTIVE_CHARACTER.SLOTH:
				_activeCharacter = (int)ACTIVE_CHARACTER.MONKEY;
                _playerCombine = _monkeyCarrySloth;
				_playerSloth.GetComponent<Platformer2DUserControl>().enabled = false;
				_playerMonkey.GetComponent<Platformer2DUserControl>().enabled = true;
				break;
			}
			break;
		}
	}
	
	private void CharacterControlProcess(){
		if(Input.GetKey(KeyCode.Tab)){
			if(!isHoldingDone){
				_switchTimer += Time.deltaTime;
				if(_switchTimer >= 0.5f){
                    Debug.Log("Flow::Before-SwitchCarryMode");
					isHoldingDone = true;
					SwitchCarryMode();
				}
			}
		}
		else if(Input.GetKeyUp(KeyCode.Tab)){

			if(_switchTimer<0.5f){
                Debug.Log("Flow::Before-SwitchCharacterControl");
				SwitchCharacterControl();

			}
			_switchTimer = 0;
			isHoldingDone = false;
		}
	}


}