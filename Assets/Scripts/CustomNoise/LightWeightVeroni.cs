using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightWeightVeroni
{
    Vector2[,] grid;
    public LightWeightVeroni(int size, float radius, int seed)
    {
        PoissonDiscSampler poissonDisc = new PoissonDiscSampler(size, size, radius, seed);

        List<Vector2> points = poissonDisc.Samples().ToList();

        float cellSize = radius / 1.41421356237f;
        grid = new Vector2[Mathf.CeilToInt(size / cellSize), Mathf.CeilToInt(size / cellSize)];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                
            }
        }
    }

    /*public Vector2 getPoint(int x, int y)
    {

    }*/
}
