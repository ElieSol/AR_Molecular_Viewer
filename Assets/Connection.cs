using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SocketIO;

public class Connection: MonoBehaviour
{
	public string url;
	public string ip;
	public string url_pdb;
	public SocketIOComponent socket;
	//
	public void ConnectionToServer(){
		url_pdb = getURLPDB ();
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();
		url = socket.setURL ();
		Debug.Log ("Adresse of server="+url);
		Debug.Log ("Client Ready. Ip of host set.");
		Debug.Log ("Client started listening");
		socket.On ("message", OnMessage);
		socket.On ("boop", TestBoop);
		StartCoroutine (ConnectToServer (url_pdb));
	}
	// Getter
	public string getURL(){
		string url = socket.getURL ();
		return url;
	}

	public string getURLPDB(){
		GameObject sc = GameObject.Find("ARCamera");
		VuforiaScanner scan = sc.GetComponent<VuforiaScanner>();
		string urlpdb = scan.getUrlPDB ();
		return urlpdb;
	}

	public string getIP(){
		string ip = socket.getIP ();
		return ip;
	}

	//_Partie Connexion au serveur et envoi url du pdb
	IEnumerator ConnectToServer(string url_pdb){
		socket.Emit ("connection");
		yield return new WaitForSeconds (1);
		Debug.Log ("data to send="+url_pdb);
		socket.Emit ("url", JSONObject.CreateStringObject(url_pdb));

		yield return 0;
	}
	//Listeners
	void OnMessage(SocketIOEvent socketIOEvent){
		Debug.Log ("Client listening to message");
		string data = socketIOEvent.data.ToString ();
		Debug.Log (data);
	}

	void TestBoop(SocketIOEvent socketIOEvent){
		Debug.Log ("Boop event message result =");
		string data = socketIOEvent.data.ToString ();
		Debug.Log (data);
	}

	//
	IEnumerator myMsg(string msgToSend){
		yield return msgToSend; 
	}

	//_ Partie téléchargement des modeles 3D
	// Méthode test de téléchargement
	public void getModel(string url_server, string url_pdb, string type){
		StartCoroutine(getModelByType(url_server, url_pdb, type));
	}
	// Méthode globale permettant le telechargement des modeles contenus dans le dossier serveur node.js
	public IEnumerator getAllModels(string url_server, string url_pdb){
		Debug.Log ("URL SERVEUR = "+url_server);
		Debug.Log ("URL PDB ="+url_pdb);

		WWW www = new WWW(url_server);

		getModelByType(url_server, "Cartoon", url_pdb);
		getModelByType(url_server, "BallStick", url_pdb);
		getModelByType(url_server, "Spheres", url_pdb);
		getModelByType(url_server, "Sticks", url_pdb);

		while (!www.isDone)
			yield return www;
		Debug.Log(www);

	}
	//Un par un
	public IEnumerator getModelByType(string url_server, string url_pdb, string type){
		string id_pdb = getID (url_pdb);
		string ip = socket.getIP ();
		Debug.Log ("ip="+ip);
		string preurl = "http://"+ip+":8000/"+type+"_"+id_pdb+".dae";
		string url = preurl.ToString();

		WWW www = new WWW(url);
		while (!www.isDone)
			yield return www;
		Debug.Log(www);

		if (www.isDone)
			Debug.Log("Done");

		string prefilename = type+"_"+id_pdb +".dae";
		string filename = prefilename.ToString ();
		string prefile_name = Application.persistentDataPath + "/" + filename;
		string file_name = prefile_name.ToString ();
		File.WriteAllBytes(file_name, www.bytes);
		Debug.Log("path to file ="+file_name);
		new WWW ("file://" + Application.persistentDataPath + "/" + filename);
	}

	// Méthode utilitaire 
	public string getID(string url){
		string [] urlSplit = url.Split(new char[]{':','.','/'});
		Debug.Log ("ID="+urlSplit[7]);
		string id = urlSplit [7];
		return id;
	}
}


