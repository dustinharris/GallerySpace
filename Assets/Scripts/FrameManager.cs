using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager {

    private GameObject[] framesObjects;
    private bool[] framesSet;
    private GameObject[] orderedFramesObjects;

    public void InitialFrameLoad ()
    {
        Debug.Log("Made it to load frames");
        
        framesObjects = GameObject.FindGameObjectsWithTag("Frame");
       
        Debug.Log("Frame count: " + framesObjects.Length.ToString());

        framesSet = new bool[framesObjects.Length];
        orderedFramesObjects = new GameObject[framesObjects.Length];

        if (framesObjects.Length > 5)
        {
            GameObject.Find("Removable Wall 1").SetActive(false);
        }
        if (framesObjects.Length > 10)
        {
            GameObject.Find("Removable Wall 2").SetActive(false);
        }

        // Initialize framesSet & orderedFramesObjects
        for (int i = 0; i < framesObjects.Length; i++)
        {
            Debug.Log("lengthhhh");
            framesSet[i] = false;
            string frameString = "Picture" + (i + 1).ToString() + " Frame";
            Debug.Log(frameString);
            orderedFramesObjects[i] = GameObject.Find(frameString);
        }
    }

    public void AddImageToFrame(Texture2D wallImage)
    {
        int storeImageLocation = -1;
        int onImageLocation = 0;

        // Find first empty spot in frames array, put image there
        while (storeImageLocation == -1) {
            if (framesSet[onImageLocation] == false)
            {
                Debug.Log("Storing image at " + onImageLocation.ToString());
                storeImageLocation = onImageLocation;
                framesSet[onImageLocation] = true;
                orderedFramesObjects[storeImageLocation].GetComponent<Renderer>().material.mainTexture = wallImage;
                float existingdepth = framesObjects[storeImageLocation].transform.parent.localScale.x;
                float existingwidth = framesObjects[storeImageLocation].transform.parent.localScale.z;
                float existingheight = framesObjects[storeImageLocation].transform.parent.localScale.y;
                
                // Normalize to Height
                if (wallImage.height > wallImage.width)
                {
                    framesObjects[storeImageLocation].transform.parent.transform.localScale = new Vector3(existingdepth, existingheight, (float)wallImage.width / wallImage.height * existingwidth);
                    //framesObjects[storeImageLocation].transform.parent.transform.localScale = new Vector3(10, 10, 10);
                }

                // Normalize to Width
                if (wallImage.width > wallImage.height)
                {
                    framesObjects[storeImageLocation].transform.parent.transform.localScale = new Vector3(existingdepth, wallImage.height / (float)wallImage.width * existingheight, existingwidth);
                }
            }
            else if (storeImageLocation > framesObjects.Length)
            {
                Debug.Log("more pictures than frames");
            } else
            {
                onImageLocation++;
            }
        }
    }
}
