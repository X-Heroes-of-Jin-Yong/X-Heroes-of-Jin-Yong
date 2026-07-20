using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BigEvents.Editor
{
    public class SpriteReplacer : EditorWindow
    {
        private Sprite oldSprite;
        private Sprite newSprite;
        private bool includeScenes = true;
        private bool includePrefabs = true;
        private bool includeAllScenes = false; // 新增选项：是否包含所有场景

        [MenuItem("Big Events/Replace Sprite")]
        public static void ShowWindow()
        {
            GetWindow<SpriteReplacer>("Sprite Replacer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Sprite Replacement Tool", EditorStyles.boldLabel);

            oldSprite = (Sprite)EditorGUILayout.ObjectField("Old Sprite", oldSprite, typeof(Sprite), false);
            newSprite = (Sprite)EditorGUILayout.ObjectField("New Sprite", newSprite, typeof(Sprite), false);

            includeScenes = EditorGUILayout.Toggle("Include Scenes", includeScenes);
            
            // 只有当includeScenes为true时才显示includeAllScenes选项
            if (includeScenes)
            {
                EditorGUI.indentLevel++;
                includeAllScenes = EditorGUILayout.Toggle("Include All Scenes (not just current)", includeAllScenes);
                EditorGUI.indentLevel--;
            }
            
            includePrefabs = EditorGUILayout.Toggle("Include Prefabs", includePrefabs);

            if (GUILayout.Button("Find and Replace"))
            {
                if (oldSprite == null || newSprite == null)
                {
                    EditorUtility.DisplayDialog("Error", "Please select both old and new sprites", "OK");
                    return;
                }

                ReplaceSprites();
            }
        }

        private void ReplaceSprites()
        {
            int replacementCount = 0;

            // 处理场景中的Sprite
            if (includeScenes)
            {
                if (includeAllScenes)
                {
                    // 保存当前场景路径，以便稍后恢复
                    string currentScenePath = SceneManager.GetActiveScene().path;
                    bool currentSceneIsDirty = EditorSceneManager.GetActiveScene().isDirty;
                    
                    // 获取所有场景路径
                    string[] allScenePaths = AssetDatabase.FindAssets("t:Scene")
                        .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                        .ToArray();
                    
                    // 提示用户保存当前场景
                    if (currentSceneIsDirty)
                    {
                        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            EditorUtility.DisplayDialog("Operation Cancelled", "Scene replacement cancelled. Please save your scene first.", "OK");
                            return;
                        }
                    }
                    
                    // 处理每个场景
                    for (int i = 0; i < allScenePaths.Length; i++)
                    {
                        string scenePath = allScenePaths[i];
                        EditorUtility.DisplayProgressBar("Processing Scenes", $"Replacing sprites in {scenePath}", 
                            i / (float)allScenePaths.Length);
                        
                        // 打开场景
                        Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                        
                        // 获取场景中的所有根游戏对象
                        var rootObjects = scene.GetRootGameObjects();
                        foreach (var rootObject in rootObjects)
                        {
                            replacementCount += ReplaceInGameObject(rootObject);
                        }
                        
                        // 如果有修改，保存场景
                        if (scene.isDirty)
                        {
                            EditorSceneManager.SaveScene(scene);
                        }
                    }
                    
                    // 恢复到原始场景
                    EditorUtility.ClearProgressBar();
                    EditorSceneManager.OpenScene(currentScenePath);
                }
                else
                {
                    // 只处理当前场景
                    var sceneObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                    foreach (var go in sceneObjects)
                    {
                        if (PrefabUtility.IsPartOfPrefabInstance(go)) continue;
                        
                        replacementCount += ReplaceInGameObject(go);
                    }
                }
            }

            // 处理预制体
            if (includePrefabs)
            {
                var prefabPaths = AssetDatabase.GetAllAssetPaths()
                    .Where(path => path.EndsWith(".prefab"))
                    .ToArray();

                foreach (var path in prefabPaths)
                {
                    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab == null) continue;

                    var serializedPrefab = new SerializedObject(prefab);
                    bool modified = false;

                    var components = prefab.GetComponentsInChildren<Component>(true);
                    foreach (var component in components)
                    {
                        if (component is SpriteRenderer || component is Image)
                        {
                            var serializedComponent = new SerializedObject(component);
                            var spriteProperty = component is SpriteRenderer
                                ? serializedComponent.FindProperty("m_Sprite")
                                : serializedComponent.FindProperty("m_Sprite");

                            if (spriteProperty != null && spriteProperty.objectReferenceValue == oldSprite)
                            {
                                spriteProperty.objectReferenceValue = newSprite;
                                serializedComponent.ApplyModifiedProperties();
                                modified = true;
                                replacementCount++;
                            }
                        }
                    }

                    if (modified)
                    {
                        PrefabUtility.SavePrefabAsset(prefab);
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            string message;
            if (includeScenes && includeAllScenes)
            {
                int sceneCount = AssetDatabase.FindAssets("t:Scene").Length;
                message = $"Replaced {replacementCount} references to {oldSprite.name}\n" +
                          $"Processed {sceneCount} scenes";
            }
            else
            {
                message = $"Replaced {replacementCount} references to {oldSprite.name}";
                if (includeScenes)
                    message += " in current scene";
            }
            
            if (includePrefabs)
                message += " and prefabs";
                
            EditorUtility.DisplayDialog("Sprite Replacement Complete", message, "OK");
        }

        private int ReplaceInGameObject(GameObject go)
        {
            int count = 0;
            var components = go.GetComponentsInChildren<Component>(true);

            foreach (var component in components)
            {
                if (component is SpriteRenderer spriteRenderer)
                {
                    if (spriteRenderer.sprite == oldSprite)
                    {
                        spriteRenderer.sprite = newSprite;
                        EditorUtility.SetDirty(spriteRenderer);
                        count++;
                    }
                }
                else if (component is Image image)
                {
                    if (image.sprite == oldSprite)
                    {
                        image.sprite = newSprite;
                        EditorUtility.SetDirty(image);
                        count++;
                    }
                }
            }

            return count;
        }
    }
}