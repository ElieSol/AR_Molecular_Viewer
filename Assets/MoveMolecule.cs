using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MoveMolecule : MonoBehaviour {

    public Transform tmolecule;
    public GameObject molecule;
    public float rotateSpeed = 10f;
    bool rotateXStatus = false;
	bool rotateYStatus = false;
    bool ScaleUpStatus = false;
    bool ScaleDownStatus = false;
    GameObject obj;
    bool objStatus;
    string match;

    public void rotateX() // change the status to activate / desactivate 
    {

        if (rotateXStatus == false)
        {
            rotateXStatus = true;
        }
        else
        {
            rotateXStatus = false;
        }
    }

	public void rotateY()
	{

		if (rotateYStatus == false)
		{
			rotateYStatus = true;
		}
		else
		{
			rotateYStatus = false;
		}
	}

    public void scaleUp()
    {
        if (ScaleUpStatus == false)
        {
            ScaleUpStatus = true;
        }
        else
        {
            ScaleUpStatus = false;
        }
    }


    public void scaleDown()
    {
        if (ScaleDownStatus == false)
        {
            ScaleDownStatus = true;
        }
        else
        {
            ScaleDownStatus = false;
        }
    }

    public void Update()

    {

        foreach(GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) // search the imported molecule and set it as parent of empty game obejct "molecule"
        {
            if (go.name.StartsWith("scene"))
            {
                match = go.name;
                go.transform.SetParent(molecule.transform, true);
                objStatus = true;
                break;
            }
        }

        if (rotateXStatus == true)
        {
			Vector3 point = molecule.transform.position;
			transform.LookAt (point);
			molecule.transform.Rotate(0,rotateSpeed * Time.deltaTime,0);
        }

		if (rotateYStatus == true)
		{
			Vector3 point = molecule.transform.position;
			transform.LookAt (point);
			molecule.transform.Rotate(rotateSpeed * Time.deltaTime,0,0);
		}

        if (ScaleUpStatus == true)
        {
            if(objStatus == true)
            {
                obj = GameObject.Find(match);
                obj.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            }
        }

        if (ScaleDownStatus == true)
        {
            if(objStatus == true)
            {
                obj = GameObject.Find(match);
                obj.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
            }
        }
 
    }

}
