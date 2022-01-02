using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BiomeMapper : MonoBehaviour
{
    private Dictionary<Vector2, int> fullHashes;
    private float percentWater;
    public Dictionary<int, string> biomeDictonairy;


    public BiomeMapper(Dictionary<Vector2, int> fullHashes, float percentWater)
    {
        this.fullHashes = fullHashes;
        this.percentWater = percentWater;
        biomeDictonairy = new Dictionary<int, string>();
    }
        
    public void MapHashesToBiomes(Dictionary<Vector2, int> fullHashes, float percentWater)
    {
        // var actualHashes = fullHashes.GroupBy(x => x.Value).Where(x => x.Count() > 1);  // Might have better performance
        var actualHashes = fullHashes.Values.Distinct().ToList(); // Finds all unique hashes

        biomeDictonairy.Add(fullHashes[Vector2.zero], "Grasslands"); // makes the starting point be grasslands

        AddWater(actualHashes, percentWater);

        for (int i = 0; i < actualHashes.Count; i++)
        {
            if (actualHashes[i] == fullHashes[Vector2.zero] || biomeDictonairy[actualHashes[i]].Equals("Nothing")) continue;

            Vector2 location = fullHashes.FirstOrDefault(x => x.Value == actualHashes[i]).Key;

            float randomValue = Random.value;

            if (Vector2.Distance(location, Vector2.zero) <= 300)
            {
                biomeDictonairy[actualHashes[i]].Equals("Grasslands");
            } else if (Vector2.Distance(location, Vector2.zero) <= 600)
            {
                if (biomeDictonairy[actualHashes[i - 1]].Equals("Forest"))
                {
                    if (randomValue <= 0.7f)
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Forest");
                    }
                    else
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Grasslands");
                    }
                }
                else
                {
                    if (randomValue <= 0.5f)
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Forest");
                    }
                    else
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Grasslands");
                    }
                }
            } else if (Vector2.Distance(location, Vector2.zero) <= 1000)
            {

                if(!actualHashes[i - 1].Equals("Water") && !actualHashes[i + 1].Equals("Water"))
                {
                    if (randomValue <= 0.3)
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Mountians");
                    } else if (randomValue <= 0.7)
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Forest");
                    }
                    else
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Grasslands");
                    }
                }
                else
                {
                    if (randomValue <= 0.2)
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Beach");
                    }
                    else if (randomValue <= 0.6)
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Forest");
                    }
                    else
                    {
                        biomeDictonairy[actualHashes[i]].Equals("Grasslands");
                    }
                }
            }
            else
            {
                biomeDictonairy[actualHashes[i]].Equals("Water");
            }
        }
    }

    private void AddWater(List<int> actualHashes, float percentWater)
    {
        int waterTiles = Mathf.RoundToInt(actualHashes.Count * percentWater);

        for (int i = 0; i < actualHashes.Count; i++)
        {
            if (biomeDictonairy.ContainsKey(actualHashes[i])) continue; // Checking if biomeDictonairy already has the hash that we are trying to add to it

            if (actualHashes.Count - i <= waterTiles)
            {
                biomeDictonairy.Add(actualHashes[i], "Water");
                continue;
            }

            // Vector2 location = fullHashes.FirstOrDefault(x => x.Value == actualHashes[i]).Key; // Keeping this hear incase I want to lower the ammount of water at a certain range

            if (Random.value <= percentWater)
            {
                biomeDictonairy.Add(actualHashes[i], "Water");
            }
            else
            {
                biomeDictonairy.Add(actualHashes[i], "Nothing");
            }
        }
    }
}
