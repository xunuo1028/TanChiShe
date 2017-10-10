using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGF;

public class AppMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.Log("This is Self Debug Test");
        Snaker.Module.Service.UIManager.Example.Example1 ex = new Snaker.Module.Service.UIManager.Example.Example1();
        ex.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
