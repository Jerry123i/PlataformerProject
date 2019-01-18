using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadersCorrector : MonoBehaviour {

    public Camera gateCamera;
    public Camera doorsCamera;

    public SpriteRenderer koizo;
    public List<Material> gates;

	void Start () {

        Vector2 screenSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

        RenderTexture rtGates = new RenderTexture((int)screenSize.x, (int)screenSize.y, 24, RenderTextureFormat.ARGB32);
        RenderTexture rtDoors = new RenderTexture((int)screenSize.x, (int)screenSize.y, 24, RenderTextureFormat.ARGB32);

        rtGates.Create();
        rtDoors.Create();

        gateCamera.targetTexture = rtGates;
        doorsCamera.targetTexture = rtDoors;
        
        koizo.sharedMaterial.SetTexture("Texture2D_2F7F96CD", rtDoors);

        foreach(Material g in gates)
        {
            g.SetTexture("Texture2D_B90852FE", rtGates);
        }

    }
}
