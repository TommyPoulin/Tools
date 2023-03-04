using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public int shapeNumber;
    List<GameObject> shapes = new List<GameObject>();
    string path = "";
    void Start() {
        CreateShapes();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z))
            SaveJson();
        if (Input.GetKeyDown(KeyCode.X))
            LoadJson();
        if (Input.GetKeyDown(KeyCode.C))
            SaveXML();
        if (Input.GetKeyDown(KeyCode.V))
            LoadXML();
        if (Input.GetKeyDown(KeyCode.B))
            SaveBinary();
        if (Input.GetKeyDown(KeyCode.N))
            LoadBinary();
    }

    void CreateShapes() {
        for (int i = 0; i < shapeNumber; i++) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(Random.Range(-3, 3), Random.Range(2, 5), Random.Range(-3, 3));
            go.AddComponent<Rigidbody>();
            shapes.Add(go);
        }
    }

    object SerializeObject() {
        List<Shape> s = new List<Shape>();
        for (int i = 0; i < shapes.Count; i++) {
            Vec3 p, sc, v;
            Quat r;
            p = new Vec3(shapes[i].transform.position.x, shapes[i].transform.position.y, shapes[i].transform.position.z);
            sc = new Vec3(shapes[i].transform.localScale.x, shapes[i].transform.localScale.y, shapes[i].transform.localScale.z);
            Vector3 tempVel = shapes[i].GetComponent<Rigidbody>().velocity;
            v = new Vec3(tempVel.x, tempVel.y, tempVel.z);
            r = new Quat(shapes[i].transform.rotation.w, shapes[i].transform.rotation.x, shapes[i].transform.rotation.y, shapes[i].transform.rotation.z);

            s.Add(new Shape(p, r, sc, v));
        }
        GameInfo game = new GameInfo(shapeNumber, s);
        return game;
    }

    void DeserializeObject(object o) {
        GameInfo gi = o as GameInfo;
        this.shapeNumber = gi.shapeCount;
        foreach (GameObject go in shapes) {
            Destroy(go);
        }
        shapes = new List<GameObject>();
        for (int i = 0; i < shapeNumber; i++) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(gi.shapes[i].position.x, gi.shapes[i].position.y, gi.shapes[i].position.z);
            go.transform.rotation = new Quaternion(gi.shapes[i].rotation.x, gi.shapes[i].rotation.y, gi.shapes[i].rotation.z, gi.shapes[i].rotation.w);
            go.transform.localScale = new Vector3(gi.shapes[i].scale.x, gi.shapes[i].scale.y, gi.shapes[i].scale.z);
            go.AddComponent<Rigidbody>().velocity = new Vector3(gi.shapes[i].velocity.x, gi.shapes[i].velocity.y, gi.shapes[i].velocity.z);
            shapes.Add(go);
        }
    }

    public void SaveJson() {
        GameInfo game = SerializeObject() as GameInfo;

        string jsonSerializationOfNewClass = JsonUtility.ToJson(game);

        string directoryPath = Path.Combine(Application.streamingAssetsPath, "GameSaves/");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath;
        if (path == "")
            filePath = Path.Combine(directoryPath, "jsonSave.txt");
        else
            filePath = Path.Combine(directoryPath, path + ".txt");
        File.WriteAllText(filePath, jsonSerializationOfNewClass);
    }

    public void LoadJson() {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "GameSaves/");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath;
        if (path == "")
            filePath = Path.Combine(directoryPath, "jsonSave.txt");
        else
            filePath = Path.Combine(directoryPath, path + ".txt");

        if (File.Exists(filePath)) {
            string jsonDeserialized = File.ReadAllText(filePath);

            GameInfo newClassLoadedFromJson = JsonUtility.FromJson<GameInfo>(jsonDeserialized);
            DeserializeObject(newClassLoadedFromJson);
        }
        else {
            Debug.Log("File not found.");
        }
    }

    public void SaveXML() {
        object o = SerializeObject();
        var serializer = new XmlSerializer(typeof(GameInfo));
        string filePath;
        if (path == "")
            filePath = Application.streamingAssetsPath + "/GameSaves/XMLSave";
        else
            filePath = Application.streamingAssetsPath + "/GameSaves/" + path;
        using (var stream = new FileStream(filePath, FileMode.Create)) {
            serializer.Serialize(stream, o);
        }
    }

    public void LoadXML() {
        var serializer = new XmlSerializer(typeof(GameInfo));
        string filePath;
        if (path == "")
            filePath = Application.streamingAssetsPath + "/GameSaves/XMLSave";
        else
            filePath = Application.streamingAssetsPath + "/GameSaves/" + path;
        if (File.Exists(filePath)) {
            using (var stream = new FileStream(filePath, FileMode.Open)) {
                DeserializeObject(serializer.Deserialize(stream) as GameInfo);
            }
        }
        else {
            Debug.Log("File not found.");
        }
    }

    public void SaveBinary() {
        BinaryFormatter bf = new BinaryFormatter();
        string filePath;
        if (path == "")
            filePath = Application.streamingAssetsPath + "/GameSaves/binarySave";
        else
            filePath = Application.streamingAssetsPath + "/GameSaves/" + path;
        FileStream file = File.Create(filePath);
        bf.Serialize(file, SerializeObject());
        file.Close();
    }

    public void LoadBinary() {
        string filePath;
        if (path == "")
            filePath = Application.streamingAssetsPath + "/GameSaves/binarySave";
        else
            filePath = Application.streamingAssetsPath + "/GameSaves/" + path;
        if (File.Exists(filePath)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            DeserializeObject(bf.Deserialize(file));
            file.Close();
        }
        else {
            Debug.Log("File not found.");
        }
    }

    public void ChangePath(string p) {
        path = p;
    }
}

[System.Serializable]
public class Vec3
{
    public float x, y, z;
    public Vec3() {
        x = 0;
        y = 0;
        z = 0;
    }

    public Vec3(float _x, float _y, float _z) {
        x = _x;
        y = _y;
        z = _z;
    }
}

[System.Serializable]
public class Quat
{
    public float w, x, y, z;
    public Quat() {
        w = 0;
        x = 0;
        y = 0;
        z = 0;
    }

    public Quat(float _w, float _x, float _y, float _z) {
        w = _w;
        x = _x;
        y = _y;
        z = _z;
    }
}

[System.Serializable]
public class Shape
{
    public Vec3 position;
    public Quat rotation;
    public Vec3 scale;
    public Vec3 velocity;

    public Shape() {
        position = new Vec3();
        rotation = new Quat();
        scale = new Vec3();
        velocity = new Vec3();
    }
    public Shape(Vec3 p, Quat r, Vec3 s, Vec3 v) {
        position = p;
        rotation = r;
        scale = s;
        velocity = v;
    }
}

[System.Serializable]
public class GameInfo
{
    public int shapeCount;
    public List<Shape> shapes;

    public GameInfo() {
        shapeCount = 0;
        shapes = new List<Shape>();
    }

    public GameInfo(int sc, List<Shape> ss) {
        shapeCount = sc;
        shapes = ss;
    }
}

