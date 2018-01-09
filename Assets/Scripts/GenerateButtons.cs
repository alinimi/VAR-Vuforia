using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class GenerateButtons : MonoBehaviour {
    Object[] furniture;
    public Transform target;
    public GameObject iface;
    public ChangeColor changeColor;
    public InputHandler inputHandler;
    
    public GameObject buttonPrefab;
    StreamReader wr;
    List<GameObject> buttons = new List<GameObject>();

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
        string t;
        while ((t = wr.ReadLine()) != null)
        {
            GameObject button = Instantiate(buttonPrefab);
            buttons.Add(button);
            button.transform.SetParent(transform);
            button.GetComponentInChildren<Text>().text = t;//Path.GetFileNameWithoutExtension(t);
            LoadFurniture loader = button.GetComponent<LoadFurniture>();
            loader.target = target;
            loader.iface = iface;
            loader.changeColor = changeColor;
            loader.inputHandler = inputHandler;
            loader.filename = Path.GetFileNameWithoutExtension(t);

        }

        
        foreach (string s in Directory.GetFiles("Assets/Resources","*.prefab"))
        {
            GameObject button = Instantiate(buttonPrefab);
            buttons.Add(button);
            button.transform.SetParent(transform);
            button.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(s) ;
            LoadFurniture loader = button.GetComponent<LoadFurniture>();
            loader.target = target;
            loader.iface = iface;
            loader.changeColor = changeColor;
            loader.inputHandler = inputHandler;
            loader.filename = Path.GetFileNameWithoutExtension(s);

        }

    }
	
    public void ChangeTarget(Transform t)
    {
        target = t;
        foreach(var b in buttons)
        {
            b.GetComponent<LoadFurniture>().target = t;
        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}
