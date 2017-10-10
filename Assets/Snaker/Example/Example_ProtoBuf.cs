using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using SGF;

public class Example_ProtoBuf : MonoBehaviour {

    [ProtoContract]  //让该类与ProtoBuf关联
    public class UserInfo
    {
        [ProtoMember(1)]    
        public int id;
        [ProtoMember(2)]
        public string name;

        public string title;
    }


	// Use this for initialization
	void Start () {
        UserInfo info = new UserInfo();
        info.id = 123;
        info.name = "slicol";
        info.title = "student";


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
