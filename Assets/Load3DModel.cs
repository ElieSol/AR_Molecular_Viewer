using System;
using UnityEngine;
using TriLib;


public class Load3DModel : MonoBehaviour{
	//private GameObject molecule;
#if !UNITY_WINRT
	public void Start(){}

	public void loadModel(string filename){
		if (GameObject.Find ("molecule") != null) {
			GameObject mol = GameObject.Find ("molecule");
			mol.transform.DestroyChildren();
		}
		using (var assetLoader = new AssetLoader()){
			try{
				var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
				assetLoaderOptions.RotationAngles = new Vector3(90f, 180f, 0f);
				assetLoaderOptions.AutoPlayAnimations = true;
				//var loadedGameObject = assetLoader.LoadFromFile(Application.dataPath + "/" + filename, assetLoaderOptions);
				var loadedGameObject = assetLoader.LoadFromFile(Application.persistentDataPath + "/" + filename, assetLoaderOptions);
				//var loadedGameObject = assetLoader.LoadFromFile("/Users/Juliesolacroup/Desktop" + "/" + filename, assetLoaderOptions);
				//var loadedGameObject = assetLoader.LoadFromFile("file://" + Application.persistentDataPath + "/" + filename);
				loadedGameObject.transform.position = new Vector3(0f, 0f, 128f);
				}
			catch (Exception e){
				Debug.LogError(e.ToString());
				}
			}
		}
#endif
}
