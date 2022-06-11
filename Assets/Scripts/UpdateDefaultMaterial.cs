using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
class UpdateDefaultMaterial
{
    static Renderer[] MRenderers;

    // let users turn on 
    [MenuItem("Tools/URP/Update Default Material")]
    static void UpdateMaterials()
    {
        MRenderers = Object.FindObjectsOfType<Renderer>();
        Shader s = Shader.Find("Universal Render Pipeline/Lit");
        Material m = new Material(s);
        AssetDatabase.CreateAsset(m, "Assets/Materials/DefaultLitMaterial.mat");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        foreach (Renderer mr in MRenderers)
        {
            if (mr.sharedMaterial == null ||
                mr.sharedMaterial.name.Contains("Default-Material") ||
                mr.sharedMaterial.shader.name == "Standard"
                )
            {
                mr.sharedMaterial = m;
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                Debug.Log(mr.gameObject.name + " -> set material to URP/Lit");
            }
        }
    }


}
