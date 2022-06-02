using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class GUIManager : MonoBehaviour
{
   public GameObject canvasObject;
    Dictionary<string,GameObject> inputs = new Dictionary<string,GameObject>();
    bool menuOpened = false;
    
    void Awake()
    {
        canvasObject = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        CanvasScaler canvasScaler = canvasObject.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        canvasObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.sortingOrder = 50;

        new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule), typeof(BaseInput));
        PlaceGUI();
    }

    void PlaceGUI()
    {
        // konfiguracja przycisku od menu
        GameObject settingsButton = new GameObject("SettingsButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        settingsButton.transform.SetParent(canvasObject.transform);
        RectTransform settButtonRect = settingsButton.GetComponent<RectTransform>();
        settButtonRect.anchorMax = new Vector2(1,1);
        settButtonRect.anchorMin = new Vector2(1,1);
        settButtonRect.pivot = new Vector2(1, 1);
        settButtonRect.anchoredPosition = new Vector2(-25, -25);
        settButtonRect.sizeDelta = new Vector2(100, 100);
        settButtonRect.localScale = new Vector2(1, 1);
        Button settButton = settingsButton.GetComponent<Button>();

        // konfiguracja panelu menu
        GameObject panelObject = new GameObject("MenuPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        panelObject.transform.SetParent(canvasObject.transform);
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.localScale = new Vector2(1, 1);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchorMin = new Vector2(0.5f, 0.5f);
        panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = new Vector2(0,0);
        panelRect.sizeDelta = new Vector2(600,800);
        panelObject.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, 0.5f);

        // konfiguracja przycisku resetu
        GameObject startButton = new GameObject("StartButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        startButton.transform.SetParent(panelObject.transform);
        RectTransform startButtonRect = startButton.GetComponent<RectTransform>();
        startButtonRect.anchorMax = new Vector2(1, 0);
        startButtonRect.anchorMin = new Vector2(1, 0);
        startButtonRect.pivot = new Vector2(1, 0);
        startButtonRect.anchoredPosition = new Vector2(-25, 25);
        startButtonRect.sizeDelta = new Vector2(100, 100);
        startButtonRect.localScale = new Vector2(1, 1);
        Button startbutton = startButton.GetComponent<Button>();
        startbutton.onClick.AddListener(() => ResetSimulation());

        // konfiguracja przycisku zmian zmiennych do domy�lnych
        GameObject defaultButton = new GameObject("DefaultButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        defaultButton.transform.SetParent(panelObject.transform);
        RectTransform defButtonRect = defaultButton.GetComponent<RectTransform>();
        defButtonRect.anchorMax = new Vector2(0, 0);
        defButtonRect.anchorMin = new Vector2(0, 0);
        defButtonRect.pivot = new Vector2(0, 0);
        defButtonRect.anchoredPosition = new Vector2(50, 25);
        defButtonRect.sizeDelta = new Vector2(200, 100);
        defButtonRect.localScale = new Vector2(1, 1);
        Button defButton = defaultButton.GetComponent<Button>();
        defButton.onClick.AddListener(() => Variables.SetToDefault());


        GameObject exitButton = new GameObject("ExitButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        exitButton.transform.SetParent(panelObject.transform);
        RectTransform exitButtonRect = exitButton.GetComponent<RectTransform>();
        exitButtonRect.anchorMax = new Vector2(1, 1);
        exitButtonRect.anchorMin = new Vector2(1, 1);
        exitButtonRect.pivot = new Vector2(1, 1);
        exitButtonRect.anchoredPosition = new Vector2(-25, -25);
        exitButtonRect.sizeDelta = new Vector2(50, 50);
        exitButtonRect.localScale = new Vector2(1, 1);
        Button exButton = exitButton.GetComponent<Button>();
        exButton.onClick.AddListener(() => ShowMenu(panelObject));

        settButton.onClick.AddListener(() => ShowMenu(panelObject));
        PlaceInputs(panelObject);
        panelObject.SetActive(false);

    }

    void PlaceInputs(GameObject panel)
    {
        inputs.Add("AntMoveSpeedInput", new GameObject("AntMoveSpeedInput", typeof (RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AntMaxTurnStrength", new GameObject("AntMaxTurnStrength", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AntStepTime", new GameObject("AntStepTime", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AntLeavePointTime", new GameObject("AntLeavePointTime", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AntMaxNumberOfPoints", new GameObject("AntMaxNumberOfPoints", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AntSensorRadius", new GameObject("AntSensorRadius", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AmountOfAnts", new GameObject("AmountOfAnts", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("TimeSpeed", new GameObject("TimeSpeed", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("FoodHealth", new GameObject("FoodHealth", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AmountOfFoodInPoint", new GameObject("AmountOfFoodInPoint", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("FoodSpawnRadiusInPoint", new GameObject("FoodSpawnRadiusInPoint", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("AmountOfFoodPoints", new GameObject("AmountOfFoodPoints", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));

        int posY = -40;
        foreach(GameObject input in inputs.Values)
        {
            RectTransform inputRect = input.GetComponent<RectTransform>();
            inputRect.transform.SetParent(panel.transform);
            inputRect.anchorMax = new Vector2(0.5f, 1);
            inputRect.anchorMin = new Vector2(0.5f, 1);
            inputRect.pivot = new Vector2(0.5f, 1);
            inputRect.anchoredPosition = new Vector2(-125, posY);
            inputRect.sizeDelta = new Vector2(250, 35);
            inputRect.localScale = new Vector2(1, 1);
            posY -= 50;

            TMP_InputField inputField = input.GetComponent<TMP_InputField>();

            GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            text.transform.SetParent(input.transform);
            text.GetComponent<TextMeshProUGUI>().color = Color.black;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = new Vector2(0, 0);
            textRect.sizeDelta = new Vector2(250, 35);
            textRect.localScale = new Vector2(1, 1);

            inputField.textViewport = textRect;
            inputField.textComponent = text.GetComponent<TextMeshProUGUI>();
            inputField.fontAsset = (TMP_FontAsset)Resources.Load("LiberationSans SDF");
            // doda� komponenty text itp skonfigurowa� menu, i zrobi� �e mo�na zmienia� zmienne do symulacji !!!!
            // doda� text do przycisk�w zamiast ikon wtf
        }
    }
    void ShowMenu(GameObject menu)
    {
        if(!menuOpened)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
            menuOpened = true;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = Variables.timeSpeed;
            menuOpened = false;
        }
    }

    void ResetSimulation()
    {
        ObjectPooling.pooledToFoodPoints.Clear();
        ObjectPooling.pooledToNestPoints.Clear();
        FoodManager.foodList.Clear();
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = Variables.timeSpeed;
    }

}
