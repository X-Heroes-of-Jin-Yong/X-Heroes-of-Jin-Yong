using UnityEditor;
using UnityEngine;

namespace BigEvents.Editor
{
    public static class AnimatedButtonMenu
    {
        private const string MenuPath = "GameObject/BigEvents/UI/Animated Button";
        private const string PrefabPath = "Assets/Plugins/BigEvents/Resources/Prefabs/Button.prefab";

        [MenuItem(MenuPath, false, 10)]
        private static void CreateAnimatedButton(MenuCommand menuCommand)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);

            if (prefab == null)
            {
                EditorUtility.DisplayDialog("Error", $"找不到 Prefab：{PrefabPath}", "确定");
                return;
            }

            // 从 Prefab 实例化，保留关联
            var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            go.name = "Animated Button";
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create Animated Button");
            Selection.activeGameObject = go;
        }

        [MenuItem(MenuPath, true)]
        private static bool ValidateCreateAnimatedButton()
        {
            return true;
        }
    }
}
