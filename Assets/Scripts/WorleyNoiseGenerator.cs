using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static float[,] GenerateVeroni(int mapSize, float frequency, float displacement, int seed, bool useDistance, Vector2 offset)
    {
        LibNoise.Generator.Voronoi noise = new LibNoise.Generator.Voronoi(frequency, displacement, seed, useDistance);
        LibNoise.Operator.Turbulence turbulence = new LibNoise.Operator.Turbulence(noise);

        float[,] heightMap = new float[mapSize, mapSize];

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {

                // heightMap[x, y] = (float)noise.GetValue(x + offset.x, 1, y + offset.y);
                heightMap[x, y] = (float)turbulence.GetValue(x + offset.x, 1, y + offset.y);
            }
        }

        return heightMap;
    }

    public static float[,] GenerateFastNoiseLite(int mapSize, float jitter, float frequency, int seed, Vector2 offset)
    {
        FastNoiseLite noiseLite = new FastNoiseLite();
        //FastNoiseLite domainWarp = new FastNoiseLite();
        float[,] noiseMap = new float[mapSize, mapSize];

        noiseLite.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
        noiseLite.SetSeed(seed);
        noiseLite.SetCellularDistanceFunction(FastNoiseLite.CellularDistanceFunction.Hybrid); // Euclidean, Euclidean sqrd, Hybrid, Manhatten
        noiseLite.SetCellularJitter(jitter);
        noiseLite.SetCellularReturnType(FastNoiseLite.CellularReturnType.CellValue); // Check other types for diffrent pixle values
        noiseLite.SetFrequency(frequency);
        //domainWarp.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
        //domainWarp.SetDomainWarpAmp(10f);

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                //domainWarp.DomainWarp(ref x, ref y);
                noiseMap[x, y] = noiseLite.GetNoise(x + offset.x, y + offset.y);
            }
        }

        return noiseMap;
    }

    /*public static Color[] GenerateFastNoiseTwo(int size, int seed)
    {
        Color[] noiseMap = new Color[size * size];

        FastNoise cellular = new FastNoise("CellularDistance");
        cellular.Set("ReturnType", "Index0Add1");
        cellular.Set("DistanceIndex0", 2);

        FastNoise fractal = new FastNoise("FractalFBm");
        fractal.Set("Source", new FastNoise("Simplex"));
        fractal.Set("Gain", 0.3f);
        fractal.Set("Lacunarity", 0.6f);

        FastNoise addDim = new FastNoise("AddDimension");
        addDim.Set("Source", cellular);
        addDim.Set("NewDimensionPosition", 0.5f);
        // or
        // addDim.Set("NewDimensionPosition", new FastNoise("Perlin"));

        FastNoise maxSmooth = new FastNoise("MaxSmooth");
        maxSmooth.Set("LHS", fractal);
        maxSmooth.Set("RHS", addDim);


        float[] noiseData = new float[size * size];
        FastNoise.OutputMinMax minMax = maxSmooth.GenUniformGrid2D(noiseData, 0, 0, size, size, 0.02f, seed);

        float scale = 255.0f / (minMax.max - minMax.min);

        int i = 0;
        foreach (float noise in noiseData)
        {
            //Scale noise to 0 - 255
            int noiseI = (int)Mathf.Round((noise - minMax.min) * scale);

            noiseMap[i] = new Color(Mathf.Clamp(noiseI, 0, 255), 0, 0);
            i++;
        }

        return noiseMap;
    }*/
}
