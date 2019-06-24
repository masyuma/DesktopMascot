using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject Menu,GoTimer, GoClock;

	// Use this for initialization
	void Start () {
        Menu.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Timer()
    {
        GoTimer.SetActive(true);
        Menu.SetActive(false);
    }

    public void Clock()
    {
        GoClock.SetActive(true);
        Menu.SetActive(false);
    }

    public void GameEnd()
    {
       Application.Quit();
    }
}
