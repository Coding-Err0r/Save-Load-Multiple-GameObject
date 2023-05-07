
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawnedObjects;
    public bool rEnabled = false;
    public SpawnGameObjects spawnGameObjects;
    public bool editMode = false;
    public string selectedObjName = "";
    private GameObject selectedObj;
    


    [SerializeField] public GameObject drag;
    [SerializeField] public GameObject rotate;
    [SerializeField] public GameObject edit;




    private void Awake() {
        SaveSystem.Init();
        Load();
    }
    private void Start() {
        spawnGameObjects = FindObjectOfType<SpawnGameObjects>();
        rotate.SetActive(false);
        drag.SetActive(false);
        edit.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rEnabled = !rEnabled;
            if(rEnabled == true)
            {
                spawnedObjects =  GameObject.FindGameObjectsWithTag("Spawn"); 
                foreach (GameObject spawnedObject in spawnedObjects)
                {
                    if(spawnedObject.name == selectedObjName)
                    {
                        selectedObj.GetComponent<Renderer> ().material.color = Color.red;
                        spawnedObject.GetComponent<RotateGameObject>().enabled = true;
                        spawnedObject.GetComponent<DragGameObjects>().enabled = false;
                    }
                    rotate.SetActive(true);
                    drag.SetActive(false);
                    
                }
            }
            else
            {
                spawnedObjects =  GameObject.FindGameObjectsWithTag("Spawn"); 
                foreach (GameObject spawnedObject in spawnedObjects)
                {
                    if(spawnedObject.name == selectedObjName){
                        selectedObj.GetComponent<Renderer> ().material.color = Color.green;
                        spawnedObject.GetComponent<RotateGameObject>().enabled = false;
                        spawnedObject.GetComponent<DragGameObjects>().enabled = true;
                    }
                    rotate.SetActive(false);
                    drag.SetActive(true);
                }
            }

            
        }

        if (Input.GetMouseButtonDown(0)) {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                selectedObj = hit.collider.gameObject;
                if(selectedObj.tag == "Spawn"){
                    selectedObjName = hit.transform.name; 
                    editMode  = true;
                    selectedObj.GetComponent<Renderer> ().material.color = Color.blue;
                }
            }  
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            editMode = false;
            edit.SetActive(false);
            rotate.SetActive(false);
            drag.SetActive(false);
            selectedObj.GetComponent<Renderer> ().material.color = Color.white;
        }
        if(editMode == true){
            edit.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Delete)){
            Destroy(selectedObj);
        }
    }

    
    private void Save()
    {
        SaveGameObjects saveData = new SaveGameObjects();
        saveData.Data = new List<Data>();
        spawnedObjects =  GameObject.FindGameObjectsWithTag("Spawn"); 
        foreach (GameObject spawnedObject in spawnedObjects)
        {
            Vector3 gObjectPosition = spawnedObject.transform.position;

            float rotX;
            if(spawnedObject.transform.eulerAngles.x <= 180f)
            {
                rotX = spawnedObject.transform.eulerAngles.x;
            }
            else
            {
                rotX = spawnedObject.transform.eulerAngles.x - 360f;
            }
            float rotY;
            if(spawnedObject.transform.eulerAngles.y <= 180f)
            {
                rotY = spawnedObject.transform.eulerAngles.y;
            }
            else
            {
                rotY = spawnedObject.transform.eulerAngles.y - 360f;
            }
            float rotZ;
            if(spawnedObject.transform.eulerAngles.z <= 180f)
            {
                rotZ = spawnedObject.transform.eulerAngles.z;
            }
            else
            {
                rotZ = spawnedObject.transform.eulerAngles.z - 360f;
            }
            
            

            Data _data = new Data();
            _data.name = spawnedObject.name;
            _data.position = gObjectPosition;

            _data.Rotation = new List<Rotation>();
            Rotation rotation = new Rotation();
            rotation.x = rotX;
            rotation.y = rotY;
            rotation.z = rotZ;
            
            _data.Rotation.Add(rotation);

            saveData.Data.Add(_data);
        }
        string json = JsonUtility.ToJson(saveData);
        SaveSystem.Save(json);

    }

    private void Load()
    {
        string saveData = SaveSystem.Load();
        if(saveData !=null){
            
            SaveGameObjects saveGameObjects = JsonUtility.FromJson<SaveGameObjects>(saveData);
            for (int i = 0; i < saveGameObjects.Data.Count; i++)
            {
                if(saveGameObjects.Data[i].name.ToString().Contains("cube")){
                    GameObject spawnedObject= Instantiate(spawnGameObjects.cubePrefab, saveGameObjects.Data[i].position, Quaternion.identity);
                    spawnedObject.name = saveGameObjects.Data[i].name;
                    spawnedObject.transform.rotation = Quaternion.Euler(saveGameObjects.Data[i].Rotation[0].x,saveGameObjects.Data[i].Rotation[0].y,saveGameObjects.Data[i].Rotation[0].z);
                }
                else if(saveGameObjects.Data[i].name.ToString().Contains("sphere")){
                    GameObject spawnedObject= Instantiate(spawnGameObjects.spherePrefab, saveGameObjects.Data[i].position, Quaternion.identity);
                    spawnedObject.name = saveGameObjects.Data[i].name;
                    spawnedObject.transform.rotation = Quaternion.Euler(saveGameObjects.Data[i].Rotation[0].x,saveGameObjects.Data[i].Rotation[0].y,saveGameObjects.Data[i].Rotation[0].z);
                }
                
            }
            
            
        }
    }

    void OnApplicationQuit()
    {
        Save();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }

    public void SaveAndQuit()
    {
        Save();
        Application.Quit();
    }

    [System.Serializable]
    public struct SaveGameObjects{
        public List<Data> Data;
    }

    [System.Serializable]
     public struct Data
     {
        public string name;
        public Vector3 position;
        public List<Rotation> Rotation;
     }
    [System.Serializable]
     public struct Rotation
     {
        public float x;
        public float y;
        public float z;
     }
}
