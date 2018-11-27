using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component goes to Camera

public class CameraDraw : MonoBehaviour {

    public Camera cam; 

	void Start () {

        cam = GetComponent<Camera>();
	}

	void Update () {
        
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        // This shoots a ray to the mouse locations on screen
        // Hit results will be stored to the "hit" variable
        //RaycastHit hit;
        //if(!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        //{
        //    // In this case the ray has hit nothing, so we do not continue Update() loop
        //    return; 
        //}
        //// We have hit the object with ray  

        //Renderer rend = hit.transform.GetComponent<Renderer>();
        //MeshCollider meshCollider = hit.collider as MeshCollider;

        //// Let's check that the background has all the "features" we need to be 
        //// able to make drawing

        //if(rend == null || rend.sharedMaterial == null || 
        //    rend.sharedMaterial.mainTexture == null || meshCollider == null)
        //{
        //    return; 
        //}

        //// We are ready to do some painting
        //Texture2D tex = rend.material.mainTexture as Texture2D;
        //Vector2 pixelUV = hit.textureCoord;

        //pixelUV.x *= tex.width;
        //pixelUV.y *= tex.height;
        //Debug.Log(pixelUV.x);


        //tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.clear);
        //Debug.Log((int)pixelUV.x);
        //tex.Apply();

    }

    public bool DrawRay(Vector3 testray)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(testray)), out hit))
        {
            return false;
        }
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null ||
            rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        // We check if the pixel's alpha value is 1.0, eli opaque

        if(tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
     //       Debug.Log("VÄRI: " + tex.GetPixel((int)pixelUV.x, (int)pixelUV.y));
            return true;

        }
        else
        {
            return false; 
        }

    }


    public bool PaintRay(Vector3 testray)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(testray)), out hit))
        {
            return false;
        }
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null ||
            rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        // We check if the pixel's alpha value is 1.0
        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            Debug.Log("VÄRI: " + tex.GetPixel((int)pixelUV.x, (int)pixelUV.y));
            // Ammo has hit the wall, let's draw a transparent circle to the texture
            Circle(tex, (int)pixelUV.x, (int)pixelUV.y, 5, Color.clear);
            // IMPORTANT!
            tex.Apply();
            return true;

        }
        else
        {
            return false;
        }

    }

    public bool Digging(Vector3 location, int radius)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(location)), out hit))
        {
            return false;
        }
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null ||
            rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        // We check if the pixel's alpha value is 1.0
        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            Debug.Log("VÄRI: " + tex.GetPixel((int)pixelUV.x, (int)pixelUV.y));
            // Ammo has hit the wall, let's draw a transparent circle to the texture
            Circle(tex, (int)pixelUV.x, (int)pixelUV.y, radius, Color.clear);
            // IMPORTANT!
            tex.Apply();
            return true;

        }
        else
        {
            return false;
        }
    }

    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;

        for(x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

            for(y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);
            }
        }
    }


}
