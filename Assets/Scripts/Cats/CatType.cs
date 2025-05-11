using UnityEngine;

[CreateAssetMenu(menuName = "Cat/Create New Cat")]
public class CatType : ScriptableObject
{
    public string id;
    public GameObject visualPrefab;
    public int value;
    public float fallSpeed;
    public float spawnWeight;
}