using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeModel : MonoBehaviour {

	public string url_pdb;
	public string url_server;
	public string type;

	public void chargeCartoon(){
		StartCoroutine (Cartoon());
	}

	public void chargeBallStick(){
		StartCoroutine (BallStick());
	}

	public void chargeSpheres(){
		StartCoroutine (Spheres());
	}

	public void chargeSticks(){
		StartCoroutine (Sticks());
	}

	IEnumerator Cartoon(){
		GameObject co = GameObject.Find("SocketIO");
		Connection connection = co.GetComponent<Connection>();
		GameObject lo = GameObject.Find("ARCamera");
		Load3DModel load = lo.GetComponent<Load3DModel> ();
		//
		string url_server = connection.getURL();
		string url_pdb = connection.getURLPDB();
		// Appel a la fonction download model de Connection.cs
		Debug.Log("Server at ="+url_server);
		Debug.Log("Getting Cartoon Model online");
		connection.getModel(url_server, url_pdb,"Cartoon");
		yield return new WaitForSeconds (3);
		//Appel a la fonction load de load3dmodel
		Debug.Log("Saving file");
		string filename = getFilename(url_pdb,"Cartoon");
		Debug.Log ("filename =" + filename);
		load.loadModel(filename);
		Debug.Log("Charge Cartoon");
	}

	IEnumerator BallStick()
	{
		GameObject co = GameObject.Find("SocketIO");
		Connection connection = co.GetComponent<Connection>();
		GameObject lo = GameObject.Find("ARCamera");
		Load3DModel load = lo.GetComponent<Load3DModel> ();
		//
		string url_server = connection.getURL();
		string url_pdb = connection.getURLPDB();
		// Appel a la fonction download model de Connection.cs
		Debug.Log("Server at ="+url_server);
		Debug.Log("Getting BallStick Model online");
		connection.getModel(url_server, url_pdb,"BallStick");
		yield return new WaitForSeconds (3);
		//Appel a la fonction load de load3dmodel
		Debug.Log("Saving file");
		string filename = getFilename(url_pdb,"BallStick");
		yield return new WaitForSeconds (3);
		Debug.Log ("filename =" + filename);
		load.loadModel(filename);
		Debug.Log("Charge BallStick");
	}

	IEnumerator Spheres()
	{
		GameObject co = GameObject.Find("SocketIO");
		Connection connection = co.GetComponent<Connection>();
		GameObject lo = GameObject.Find("ARCamera");
		Load3DModel load = lo.GetComponent<Load3DModel> ();
		//
		string url_server = connection.getURL();
		string url_pdb = connection.getURLPDB();
		// Appel a la fonction download model de Connection.cs
		Debug.Log("Server at ="+url_server);
		Debug.Log("Getting Spheres Model online");
		connection.getModel(url_server, url_pdb,"Spheres");
		yield return new WaitForSeconds (3);
		//Appel a la fonction load de load3dmodel
		Debug.Log("Saving file");
		string filename = getFilename(url_pdb,"Spheres");
		yield return new WaitForSeconds (3);
		Debug.Log ("filename =" + filename);
		load.loadModel(filename);
		Debug.Log("Charge Spheres");
	}

	IEnumerator Sticks()
	{
		GameObject co = GameObject.Find("SocketIO");
		Connection connection = co.GetComponent<Connection>();
		GameObject lo = GameObject.Find("ARCamera");
		Load3DModel load = lo.GetComponent<Load3DModel> ();
		//
		string url_server = connection.getURL();
		string url_pdb = connection.getURLPDB();
		// Appel a la fonction download model de Connection.cs
		Debug.Log("Server at ="+url_server);
		Debug.Log("Getting Sticks Model online");
		connection.getModel(url_server, url_pdb,"Sticks");
		yield return new WaitForSeconds (3);
		//Appel a la fonction load de load3dmodel
		Debug.Log("Saving file");
		string filename = getFilename(url_pdb,"Sticks");
		yield return new WaitForSeconds (3);
		Debug.Log ("filename =" + filename);
		load.loadModel(filename);
		Debug.Log("Charge Sticks");
	}

	// Méthode utilitaire 
	public string getID(string url){
		string [] urlSplit = url.Split(new char[]{':','.','/'});
		Debug.Log ("ID="+urlSplit[7]);
		string id = urlSplit [7];
		return id;
	}

	public string getFilename(string url_pdb, string type){
		string id_pdb = getID (url_pdb);
		string prefilename = type+"_"+id_pdb +".dae";
		string filename = prefilename.ToString ();
		return filename;
	}
}
