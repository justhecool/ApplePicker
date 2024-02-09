using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class goHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private Image refreshButton = null;
    [SerializeField] private Button restart;
    [SerializeField] private Button titleScreen;
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        var texture = UnityEditor.EditorGUIUtility.IconContent("Refresh").image;
        refreshButton.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.7f, 0.5f));
        //scale down
        refreshButton.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        restart.onClick.AddListener(OnRestartButtonClick);
        titleScreen.onClick.AddListener(() => {
            panel.SetActive(false);
            SceneManager.LoadScene("UI");
        });
        refreshButton.GetComponent<Button>().onClick.AddListener(OnRestartButtonClick);

        DontDestroyOnLoad(this.gameObject);

        EventTrigger triggerStart = restart.gameObject.AddComponent<EventTrigger>();
        EventTrigger triggerTitle = titleScreen.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter
        };
        EventTrigger.Entry entryTitle = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter
        };
        entry.callback.AddListener((data) => { OnRestartButtonHover(); });
        entryTitle.callback.AddListener((data) => { OnTitleButtonHover(); });
        triggerStart.triggers.Add(entry);
        triggerTitle.triggers.Add(entryTitle);

        EventTrigger.Entry entry2 = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        EventTrigger.Entry entryTitle2 = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryTitle2.callback.AddListener((data) => { OnTitleButtonExit(); });
        entry2.callback.AddListener((data) => { OnRestartButtonExit(); });
        triggerStart.triggers.Add(entry2);
        triggerTitle.triggers.Add(entryTitle2);

        score.text = "Score: " + PlayerPrefs.GetInt("Score");
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    private void OnTitleButtonExit()
    {
        titleScreen.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTitleButtonHover()
    {
        titleScreen.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnRestartButtonClick()
    {
        panel.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    private void OnRestartButtonExit()
    {
        restart.transform.localScale = new Vector3(1.5f, 1.6f, 1f);
    }

    private void OnRestartButtonHover()
    {
        restart.transform.localScale = new Vector3(1.6f, 1.7f, 1.1f);
    }

   void OnValidate()
    {
        if (refreshButton != null)
        {
            var texture = UnityEditor.EditorGUIUtility.IconContent("Refresh").image;
            refreshButton.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.7f, 0.5f));
        }
        Assert.IsNotNull(score, "score is null");
        Assert.IsNotNull(highScore, "highScore is null");
        Assert.IsNotNull(refreshButton, "refreshButton is null");
        Assert.IsNotNull(restart, "restart is null");
    }
}
