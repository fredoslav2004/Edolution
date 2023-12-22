using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Sirenix.Utilities;
using Sirenix.Serialization;

public class SimulationManager : SerializedMonoBehaviour
{
    #region VARIABLES
    [OdinSerialize] private Dictionary<string, ISimulationObject> simulationObjectCollection;
    #endregion
    [Button]
    void SimulateFrame()
    {
        simulationObjectCollection.Values.ForEach(x => 
        {
            if(x.Active) x.SimulateFrame(simulationObjectCollection);
        });
    }
}
public struct SimulationState
{
    public long FramesPassed;
    public ISimulationObject[] SimulationObjectCollection;
}
public interface ISimulationObject
{
    public string code { get; set; }
    public Vector2 Position { get; set; }
    public void SimulateFrame(Dictionary<string, ISimulationObject> simulationObjects) { }
    public bool Active { get; set; }
}
public interface IAlive : ISimulationObject
{    
    [SerializeField] public string Genome { get; set; }
}
[Serializable]
public class SimulationUnit : IAlive
{
    [SerializeField] public string Genome { get; set; }
    [SerializeField] public string code { get; set; }
    [SerializeField] public Vector2 Position { get; set; }
    [SerializeField] public bool Active { get; set; }
    public void SimulateFrame(Dictionary<string, ISimulationObject> simulationObjects)
    {
        Debug.Log(Genome);
    }
}
