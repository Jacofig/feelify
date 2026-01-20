using System.Collections.Generic;

public class Metal
{
    // definicja
    public string id;
    public float minHitTemp;
    public float maxTemp;

    // runtime state
    public float temperature;
    public int hits;
    public bool enchanted;

    public HashSet<string> additives = new();
}
