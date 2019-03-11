using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class SpriteAnimationCreator : MonoBehaviour {

    private static float defaultInterval = 0.1f;

    [MenuItem("Assets/Create/Sprite Animation")]
    public static void Create()
    {
        List<Sprite> selectedSprites = new List<Sprite>(Selection.GetFiltered(typeof(Sprite), SelectionMode.DeepAssets).OfType<Sprite>());
        Object[] selectedTextures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

        foreach(Object texture in selectedTextures)
        {
            selectedSprites.AddRange(AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture)).OfType<Sprite>());
        }

        if(selectedSprites.Count < 1)
        {
            Debug.LogWarning("No sprite selected.");
            return;
        }

        string suffixPattern = "_?([0-9]+)$";
        selectedSprites.Sort((Sprite a, Sprite b) =>
        {
            Match match1 = Regex.Match(a.name, suffixPattern);
            Match match2 = Regex.Match(b.name, suffixPattern);
            if(match1.Success && match2.Success)
            {
                return (int.Parse(match1.Groups[1].Value) - int.Parse(match2.Groups[1].Value));
            }
            else
            {
                return a.name.CompareTo(b.name);
            }
        });

        string baseDir = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedSprites[0]));
        string baseName = Regex.Replace(selectedSprites[0].name, suffixPattern, "");
        if (string.IsNullOrEmpty(baseName))
        {
            baseName = selectedSprites[0].name;
        }
        Canvas canvas = FindObjectOfType<Canvas>();
        if(canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<GraphicRaycaster>();
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.layer = LayerMask.NameToLayer("UI");
        }

        GameObject obj = new GameObject(baseName);
        obj.transform.parent = canvas.transform;
        obj.transform.localPosition = Vector3.zero;

        Image image = obj.AddComponent<Image>();
        image.sprite = (Sprite)selectedSprites[0];
        image.SetNativeSize();

        Animator animator = obj.AddComponent<Animator>();

        AnimationClip animationClip = AnimatorController.AllocateAnimatorClip(baseName);

        EditorCurveBinding editorCurveBinding = new EditorCurveBinding();
        editorCurveBinding.type = typeof(Image);
        editorCurveBinding.path = "";
        editorCurveBinding.propertyName = "m_Sprite";

        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[selectedSprites.Count];

        for(int i = 0;i < selectedSprites.Count; i++)
        {
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = i * defaultInterval;
            keyFrames[i].value = selectedSprites[i];
        }

        AnimationUtility.SetObjectReferenceCurve(animationClip, editorCurveBinding, keyFrames);

        SerializedObject serializedAnimationCilp = new SerializedObject(animationClip);
        SerializedProperty serializedAnimationClipSettings = serializedAnimationCilp.FindProperty("m_AnimationClipSettings");
        serializedAnimationClipSettings.FindPropertyRelative("m_LoopTime").boolValue = true;
        serializedAnimationCilp.ApplyModifiedProperties();

        SaveAsset(animationClip, baseDir + "/" + baseName + ".anim");

        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPathWithClip(baseDir + "/" + baseName + ".controller", animationClip);
        animator.runtimeAnimatorController = (RuntimeAnimatorController)animatorController;
    }

    private static void SaveAsset(Object obj,string path)
    {
        Object existingAsset = AssetDatabase.LoadMainAssetAtPath(path);
        if(existingAsset != null)
        {
            EditorUtility.CopySerialized(obj, existingAsset);
            AssetDatabase.SaveAssets();
        }
        else
        {
            AssetDatabase.CreateAsset(obj, path);
        }
    }
}
