using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AwakeScreenEffect : MonoBehaviour
{
    public Shader shader;

    [SerializeField]
    Material material;
    Material Material 
    {
        get 
        {
            if (material == null)
            {
                material           = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
            }
            return material;
        }
    }
    
    [Range(0f, 1f)][Tooltip("苏醒进度")]
    public float progress;
    
    void OnRenderImage(RenderTexture src, RenderTexture dest) 
    {
        Material.SetFloat("_Progress", progress);
        Graphics.Blit(src, dest, material);
    }

    public async void CloseEye(Button _openEyeButton)
    {
        await DOTween.To(() => progress, (_value => progress = _value), 0f, 2f).AsyncWaitForCompletion();
        _openEyeButton.gameObject.SetActive(true);
    }

    public async void OpenEye(Button _closeEyeButton)
    {
        await DOTween.To(() => progress, (_value => progress = _value), 1f, 2f).AsyncWaitForCompletion();
        _closeEyeButton.gameObject.SetActive(true);
    }
}
