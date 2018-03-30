﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

	public static GameObject CreatePlane(float width, float height, Material mat)
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();

        m.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(width, height, 0),
            new Vector3(0, height, 0)
        };

        m.uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };

        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        mf.mesh = m;
        // Create texture
        Texture2D tex = new Texture2D(2, 2);
        

        //tex.LoadImage(Managers.Images.GetWebImage(OnWebImage));

        // Apply texture
        mr.material = mat;

        m.RecalculateBounds();
        m.RecalculateNormals();

        go.transform.Rotate(0, 180, 0);
        return go;
    }

    /**private void OnWebImage(Texture2D image)
    {
        //Renderer targetObject = GameObject.Find("Plane").GetComponent<Renderer>();
        //targetObject.material.mainTexture = image;
        Debug.Log("Found it");
    }**/
    
}