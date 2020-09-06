using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using MonoNet.Util.Datatypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SimpleAStar
{
    private static readonly int maxTries = 4000;


    public SimpleAStar()
    {
    }

    /// <summary>
    /// Calculates the cloest path between two tiles "startTile" and "targetTile".
    /// Will return a Fast2DArray, which will return a sorted list of tiles to move to, in order.
    /// Or will return <code>null</code> if no path could be found (= i.e. of the path is completely blocked)
    /// </summary>
    /// <param name="startTile"></param>
    /// <param name="targetTile"></param>
    /// <returns></returns>
    public List<Tile> GeneratePath(Tile startTile, Tile targetTile, bool forceEntry = false)
    {
        int currentTries = 0;
        // List of Open (Not yet traversted) tiles. Populate first with start tile only.
        var openSet = new List<Tile>();
        openSet.Add(startTile);

        // Keeping track of the tiles we came form
        var cameFrom = new Dictionary<Tile, Tile>();

        // G Score = Cost from the start node to current node
        var gScore = new Dictionary<Tile, float>();
        gScore[startTile] = 0;

        currentTries = 0;
        while (openSet.Count > 0)
        {
            currentTries++;
            if (currentTries >= maxTries)
            {
                Debug.Log("Too many tries!");
                return null;
            }

            // Finding the tile with the currently lowest G-Score
            Tile currentTile =
                openSet.Where((x) => Math.Abs(gScore[x] - openSet.Min(y => gScore[y])) < 0.1f).ToList()[0];

            //If we cannot build on this tile, then forget it
            if (currentTile.Tower != null)
            {
                if (forceEntry == false || startTile != currentTile)
                {
                    openSet.Remove(currentTile);
                    continue;
                }
            }

            //If the current tile is the target tile, then we have finished the path
            if (currentTile == targetTile)
            {
                return ReconstructPath(cameFrom, currentTile);
            }

            //Set "default" value to infinity
            // if (!gScore.ContainsKey(currentTile)) gScore[currentTile] = 9999999;
            // "Mark" tile as visited
            openSet.Remove(currentTile);

            // Get all neighbours of this tile
            var currentNeighbours = GetNeighbours(currentTile);

            // Iterate over all of them
            for (var index = 0; index < currentNeighbours.Count; index++)
            {
                var neighbourTile = currentNeighbours[index];
                //Set "default" value to infinity
                if (!gScore.ContainsKey(neighbourTile)) gScore[neighbourTile] = 9999999;
                // Calculate a tentative G Score (temporary G score) for the current neighbour.
                var tentativeGScore = (int)(gScore[currentTile] + MathUtil.ManhattanDistance(currentTile.X,
                    currentTile.Y, neighbourTile.X,
                    neighbourTile.Y));

                // If the calculated score is lower than the neighbour g score, then this neighbour is better 
                if (tentativeGScore < gScore[neighbourTile])
                {
                    cameFrom[neighbourTile] = currentTile;
                    gScore[neighbourTile] = tentativeGScore;
                    if (!openSet.Contains(neighbourTile))
                    {
                        openSet.Add(neighbourTile);
                    }
                }
            }
        }


        return null;
    }

    /// <summary>
    /// Reconstructs the path backwards to create an iterable list, which has the required steps of tiles in order.
    /// </summary>
    /// <param name="cameFrom"></param>
    /// <param name="current"></param>
    /// <returns></returns>
    private List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
    {
        var totalPath = new List<Tile>();
        totalPath.Add(current);
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath = totalPath.Prepend(current).ToList();
        }

        return totalPath;
    }

    /// <summary>
    /// Puts all direct, non-diagonal neighbours into a list to iterate over.
    /// Loops over all neighbours with an x,y offset from -1 to 1 inclusive, checks if they are still in bounds and then adds them to the list
    /// </summary>
    /// <param name="originTile"></param>
    /// <returns></returns>
    private List<Tile> GetNeighbours(Tile originTile)
    {
        var sx = originTile.X;
        var sy = originTile.Y;

        var neighbours = new List<Tile>();
        var wTiles = World.Instance.Tiles;

        // Iterate the x offset
        for (var ox = -1; ox <= 1; ox++)
        {
            // Iterate the y offset
            for (var oy = -1; oy <= 1; oy++)
            {
                //Check if diagonal or both zero (if both are not null, then you are diagonal)

                if ((ox == 0 && oy == 0) || (ox != 0 && oy != 0)) continue;

                //Calculate absolute position of neighbour tile
                var px = sx + ox;
                var py = sy + oy;


                // Check if these new coords are in tile bound
                if (wTiles.InBounds(px, py))
                {
                    Tile t = wTiles.Get(px, py);
                    if (!t || t.Tower) continue;
                    Vector2Int test = new Vector2Int(t.X, t.Y);
                    neighbours.Add(t);
                        
                }
            }
        }

        return neighbours;
    }
}