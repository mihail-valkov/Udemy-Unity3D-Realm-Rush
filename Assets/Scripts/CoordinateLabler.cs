using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabler : MonoBehaviour
{
    private TextMeshPro textMesh;

    private void Awake() 
    {
        textMesh = GetComponent<TextMeshPro>();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        textMesh.text = 
        "(" + transform.parent.position.x / UnityEditor.EditorSnapSettings.gridSize.x + 
        ", " + 
        transform.parent.position.z / UnityEditor.EditorSnapSettings.gridSize.y 
        + ")";
    }

    // Update is called once per frame
    void Update()
    {
        //call updatelabel if moved
        if (!Application.isPlaying && transform.hasChanged) 
        {
            UpdateLabel();
            UpdateObjectName();
            transform.hasChanged = false;
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = textMesh.text;
    }  
}
