using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private GameObject visualizeUnitPrefab;
    private void VisualizeUnit(VisualizationData data)
    {
        var _unit = Instantiate(parent: transform, original: visualizeUnitPrefab);
        if(data.Image != null) _unit.GetComponent<SpriteRenderer>().sprite = data.Image;
        _unit.GetComponent<SpriteRenderer>().color = data.ObjectColor;
        _unit.transform.position = data.Position;
        _unit.transform.localRotation = Quaternion.Euler(data.Rotation.x,data.Rotation.y,data.Rotation.z);
        _unit.transform.localScale = data.Scale;
    }
    public void Visualize(VisualizationData[] data)
    {
        for(int i = transform.childCount-1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach(var _unit in data)
        {
            VisualizeUnit(_unit);
        }
    }
}
public struct VisualizationData
{
    public Sprite Image;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public Color ObjectColor;    
}
