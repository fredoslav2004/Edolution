using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Specie01 : SimulationUnit, IAlive
{
    VisualizationData ISimulationObject.ObjectVisualizationData { get => new VisualizationData()
    {
        Image = null,
        Position = Position,
        Rotation = new Vector3(),
        ObjectColor = Color.blue,
        Scale = Vector3.one
    }; set => throw new NotImplementedException(); }
    private float directionAngle = Random.Range(0,6.283f);
    protected override Vector2 Movement()
    {
        if(Age%10==0) directionAngle += Random.Range(-0.1f,0.1f);
        return Universe.AngleToNormalizedVector(directionAngle) * 0.01f;
    }
}