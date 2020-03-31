using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CRTImageEffect : MonoBehaviour
{
    public Shader CRT;
    public int channelStep;
    [Range(0f, 1f)] public float strength1;
    [Range(0f, 1f)] public float strength2;
    [Range(0f, 3f)] public float contrast;
    [Range(-1f, 1f)] public float brightness;
    public int scanlineStep;
    public Color scanlineColour;
    public float curvature;

    private Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!material) material = new Material(CRT);

        material.SetInt("_ChannelStep", channelStep);

        material.SetFloat("_Strength1", strength1);
        material.SetFloat("_Strength2", strength1);

        material.SetFloat("_Contrast", contrast);
        material.SetFloat("_Brightness", brightness);

        material.SetColor("_ScanlineColour", scanlineColour);
        material.SetInt("_ScanlineStep", scanlineStep);

        material.SetFloat("_Curvature", curvature);

        Graphics.Blit(source, destination, material);
    }
}
