using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Continent
{
    NA,
    SA,
    AS,
    AF,
    EU,
    AU
}


public enum EntityType
{
    Creature,
    god,
    Event
}
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Entity", order = 1)]
public class Entity : ScriptableObject
{
    [Header("InGame Info")]
    [SerializeField] EntityType isCreature;
    [Header("OutGame Info")]
    [SerializeField] string entityName;
    [SerializeField] int continent;
    [SerializeField] string country;
    [SerializeField] Sprite image;
    [TextArea(5, 5)]
    [SerializeField] string description;
    [SerializeField] List<string> hints;

    public EntityType IsCreature
    {
        get { return isCreature; }
    }

    public string EntityName
    {
        get { return entityName; }
    }

    public int Continent
    {
        get { return continent; }
    }

    public string Country
    {
        get { return country; }
    }

    public Sprite Image
    {
        get { return image; }
    }

    public string Description
    {
        get { return description; }
    }

    public List<string> Hints
    {
        get { return hints; }
    }
}
