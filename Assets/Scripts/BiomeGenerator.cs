using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    // https://gamedev.stackexchange.com/questions/186194/how-to-randomly-generate-biome-with-perlin-noise
    // Step 1
    // Generate a map with random zones for each biome

    // Step 2
    // For each zone find which biome its in along with the biomes near by it
    // Get a the avrage influence on the height of the nearby biomes and put that in a varible

    // Step 3 
    // Generate the heightmap for each biome sepertly then get the avrage of the ones per chunk and combine them
}
