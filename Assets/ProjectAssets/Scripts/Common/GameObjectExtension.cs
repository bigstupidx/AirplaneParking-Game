using UnityEngine;

public static class GameObjectExtension
{
    public static GameObject Create(this GameObject gameObject)
    {
        return GameObject.Instantiate(gameObject) as GameObject;
    }
}
