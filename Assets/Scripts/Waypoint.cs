
using TMPro;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] bool isPlaceable;

    TextMeshPro textLabel;

    EnemyMover target;

    public bool IsPlaceable { get { return isPlaceable; } }

    private void Awake() 
    {
        textLabel = GetComponentInChildren<TextMeshPro>();
        textLabel.enabled = false;
    }

    public Vector2 GetGridPos()
    {
        return new Vector2(
            transform.position.x / UnityEditor.EditorSnapSettings.gridSize.x,
            transform.position.z / UnityEditor.EditorSnapSettings.gridSize.y
        );
    }

    void Update()
    {
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (textLabel != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                textLabel.enabled = !textLabel.enabled;
            }
        }
    }


    // Track on mouse clicked on some waypoint
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            var tower = Instantiate(weapon, transform.position, Quaternion.identity);
            
            Debug.Log("Weapon placed on " + gameObject.name);
            isPlaceable = false;
        }
    }

}
