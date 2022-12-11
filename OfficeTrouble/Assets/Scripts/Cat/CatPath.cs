using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "catPath", menuName = "ScriptableObject/CatPath", order = 100)]
public class CatPath : ScriptableObject
{
    public List<Vector2> queue;
}