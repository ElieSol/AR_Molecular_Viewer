using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
 
public class MyPrefabInstantiator : MonoBehaviour, ITrackableEventHandler {
 
    private TrackableBehaviour mTrackableBehaviour;

    public Transform myModelPrefab;

    // Use this for initialization
    void Start ()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
 
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }               
 
    // Update is called once per frame
    void Update ()
    {
    }
 
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            OnTrackingFound();
        }
    }

    public void OnTrackingFound()
    {
        //myModelPrefab = Resources.Load("Molecule") as Transform;
        if (myModelPrefab != null)
        {
             Transform myModelTrf = GameObject.Instantiate(myModelPrefab) as Transform; // Instantiates a prefab at the path "Assets/Resources/Molecule"

             myModelTrf.parent = mTrackableBehaviour.transform;             
             myModelTrf.localPosition = new Vector3(0f, 0f, 0f);
             myModelTrf.localRotation = Quaternion.identity;
             myModelTrf.localScale = new Vector3(1, 1, 1);
 
             myModelTrf.gameObject.active = true;
         }
     }
}
