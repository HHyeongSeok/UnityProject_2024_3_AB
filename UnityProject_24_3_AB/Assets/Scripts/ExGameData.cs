using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "Game Data", order = 50)]
public class ExGameData : ScriptableObject
{
    public string gameName;
    public int gameScore;
    public bool isGameActive;
}
