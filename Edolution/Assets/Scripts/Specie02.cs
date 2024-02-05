using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Specie02 : SimulationUnit, IAlive
{
    protected float[] expressedGenome;
    VisualizationData ISimulationObject.ObjectVisualizationData { get => new VisualizationData()
    {
        Image = Database.GetImage("Cell03"),
        Position = Position,
        Rotation = new Vector3(0,0,directionAngle),
        ObjectColor = Color.white,
        Scale = Vector3.one
    }; set => throw new NotImplementedException(); }
    private float directionAngle = Random.Range(0,360f);
    protected override Vector2 Movement()
    {
        directionAngle += Random.Range(expressedGenome[1],expressedGenome[1]) * ((Age%10==0)?10:1);
        return Universe.AngleToNormalizedVector(directionAngle) * expressedGenome[2] * expressedGenome[1];
    }
    public Specie02()
    {
        Code = Universe.GenerateRandomString(10,"0123456789");
        Genome = Universe.GenerateRandomString(10,"ABCD");
        Position = new Vector2(Random.Range(-5f,5f),Random.Range(-5f,5f));
        Active = true;
        Initialize();
    }
    public override void Initialize()
    {
        expressedGenome = Universe.ExpressGenome(Genome, new int[]{ 2, 4, 4}, "ABCD");
    }
}