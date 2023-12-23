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
        Image = Database.GetImage("Specie01"),
        Position = Position,
        Rotation = new Vector3(0,0,directionAngle),
        ObjectColor = Color.white,
        Scale = Vector3.one
    }; set => throw new NotImplementedException(); }
    private float directionAngle = Random.Range(0,360f);
    protected override Vector2 Movement()
    {
        if(Age%10==0) directionAngle += Random.Range(-30f,30f);
        return Universe.AngleToNormalizedVector(directionAngle) * 0.01f;
    }
}