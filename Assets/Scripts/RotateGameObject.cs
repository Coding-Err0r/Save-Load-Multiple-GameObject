
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RotateGameObject : MonoBehaviour
{
    private float rotationSpeed = 2f;
    GameManager gm;
    private void Awake() {
        this.enabled = false;
    }
    private void Start() {
        gm = FindObjectOfType<GameManager>();
        
    }

    // private void OnMouseUp() {
    //     if(gm.editMode == false){
    //         gameObject.GetComponent<Renderer> ().material.color = Color.white; 
    //     }   
    // }
    void OnMouseDrag() {
        if(gm.editMode == true && gm.selectedObjName == gameObject.name){
            if(this.enabled == true ){
                if(this.enabled == true ){
                    if(gameObject.tag == "Spawn")
                            {
                                gameObject.GetComponent<Renderer> ().material.color = Color.red;
                            }
                }
                float xRot = Input.GetAxis("Mouse X") * rotationSpeed;
                float yRot = Input.GetAxis("Mouse Y") * rotationSpeed;

                transform.Rotate(Vector3.down, xRot);
                transform.Rotate(Vector3.right, yRot);
            }
        }
    }
}
