using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRay : MonoBehaviour
{

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
    }

    public bool RayHitObject(Vector3 testray)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(testray)), out hit))
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

        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
