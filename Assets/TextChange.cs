using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour {
	
	public Text txt;

	public void ChangeTxt () { 
		GameObject co = GameObject.Find("SocketIO");
		Connection connection = co.GetComponent<Connection>();

		string url_pdb = connection.getURLPDB ();
		string id_pdb = connection.getID (url_pdb);

		txt = gameObject.GetComponent<Text>(); 
		txt.text = "ID PDB : "+ id_pdb;

	}
}
