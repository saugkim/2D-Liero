using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PaintingPortion : MonoBehaviour
{

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 60, 100, 20), "Transparent %"))
        {
            FindTranparentShare();
        }
    }

     
    public void FindTranparentShare()
    {
        int count = 0;
        Renderer rend = transform.GetComponent<Renderer>();
        Texture2D tex = rend.material.mainTexture as Texture2D;

        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                if (tex.GetPixel(x, y).a == 0)
                    count++;
            }
        }

        int totalPixelNumber = tex.width * tex.height;
        float tranparentShare = (float)count / totalPixelNumber;
        double tranparentSharePercent = Math.Round(tranparentShare * 100, 2);
        Debug.Log(tranparentSharePercent + "%");

    }

}
