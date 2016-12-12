using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_Dialog : MonoBehaviour {

    public string[] answerButtons;
    public string[] questions;
    public Font myFont;
    bool displayDialog = false;
    public bool hasAccepted = false;
    public bool foundSword = false;
    public StartOptions start;
    private AudioSource[] allAudioSources;
    public Texture swordSprite;
    public Text swordText;
    public bool showSword = false;

    void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        swordText.text = "";
    }

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.font = myFont;
        myStyle.normal.textColor = Color.white;

        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.font = myFont;
        myButtonStyle.normal.textColor = Color.white;
        myButtonStyle.hover.textColor = Color.cyan;


        GUILayout.BeginArea(new Rect(200, 100, 400, 400), myStyle);

        if (displayDialog && !hasAccepted)
        {
            GUILayout.Label(questions[0], myStyle);
            GUILayout.Label(questions[1], myStyle);
            GUILayout.Label(questions[2], myStyle);

            if (GUILayout.Button(answerButtons[0], myButtonStyle))
            {
                displayDialog = false;
                hasAccepted = true;
            }
        }
        if (displayDialog && hasAccepted && !foundSword)
        {
            GUILayout.Label(questions[2], myStyle);

            if (GUILayout.Button(answerButtons[0], myButtonStyle))
            {
                displayDialog = false;
            }
        }

        if(displayDialog && hasAccepted && foundSword)
        {
            GUILayout.Label(questions[3], myStyle);

            if(GUILayout.Button(answerButtons[1], myButtonStyle))
            {
                displayDialog = false;
                start.StartButtonClicked();

                for (int i = 0; i < allAudioSources.Length; i++)
                {
                    allAudioSources[i].Stop();
                }
            }
        }
        GUILayout.EndArea();

        if (hasAccepted && foundSword && showSword)
        {
            swordText.enabled = true;
            swordText.text = "You got a SWORD to help you on your journey!";

            GUI.DrawTexture(new Rect(440, 280, 60, 115), swordSprite);

            if (GUI.Button(new Rect(410, 400, 120, 30), "OK!", myButtonStyle))
            {
                swordText.enabled = false;
                showSword = false;
            }
        }
    }

    void OnTriggerEnter2D()
    {
        displayDialog = true;
    }

    void OnTriggerExit2D()
    {
        displayDialog = false;
    }
}

