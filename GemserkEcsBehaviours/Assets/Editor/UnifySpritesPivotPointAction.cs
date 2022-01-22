using UnityEditor;
using UnityEngine;

public static class UnifySpritesPivotPointAction 
{
    [MenuItem("Assets/Utilities/Unify Sprites Pivot Points")]
    public static void UnifySpritesPivotPoint()
    {
        // sets the first sprite's pivot point to all the selected sprites

        var selection = Selection.objects;
        if (selection.Length == 0)
            return;

        TextureImporter first = null;

        foreach (var t in selection)
        {
            Debug.Log(t.GetType());
            
            var texture = t as Texture2D;

            if (texture == null)
                continue;
            
            var textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture)) as TextureImporter;
            
            if (first == null)
                first = textureImporter;

            textureImporter.spritePivot = first.spritePivot;
            
            EditorUtility.SetDirty(textureImporter);
        }
        
        AssetDatabase.SaveAssets();
    }
}
