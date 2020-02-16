using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class Slot
{
    public GameObject GameObject { get; }
    public Vector3 SpawnPosition { get; }
    public bool IsGraveStone { get; }
    public int X { get; }
    public int Y { get; }

    public Slot(int x, int y)
    {
        SpawnPosition = Vector3.zero;
        X = x;
        Y = y;
    }

    public Slot(GameObject gameObject, Vector3 pos, int x, int y)
    {
        GameObject = gameObject;
        SpawnPosition = pos;
        IsGraveStone = IsGameObjectGraveStone(gameObject);
        X = x;
        Y = y;
    }

    private static bool IsGameObjectGraveStone(GameObject gameObject)
    {
        return gameObject.CompareTag("Grave");
    }
}
