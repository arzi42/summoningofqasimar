using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EditorTools : MonoBehaviour
{
    [MenuItem("Tools/Set Sigils")]
    private static void SetSigils()
    {
        var images = FindObjectsByType<Image>(FindObjectsSortMode.None);
        
        Debug.Log($"found {images.Length} images");

        foreach (var image in images)
        {
            foreach (var obj in Selection.objects)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
                
                Debug.Log($"{path} has {sprites.Length} sprites");

                foreach (var sprite in sprites)
                {
                    if (sprite.name == image.name)
                        image.sprite = sprite;
                }
            }
        }
    }

    [MenuItem("Tools/Remove Texts")]
    private static void RemoveTexts()
    {
        foreach (var o in Selection.gameObjects)
        {
            var tmp = o.GetComponentInChildren<TextMeshProUGUI>();
            
            DestroyImmediate(tmp.gameObject);
        }
    }
}
