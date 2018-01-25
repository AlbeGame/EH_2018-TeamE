using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public MenuOptions menuOptionsPrefab;
    public GameObject container;

    public int index = 0;
    public Color normalColor, lockedColor;


    public MenuVoice[] voices;


        
    private void Start()
    {
        InitMenut();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index == 0)
            {
                changeIndex(voices.Length - 1);
            }
            else
            {
                changeIndex(index - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
            if (index == voices.Length - 1)
            {
                changeIndex(0);
            }
            else
            {
                changeIndex(index + 1);
            }
        if (Input.GetKey(KeyCode.Return))
        {
            ExecuteFunction(index);
        }
    }

    void InitMenut()
    {
        for (int i = 0; i < voices.Length; i++)
        {
            MenuOptions _option = Instantiate(menuOptionsPrefab, container.transform);
            _option.menuName.text = voices[i].text;
            if (voices[i].isLocked)
            {
                _option.arrow.color = lockedColor;
                _option.menuName.color = lockedColor;
            }
            else
            {
                _option.arrow.color = normalColor;
                _option.menuName.color = normalColor;
            }
            voices[i].options = _option;
            if (i== index)
            {
                voices[i].options.arrow.gameObject.SetActive(true);
            }
            else
            {
                voices[i].options.arrow.gameObject.SetActive(false);
            }
        }
    }
   void changeIndex(int _newindex)
    {
        voices[index].options.arrow.gameObject.SetActive(false);
        voices[_newindex].options.arrow.gameObject.SetActive(true);
        index = _newindex;
    }



    void ExecuteFunction(int _index)
    {
        if (voices[index].isLocked)
        {
            print("è bloccata");
        }
        else
        {
            voices[_index].Azione.Invoke();
        }
 
    }

    public void ChangeScene(int _sceneindex)
    {
        SceneManager.LoadScene(_sceneindex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
[System.Serializable]
public class MenuVoice
{
    public string text;
    public bool isLocked;
    public MenuOptions options;
    public UnityEvent Azione;
}
