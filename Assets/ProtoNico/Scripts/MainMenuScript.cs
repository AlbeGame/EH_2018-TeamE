using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	public MenuOption menuOptionPrefab;
	public GameObject container;

	public int index = 0; 
	public Color normalColor,lockedColor;

	public MenuVoice[] voices;
	
	void Start () {
	
		InitMenu();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if(index == 0){ 
				changeIndex(voices.Length - 1); 
			}
			else{ 
				changeIndex(index - 1); 
			}
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if(index == voices.Length -1){
				changeIndex(0); 
			}
			else{
				changeIndex(index + 1); 
			}
		}


		if(Input.GetKeyDown(KeyCode.Return)){
			ExecuteFunction(index); 
		}
	}

	//funzione messa public 
	public void InitMenu(){

		
		for(int i = 0; i<voices.Length; i++){ 
			MenuOption _option = Instantiate(menuOptionPrefab,container.transform); 
			_option.menuName.text = voices[i].text; 
			if(voices[i].isLocked){ 
				_option.arrow.color = lockedColor; 
				_option.menuName.color = lockedColor;
			}
			else{ 
				_option.arrow.color = normalColor;
				_option.menuName.color = normalColor;
			}
			voices[i].option = _option;

			if(i == index){ 
				voices[i].option.arrow.gameObject.SetActive(true);
			}
			else{ 
				voices[i].option.arrow.gameObject.SetActive(false);
			}
		}
	}

	//funzione messa public
    public void changeIndex(int _newindex){
		voices[index].option.arrow.gameObject.SetActive(false);
		voices[_newindex].option.arrow.gameObject.SetActive(true);
		index = _newindex;
	}
    //guardare riga 101
	void ExecuteFunction(int _index){
		if(voices[_index].isLocked){
			print("Questa funzione è bloccata!");
		}
		else{
			voices[_index].Azione.Invoke();
		}
	}

	public void ChangeScene(int _sceneindex){
		SceneManager.LoadScene(_sceneindex);
	}

	public void ExitGame(){
		Application.Quit();
	}

    //funzioni che vengono chiamate..ti ho fatto una funzione in questo modo, per capire se la vuoi cosi o solo aggiungendo "public" alle mie funzioni private.
    public void Select(int _index)
    {
        ExecuteFunction(index);
    }


}
[System.Serializable]
public class MenuVoice{
	public string text;
	public bool isLocked;
	public MenuOption option;
	public UnityEvent Azione; 
}
