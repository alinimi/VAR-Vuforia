using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFurniture : MonoBehaviour {
    public Transform target;
    public string filename;
    public GameObject iface;
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
        target.gameObject.GetComponent<ChangeColor>().InitColors();
        iface.SetActive(false);


    }
}
