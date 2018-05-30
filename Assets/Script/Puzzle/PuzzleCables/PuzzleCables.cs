using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SelectableBehaviour), typeof(PuzzleGraphic))]
public class PuzzleCables : MonoBehaviour, IPuzzle, ISelectable
{
    [Header("Cable colours")]
    public Material RedCab_Mat;
    public Material YellowCab_Mat;
    public Material GreenCab_Mat;
    public Material BlueCab_Mat;
    public Material WhiteCab_Mat;
    public Material BrownCab_Mat;

    [Header("Light colours")]
    public Material LightOff_Mat;
    Material[] lightOff_MatArr;
    public Material LightOn_Mat;
    Material[] lightOn_MatArr;
    [Header("Components")]
    public PuzzleComponents Components;

    List<SelectableSwitch> switches;

    int currentLightIndex;
    int currentLightOnAmount = 1;

    float currentLightInterval;

    SelectableBehaviour selectable;
    PuzzleGraphic graphicCtrl;

    PuzzleCablesData data;
    PuzzleCablesData.Setup chosenSetup;

    public PuzzleState SolutionState { get; set; }

    private void Update()
    {
        if (!data)
            return;

        currentLightInterval += Time.deltaTime;
        if(currentLightInterval >= data.LightInterval)
        {
            currentLightInterval = 0;
            UpdateLights();
        }
    }

    public bool CheckIfSolved()
    {
        throw new System.NotImplementedException();
    }

    public void DoLoose()
    {
        throw new System.NotImplementedException();
    }

    public void DoWin()
    {
        throw new System.NotImplementedException();
    }

    public void OnButtonSelect(SelectableButton _button)
    {
        throw new System.NotImplementedException();
    }

    public void OnSelection()
    {
        throw new System.NotImplementedException();
    }

    public void OnStateChange(SelectionState _state)
    {
        throw new System.NotImplementedException();
    }

    public void OnSwitchSelect(SelectableSwitch _switch)
    {
        CabSwitchData data = _switch.InputData as CabSwitchData;


    }

    public void OnMonitorSelect(SelectableMonitor _monitor)
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdateSelectable(IPuzzleInput _input)
    {
        throw new System.NotImplementedException();
    }

    public void Setup(IPuzzleData _data)
    {
        selectable = GetComponent<SelectableBehaviour>();
        graphicCtrl = GetComponent<PuzzleGraphic>();

        data = _data as PuzzleCablesData;

        lightOff_MatArr = new Material[] { Components.Lights[0].materials[0], LightOff_Mat };
        lightOn_MatArr = new Material[] { Components.Lights[0].materials[0], LightOn_Mat };
    }

    public void Init()
    {
        //Choose a new setup in data
        int setupIndex = Random.Range(0, data.Setups.Count);
        chosenSetup = data.Setups[setupIndex];

        switches.Clear();

        //Setup button
        Components.StartButton.Init(this, new CabButtonData() { BtnType = chosenSetup.StartButton });
        //Setup cables
        InitCables();
        //Setup lights
        InitLights();
    }

    void InitCables()
    {
        int toConnectCabs = chosenSetup.ConnectedCables.Count;
        int toDetachCabs = chosenSetup.DetachedCables.Count;
        int noCab = Components.Cables.Count - toConnectCabs - toDetachCabs;

        List<GameObject> alreadySetCabs = new List<GameObject>();
        while (toConnectCabs + toDetachCabs + noCab > 0)
        {
            //choose an avaiable cable
            int cabIndex = Random.Range(0, Components.Cables.Count);
            GameObject cab = Components.Cables[cabIndex];
            if (alreadySetCabs.Contains(cab))
                continue;
            else
                alreadySetCabs.Add(cab);

            if (toConnectCabs > 0)
            {
                cab.SetActive(true);

                toConnectCabs--;
                CableType cabColor = chosenSetup.ConnectedCables[toConnectCabs];
                MeshRenderer cabRenderer = cab.GetComponentInChildren<MeshRenderer>();
                cabRenderer.material = GetMaterialByType(cabColor);

                SelectableSwitch cabSwitch = cab.GetComponentInChildren<SelectableSwitch>();
                cabSwitch.Init(this, new CabSwitchData() { CabType = cabColor });
                cabSwitch.selectStatus = true;
            }
            else if (toDetachCabs > 0)
            {
                cab.SetActive(true);

                toDetachCabs--;
                CableType cabColor = chosenSetup.DetachedCables[toDetachCabs];
                MeshRenderer cabRenderer = cab.GetComponentInChildren<MeshRenderer>();
                cabRenderer.material = GetMaterialByType(cabColor);

                SelectableSwitch cabSwitch = cab.GetComponentInChildren<SelectableSwitch>();
                cabSwitch.Init(this, new CabSwitchData() { CabType = cabColor, CabID = cabIndex});
                cabSwitch.selectStatus = false;
            }
            else
            {
                noCab--;
                cab.SetActive(false);
            }
        }
    }

    void InitLights()
    {
        currentLightIndex = 0;
        currentLightOnAmount = 1;
    }
    //__________________________
    //Inserisci logica di funzionamento dei bottoni e degli switch
    //Inserisci doloose e do win, pulisci OnChangeState
    //______________________________
    void UpdateLights()
    {
        currentLightIndex++;
        if(currentLightIndex >= Components.Lights.Count)
            currentLightIndex = 0;

        int toLightOn = currentLightOnAmount;
        for (int i = 0; i < Components.Lights.Count; i++)
        {
            Components.Lights[i].materials = lightOff_MatArr;
        }

        for (int i = currentLightIndex; i < Components.Lights.Count; i++)
        {
            if (toLightOn <= 0)
                break;

            toLightOn--;
            Components.Lights[currentLightIndex].materials = lightOn_MatArr;
        }

        if(toLightOn > 0)
            for (int i = 0; i < currentLightIndex; i++)
            {
                if (toLightOn <= 0)
                    break;

                toLightOn--;
                Components.Lights[currentLightIndex].materials = lightOn_MatArr;
            }
    }

    Material GetMaterialByType(CableType _type)
    {
        Material toReturn = null;
        switch (_type)
        {
            case CableType.Red1:
                toReturn = new Material(RedCab_Mat);
                break;
            case CableType.Yellow2:
                toReturn = new Material(YellowCab_Mat);
                break;
            case CableType.Green3:
                toReturn = new Material(GreenCab_Mat);
                break;
            case CableType.Blue4:
                toReturn = new Material(BlueCab_Mat);
                break;
            case CableType.White5:
                toReturn = new Material(WhiteCab_Mat);
                break;
            case CableType.Brown6:
                toReturn = new Material(BrownCab_Mat);
                break;
            default:
                break;
        }

        return toReturn;
    }

    public enum CableType
    {
        Red1,
        Yellow2,
        Green3,
        Blue4,
        White5,
        Brown6
    }

    public enum ButtonType
    {
        Red, Blue, Green
    }

    [System.Serializable]
    public class PuzzleComponents
    {
        public SelectableButton StartButton;
        public List<GameObject> Cables = new List<GameObject>();
        public List<MeshRenderer> Lights = new List<MeshRenderer>();
    }

    class CabSwitchData: IPuzzleInputData
    {
        public CableType CabType;
        public int CabID;
    }

    class CabButtonData: IPuzzleInputData
    {
        public ButtonType BtnType;
    }
}
