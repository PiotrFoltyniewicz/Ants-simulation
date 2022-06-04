using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;
using System.Text.RegularExpressions;
public class GUIManager : MonoBehaviour
{
    public GameObject canvasObject;
    Dictionary<string, GameObject> inputs = new Dictionary<string, GameObject>();
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
        settButtonRect.anchorMax = new Vector2(1, 1);
        settButtonRect.anchorMin = new Vector2(1, 1);
        settButtonRect.pivot = new Vector2(1, 1);
        settButtonRect.anchoredPosition = new Vector2(-25, -25);
        settButtonRect.sizeDelta = new Vector2(100, 100);
        settButtonRect.localScale = new Vector2(1, 1);
        Button settButton = settingsButton.GetComponent<Button>();
        {
            GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            text.transform.SetParent(settingsButton.transform);
            text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().text = "Menu";
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = new Vector2(0, 0);
            textRect.sizeDelta = new Vector2(100, 100);
            textRect.localScale = new Vector2(1, 1);
        }

        // konfiguracja panelu menu
        GameObject panelObject = new GameObject("MenuPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        panelObject.transform.SetParent(canvasObject.transform);
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.localScale = new Vector2(1, 1);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchorMin = new Vector2(0.5f, 0.5f);
        panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = new Vector2(0, 0);
        panelRect.sizeDelta = new Vector2(700, 800);
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
        {
            GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            text.transform.SetParent(startButton.transform);
            text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().text = "Reset";
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = new Vector2(0, 0);
            textRect.sizeDelta = new Vector2(100, 100);
            textRect.localScale = new Vector2(1, 1);
        }

        // konfiguracja przycisku zmian zmiennych do domyślnych
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
        defButton.onClick.AddListener(() =>
        {
            Variables.SetToDefault();
            foreach (KeyValuePair<string, GameObject> input in inputs)
            {
                input.Value.GetComponent<TMP_InputField>().text = Variables.GetVariable(input.Key).ToString();
            }
        });
        {
            GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            text.transform.SetParent(defaultButton.transform);
            text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().text = "Set defaults";
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = new Vector2(0, 0);
            textRect.sizeDelta = new Vector2(200, 100);
            textRect.localScale = new Vector2(1, 1);
        }


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
        {
            GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            text.transform.SetParent(exitButton.transform);
            text.GetComponent<TextMeshProUGUI>().color = Color.black;
            text.GetComponent<TextMeshProUGUI>().text = "X";
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = new Vector2(0, 0);
            textRect.sizeDelta = new Vector2(50, 50);
            textRect.localScale = new Vector2(1, 1);
        }
        exButton.onClick.AddListener(() => ShowMenu(panelObject));

        settButton.onClick.AddListener(() => ShowMenu(panelObject));
        PlaceInputs(panelObject);
        panelObject.SetActive(false);

    }
    void PlaceInputs(GameObject panel)
    {
        inputs.Add("antMoveSpeed", new GameObject("AntMoveSpeed", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("antMaxTurnStrength", new GameObject("AntMaxTurnStrength", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("antStepTime", new GameObject("AntStepTime", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("antLeavePointTime", new GameObject("AntLeavePointTime", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("antSensorRadius", new GameObject("AntSensorRadius", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("timeSpeed", new GameObject("TimeSpeed", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("foodSpawnRadiusInPoint", new GameObject("FoodSpawnRadiusInPoint", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("antMaxNumberOfPoints", new GameObject("AntMaxNumberOfPoints", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("amountOfAnts", new GameObject("AmountOfAnts", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("foodHealth", new GameObject("FoodHealth", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("amountOfFoodInPoint", new GameObject("AmountOfFoodInPoint", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));
        inputs.Add("amountOfFoodPoints", new GameObject("AmountOfFoodPoints", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(TMP_InputField)));

        int posY = -40;
        foreach (KeyValuePair<string, GameObject> input in inputs)
        {
            RectTransform inputRect = input.Value.GetComponent<RectTransform>();
            inputRect.transform.SetParent(panel.transform);
            inputRect.anchorMax = new Vector2(0.5f, 1);
            inputRect.anchorMin = new Vector2(0.5f, 1);
            inputRect.pivot = new Vector2(0.5f, 1);
            inputRect.anchoredPosition = new Vector2(-125, posY);
            inputRect.sizeDelta = new Vector2(250, 35);
            inputRect.localScale = new Vector2(1, 1);
            posY -= 50;

            TMP_InputField inputField = input.Value.GetComponent<TMP_InputField>();

            GameObject text = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
            text.transform.SetParent(input.Value.transform);
            text.GetComponent<TextMeshProUGUI>().color = Color.black;
            inputField.text = Variables.GetVariable(input.Key).ToString();
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

            GameObject descriptionText = Instantiate(text, input.Value.transform);
            descriptionText.GetComponent<RectTransform>().anchoredPosition = new Vector2(260, 0);
            descriptionText.GetComponent<TextMeshProUGUI>().text = Regex.Replace(input.Key, "([a-z])([A-Z])", "$1 $2");
            descriptionText.GetComponent<TextMeshProUGUI>().fontSize = 22;

            // dodać komponenty text itp skonfigurować menu, i zrobić że można zmieniać zmienne do symulacji !!!!
            // dodać text do przycisków zamiast ikon wtf
        }
    }
    void ShowMenu(GameObject menu)
    {
        if (!menuOpened)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
            menuOpened = true;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = (float)Variables.GetVariable("timeSpeed");
            menuOpened = false;
        }
    }

    void ResetSimulation()
    {
        int i = 0;
        var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.NumberDecimalSeparator = ",";
        foreach (KeyValuePair<string, GameObject> input in inputs)
        {
            try
            {
                if (i < 7)
                {
                    Variables.SetVariable(input.Key, float.Parse(input.Value.GetComponent<TMP_InputField>().text, ci));
                }
                else
                {
                    Variables.SetVariable(input.Key, int.Parse(input.Value.GetComponent<TMP_InputField>().text));
                }
            }
            catch
            {
                continue;
            }
            finally
            {
                i++;
            }
        }
        ObjectPooling.pooledToFoodPoints.Clear();
        ObjectPooling.pooledToNestPoints.Clear();
        FoodManager.foodList.Clear();
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = (float)Variables.GetVariable("timeSpeed");
        Ant.movementSpeed = (float)Variables.GetVariable("antMoveSpeed");
    }

}
