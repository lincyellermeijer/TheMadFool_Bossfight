﻿using UnityEngine;
using System.Collections;

public class BackToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void backtoMenu()
    {
        Application.LoadLevel("Title_screen");
    }
}