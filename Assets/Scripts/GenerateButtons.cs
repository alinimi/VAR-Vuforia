using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class GenerateButtons : MonoBehaviour {
    Object[] furniture;
    public Transform target;
    public GameObject iface;
    
    public GameObject buttonPrefab;
    StreamReader wr;

    private void Awake()
    {
        string path = "jar:file://" + Application.dataPath + "!/assets/Prefabs.txt";
        WWW wwwfile = new WWW(path);
        while (!wwwfile.isDone) { }
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, "Prefabs.t");
        File.WriteAllBytes(filepath, wwwfile.bytes);

        wr = new StreamReader(filepath);

    }
    // Use this for initialization
    void Start () {
        //furniture = Resources.LoadAll("Assets/BigFurniturePack/Prefabs/Bathroom/");
        //Debug.Log("asdflkjsdfkjljklfsdsdfsdfsdf"+furniture.Length);
#if UNITY_ANDROID
        string t;
        while ((t = wr.ReadLine()) != null)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(transform);
            button.GetComponentInChildren<Text>().text = t;//Path.GetFileNameWithoutExtension(t);
            LoadFurniture loader = button.GetComponent<LoadFurniture>();
            loader.target = target;
            loader.iface = iface;
            loader.filename = Path.GetFileNameWithoutExtension(t);

        }
#else


        foreach (string t in Directory.GetFiles("Assets/Resources","*.bytes"))
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(transform);
            button.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(t) ;
            LoadFurniture loader = button.GetComponent<LoadFurniture>();
            loader.target = target;
            loader.iface = iface;
            loader.filename = Path.GetFileNameWithoutExtension(t);

        }

#endif
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
