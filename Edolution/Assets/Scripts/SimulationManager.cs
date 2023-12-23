using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Sirenix.Utilities;
using Sirenix.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// MonoBehaviour that runs the simulation, and distributes the state to the end user
/// </summary>
public class SimulationManager : SerializedMonoBehaviour
{
    #region VARIABLES
    [OdinSerialize] private List<ISimulationObject> simulationObjectCollection;
    [SerializeField] private List<SimulationState> simulationHistory;
    [SerializeField] private Visualizer visualizer;
    private long framesPassed;
    [SerializeField] private int frameSkipCountSaving;
    #endregion
    [Button]
    void SimulateFrame()
    {
        framesPassed++;
        var data = CaptureCurrentState();
        SimulateFrameForAll(data);
        if(framesPassed%frameSkipCountSaving==0) SaveState();
        visualizer.Visualize(simulationObjectCollection.Where(x => x.Active).Select(x => x.ObjectVisualizationData).ToArray());
    }
    private ISimulationObject[] CaptureCurrentState()
    {
        List<ISimulationObject> result = new List<ISimulationObject>();
        foreach(var _unit in simulationObjectCollection.ToArray())
        {
            result.Add(_unit.Clone());
        }
        return result.ToArray();
    }
    private void SaveState()
    {
        simulationHistory.Add(new SimulationState()
        {
            FramesPassed = framesPassed,
            SimulationObjectCollection = CaptureCurrentState()
        });
    }
    private void Update() 
    {
        SimulateFrame();
    }
    private void SimulateFrameForAll(ISimulationObject[] data)
    {
        foreach(var simulatedObject in simulationObjectCollection)
        {
            if(simulatedObject == null) throw new Exception("Null object in collection! Not active objects should persist for proper handling of their death.");
            if(!simulatedObject.Active) continue;
            simulatedObject.SimulateFrame(data);
        }
    }
    [Button]
    public void AddRandomUnit()
    {
        simulationObjectCollection.Add(new Specie01());
    }
}
/// <summary>
/// A state of the simulation from which all current data can be extracted. By tracking this over time, a full evolutionary analysis can be obtained
/// </summary>
public struct SimulationState
{
    public long FramesPassed;
    public ISimulationObject[] SimulationObjectCollection;
}
/// <summary>
/// Any part of the simulation that should be taken into consideration by other parts of the simulation
/// </summary>
public interface ISimulationObject
{
    public string Code { get; set; }
    public Vector2 Position { get; set; }
    public void SimulateFrame(ISimulationObject[] simulationObjects) { }
    public bool Active { get; set; }
    public ISimulationObject Clone();
    public VisualizationData ObjectVisualizationData { get; set; }
}
/// <summary>
/// Active parts of the simulation
/// </summary>
public interface IAlive : ISimulationObject
{    
    [SerializeField] public string Genome { get; set; }
}
/// <summary>
/// A basic simulation unit with a functionality to survive by any means, and reproduce
/// </summary>
[Serializable]
public class SimulationUnit : IAlive
{
    [SerializeField] public string Genome { get; set; }
    [SerializeField] public string Code { get; set; }
    [SerializeField] public Vector2 Position { get; set; }
    [SerializeField] public bool Active { get; set; }
    [SerializeField] public long Age;
    VisualizationData ISimulationObject.ObjectVisualizationData { get => new VisualizationData()
    {
        Image = null,
        Position = Position,
        Rotation = new Vector3(),
        ObjectColor = Color.green,
        Scale = Vector3.one
    }; set => throw new NotImplementedException(); }

    public ISimulationObject Clone()
    {
        return new SimulationUnit()
        {
            Genome = Genome,
            Code = Code,
            Position = Position,
            Active = Active,            
        };
    }
    public void SimulateFrame(ISimulationObject[] simulationObjects)
    {
        Age++;
        Position += Movement();
    }
    protected virtual Vector2 Movement()
    {
        return new Vector2();
    }
    public SimulationUnit()
    {
        Code = Universe.GenerateRandomString(10,"0123456789");
        Genome = Universe.GenerateRandomString(10,"ABCD");
        Position = new Vector2(Random.Range(-5f,5f),Random.Range(-5f,5f));
        Active = true;
    }
}