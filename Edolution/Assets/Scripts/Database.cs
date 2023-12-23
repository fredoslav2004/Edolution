using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Database : SerializedMonoBehaviour
{
    public static Database Instance;
    [SerializeField] private Dictionary<string, Sprite> imageDB;
    public static Sprite GetImage(string code) => Instance.imageDB[code];
    private void Awake() {
        Instance = this;
    }
}
