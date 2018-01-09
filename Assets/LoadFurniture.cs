using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class LoadFurniture : MonoBehaviour {
    public Transform target;
    
    public string filename;
    public GameObject iface;
    public ChangeColor changeColor;
    Object f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void LoadNow()
    {
        Debug.Log("filename " + filename);
        f  = Resources.Load(filename);
        Transform oldChild = target.GetChild(0);

        GameObject o = Instantiate(f,oldChild.position,oldChild.rotation,target) as GameObject;
        Destroy(oldChild.gameObject);
        o.transform.SetParent(target);
        o.transform.SetAsFirstSibling();
        changeColor.InitColors();
        changeColor.EndMenu();
        TrackableBehaviour.Status status = 
            target.gameObject.GetComponent<ImageTargetBehaviour>().CurrentStatus;
        if (!(status == TrackableBehaviour.Status.DETECTED||
            status == TrackableBehaviour.Status.TRACKED ||
            status == TrackableBehaviour.Status.EXTENDED_TRACKED))
        {
            foreach(MeshRenderer rend in o.GetComponentsInChildren<MeshRenderer>())
            {
                rend.enabled = false;
            }
        }

        iface.SetActive(false);


    }
}
