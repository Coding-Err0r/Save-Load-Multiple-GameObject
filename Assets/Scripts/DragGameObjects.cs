using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragGameObjects : MonoBehaviour
{
    private Vector3 mOffset;
    private float zCord;
    GameManager gm;
    private void Awake() {
        this.enabled = false;
    }
    private void Start() {
        gm = FindObjectOfType<GameManager>();
        
    }
    
    private void Update() {
        
    }
    void OnMouseDown()
    {
        if(this.enabled == true ){
        zCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        }
        
    }
    // private void OnMouseUp() {
    //     if(gm.editMode == false){
    //         gameObject.GetComponent<Renderer> ().material.color = Color.white; 
    //     }   
    // }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mPoint = Input.mousePosition;
        mPoint.z = zCord;
        return Camera.main.ScreenToWorldPoint(mPoint);
    }
    void OnMouseDrag()
    {
        if(gm.editMode == true && gm.selectedObjName == gameObject.name){
            if(this.enabled == true ){
                if(this.enabled == true ){
                    if(gameObject.tag == "Spawn")
                            {
                                gameObject.GetComponent<Renderer> ().material.color = Color.green;
                            }
                }
                transform.position = GetMouseWorldPos() + mOffset;
            }
        }
        
        
    }
    
}
