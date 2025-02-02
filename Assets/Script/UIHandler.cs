using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using UnityEngine.EventSystems;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameExitButton;
    [SerializeField] private GameObject buttonsPanel;
    private Color originalEnterColor;
    private Color originalExitColor;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        gameStartButton.onClick.AddListener(OnGameStart);
        gameExitButton.onClick.AddListener(Application.Quit);
        EventTrigger triggerStart = gameStartButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger triggerExit = gameExitButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnGameStartButtonHover(); });
        triggerStart.triggers.Add(entry);
        entryExit.eventID = EventTriggerType.PointerEnter;
        entryExit.callback.AddListener((data) => { OnGameExitButtonHover(); });
        triggerExit.triggers.Add(entryExit);
        
        // when mouse exits the button
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { OnGameStartButtonExit(); });
        triggerStart.triggers.Add(entry2);
        EventTrigger.Entry entryExit2 = new EventTrigger.Entry();
        entryExit2.eventID = EventTriggerType.PointerExit;
        entryExit2.callback.AddListener((data) => { OnGameExitButtonExit(); });
        triggerExit.triggers.Add(entryExit2);
        originalEnterColor = gameStartButton.GetComponentInChildren<TMP_Text>().color;
        originalExitColor = gameExitButton.GetComponentInChildren<TMP_Text>().color;

    }

    private void OnGameExitButtonExit()
    {
        gameExitButton.transform.localScale = new Vector3(1f, 1f, 1f);
        // change color back to default
        gameExitButton.GetComponentInChildren<TMP_Text>().color = originalExitColor;
        
    }

    private void OnGameExitButtonHover()
    {
        gameExitButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        // change color to white temporarily
        gameExitButton.GetComponentInChildren<TMP_Text>().color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {       
    
    }

    // on game start button hover

    void Destroy()
    {
        gameStartButton.onClick.RemoveListener(OnGameStart);
        gameExitButton.onClick.RemoveListener(Application.Quit);
    }

    public void OnGameStartButtonHover()
    {
        gameStartButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        // change color to white temporarily
        gameStartButton.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    public void OnGameStartButtonExit()
    {
        gameStartButton.transform.localScale = new Vector3(1f, 1f, 1f);
        // change color back to default
        gameStartButton.GetComponentInChildren<TMP_Text>().color = originalEnterColor;
    }

    public void OnGameStart()
    {
        buttonsPanel.SetActive(false);
        SceneManager.LoadScene("Game");        
    }
    void OnValidate(){
        Assert.IsNotNull(gameStartButton, $"{nameof(gameStartButton)} cannot be null in {name}");
        Assert.IsNotNull(gameExitButton, $"{nameof(gameExitButton)} cannot be null in {name}");
        Assert.IsNotNull(buttonsPanel, $"{nameof(buttonsPanel)} cannot be null in {name}");
    }

}
