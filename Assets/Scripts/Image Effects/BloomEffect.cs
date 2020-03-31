using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BloomEffect : MonoBehaviour
{
    public Shader bloomShader;
    public Material radialBlur;

    public bool debug;

    [Range(1, 16)]
    public int iterations = 1;
    [Range(0, 10)]
    public float threshold = 1;
    [Range(0, 1)]
    public float softThreshold = 0.5f;
    [Range(0, 10)]
    public float intensity = 1;

    Material bloom;

    const int BoxDownPrefilterPass = 0;
    const int BoxDownPass = 1;
    const int BoxUpPass = 2;
    const int ApplyBloomPass = 3;
    const int DebugBloomPass = 4;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (bloom == null)
        {
            bloom = new Material(bloomShader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
        }

        //bloom.SetFloat("_Threshold", threshold);
        //bloom.SetFloat("_SoftThreshold", softThreshold);
        float knee = threshold * softThreshold;
        Vector4 filter;
        filter.x = threshold;
        filter.y = filter.x - knee;
        filter.z = 2f * knee;
        filter.w = 0.25f / (knee + 0.00001f);

        bloom.SetVector("_Filter", filter);
        bloom.SetFloat("_Intensity", Mathf.GammaToLinearSpace(intensity));

        RenderTexture[] textures = new RenderTexture[16];

        int width = source.width / 2;
        int height = source.height / 2;
        RenderTextureFormat format = source.format;

        RenderTexture currentDestination = textures[0] = RenderTexture.GetTemporary(
            width, height, 0, format
        );
        Graphics.Blit(source, currentDestination, bloom, BoxDownPrefilterPass);
        RenderTexture currentSource = currentDestination;

        int i = 1;
        for ( ; i < iterations; i++)
        {
            width /= 2;
            height /= 2;
            if (Mathf.Min(width, height) < 2) break;
            currentDestination = textures[i] = RenderTexture.GetTemporary(width, height, 0, format);
            Graphics.Blit(currentSource, currentDestination, bloom, BoxDownPass);
            currentSource = currentDestination;
        }

        for (i -= 2; i >= 0; i--)
        {
            currentDestination = textures[i];
            textures[i] = null;
            Graphics.Blit(currentSource, currentDestination, bloom, BoxUpPass);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }

        RenderTexture bloomDestination = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGBHalf);

        if (debug)
        {
            Graphics.Blit(currentSource, destination, bloom, DebugBloomPass);
        } else
        {
            bloom.SetTexture("_SourceTex", source);
            Graphics.Blit(currentSource, destination, bloom, ApplyBloomPass);
        }
        /*
        currentSource = bloomDestination;
        Graphics.Blit(currentSource, destination);
        */
        RenderTexture.ReleaseTemporary(currentSource);
    }
}
