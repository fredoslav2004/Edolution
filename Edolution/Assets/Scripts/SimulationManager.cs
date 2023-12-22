using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Dictionary<string, ISimulationObject> simulationObjectCollection;
    private ISimulationObject[] getAllObjects => simulationObjectCollection.Values.ToArray();
    #endregion
}
public interface ISimulationObject
{
    public string code { get; set; }
    public Vector2 Position { get; set; }
    public virtual void SimulateFrame(ISimulationObject[] simulationObjects) { }
}
public struct SimulationState
{
    public long FramesPassed;
    public ISimulationObject[] SimulationObjectCollection;
}
