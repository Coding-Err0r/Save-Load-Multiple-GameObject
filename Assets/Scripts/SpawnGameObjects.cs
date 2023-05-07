
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

    public class SpawnGameObjects : MonoBehaviour
    {
        [SerializeField] public GameObject cubePrefab = null;
        [SerializeField] public GameObject spherePrefab = null;
        private Camera cam = null;
        private string objectToInstantiate="";
        private void Start()
        {
            cam = Camera.main;
        }
        private void Update() {
            SpawnAtMouseClick();    
        }
        private void SpawnAtMouseClick ()
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    if(objectToInstantiate == "Cube")
                    {
                        GameObject cube = Instantiate(cubePrefab, new Vector3(hit.point.x, hit.point.y + cubePrefab.transform.position.y,hit.point.z), Quaternion.identity);
                        cube.name = "cube_" + System.Guid.NewGuid().ToString();
                        objectToInstantiate="";
                    }
                    else if(objectToInstantiate == "Sphere")
                    {
                        GameObject sphere = Instantiate(spherePrefab, new Vector3(hit.point.x, hit.point.y + spherePrefab.transform.position.y,hit.point.z), Quaternion.identity);
                        sphere.name = "sphere_" + System.Guid.NewGuid().ToString();
                        objectToInstantiate="";
                    }
                    
                }
            }
        }

        public void OnClicked(GameObject button)
        {
            Debug.Log("Clicked");
            objectToInstantiate = button.name;
        }

    }


