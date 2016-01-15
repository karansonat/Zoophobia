using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharachtersUIController : MonoBehaviour {

	public Image slothImage;
	public Image MonkeyImage;


    public static float _CARRY_X = 70f;
	public static float _CARRY_Y = -110f;
    public static float _CARRIED_X = 70f;
	public static float _CARRIED_Y = -30f;
	public static float _SEPERATED_MONKEY_X = 20;
	public static float _SEPERATED_MONKEY_Y = -70;
	public static float _SEPERATED_SLOTH_X = 120;
	public static float _SEPERATED_SLOTH_Y = -70f;

	public void FadeControl(int carryMode, int character){
		if(carryMode == (int)CARRY_MODE.SEPERATE){
			if(character == (int)ACTIVE_CHARACTER.SLOTH){

				if(slothImage.color.a != 1f || MonkeyImage.color.a!=0.5f){
					float slothAlpha = Mathf.Lerp(slothImage.color.a,1f,Time.deltaTime*5f);
					float monkeyAlpha = Mathf.Lerp(MonkeyImage.color.a,0.5f,Time.deltaTime*5f);
					
					slothImage.color = new Color(slothImage.color.r,slothImage.color.g,slothImage.color.b,slothAlpha);
					MonkeyImage.color = new Color(MonkeyImage.color.r,MonkeyImage.color.g,MonkeyImage.color.b,monkeyAlpha);
				}


			}else{
				if(slothImage.color.a != 0.5f || MonkeyImage.color.a!=1f){
					float monkeyAlpha = Mathf.Lerp(MonkeyImage.color.a,1f,Time.deltaTime*5f);
					float slothAlpha = Mathf.Lerp(slothImage.color.a,0.5f,Time.deltaTime*5f);

					slothImage.color = new Color(slothImage.color.r,slothImage.color.g,slothImage.color.b,slothAlpha);
					MonkeyImage.color = new Color(MonkeyImage.color.r,MonkeyImage.color.g,MonkeyImage.color.b,monkeyAlpha);
				}
			}

		}
		else if(carryMode == (int)CARRY_MODE.COMBINE){
			float monkeyAlpha = Mathf.Lerp(MonkeyImage.color.a,1f,Time.deltaTime*5f);
			float slothAlpha = Mathf.Lerp(slothImage.color.a,1f,Time.deltaTime*5f);

			slothImage.color = new Color(slothImage.color.r,slothImage.color.g,slothImage.color.b,slothAlpha);
			MonkeyImage.color = new Color(MonkeyImage.color.r,MonkeyImage.color.g,MonkeyImage.color.b,monkeyAlpha);
		}

	}




	public void SetPositionsfForMonkeyCarries(){


		SetMonkeyPos(_CARRY_X,_CARRY_Y);
		SetSlothPos(_CARRIED_X,_CARRIED_Y);
	}

	public void SetPositionsfForSlothCarries(){
		SetSlothPos(_CARRY_X,_CARRY_Y);
		SetMonkeyPos(_CARRIED_X,_CARRIED_Y);
	}
	
	public void SetSeperatedPos(){
		SetMonkeyPos(_SEPERATED_MONKEY_X,_SEPERATED_MONKEY_Y);
		SetSlothPos(_SEPERATED_SLOTH_X,_SEPERATED_SLOTH_Y);
		
	}

	public void SetMonkeyPos (float x_pos , float y_pos){
		float newX_Monkey = Mathf.Lerp(MonkeyImage.rectTransform.anchoredPosition.x,x_pos,Time.deltaTime*5f);
		float newY_Monkey = Mathf.Lerp(MonkeyImage.rectTransform.anchoredPosition.y,y_pos,Time.deltaTime*5f);

		Vector2 _newPos =  new Vector2(newX_Monkey,newY_Monkey);

		MonkeyImage.rectTransform.anchoredPosition = _newPos;
	}

	public void SetSlothPos(float x_pos , float y_pos){
		float newX_Sloth = Mathf.Lerp(slothImage.rectTransform.anchoredPosition.x,x_pos,Time.deltaTime*5f);
		float newY_Sloth = Mathf.Lerp(slothImage.rectTransform.anchoredPosition.y,y_pos,Time.deltaTime*5f);

		Vector2 _newPos = new Vector2(newX_Sloth,newY_Sloth);
		
		slothImage.rectTransform.anchoredPosition = _newPos;
	}




}
