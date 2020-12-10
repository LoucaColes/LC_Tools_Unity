// Liscense - GPL-3.0-or-later

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSwitcherWindow : EditorWindow
{
    private bool saveOnClose = false;
    private bool openAdditiveLoaded = true;
    private Vector2 scrollPos;
    private List<SceneAsset> scenes = new List<SceneAsset>();
    private List<SceneAsset> scenesToRemove = new List<SceneAsset>();

    [MenuItem("Tools/LC Tools/Scene Switcher")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SceneSwitcherWindow window = (SceneSwitcherWindow)EditorWindow.GetWindow(typeof(SceneSwitcherWindow));
        window.Show();
    }

    void OnGUI()
    {
        DisplaySettings();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Scenes");

        EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width - 10), GUILayout.Height(position.height - 10));

        DisplaySceneEntries();

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Scene"))
        {
            scenes.Add(null);
        }

        if (GUILayout.Button("Clear All Scenes"))
        {
            scenes.Clear();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }

    private void DisplaySceneEntries()
    {
        scenesToRemove.Clear();

        for (int index = 0; index < scenes.Count; index++)
        {
            scenes[index] = (SceneAsset)EditorGUILayout.ObjectField("Scene: ", scenes[index], typeof(SceneAsset), true);

            if (GUILayout.Button("Open Scene"))
            {
                string scenePath = AssetDatabase.GetAssetPath(scenes[index]);
                if (!saveOnClose && EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scenePath);
                }
                else if (saveOnClose)
                {
                    EditorSceneManager.MarkAllScenesDirty();
                    EditorSceneManager.SaveOpenScenes();
                    EditorSceneManager.OpenScene(scenePath);
                }
            }

            if (GUILayout.Button("Open Additively"))
            {
                string scenePath = AssetDatabase.GetAssetPath(scenes[index]);
                OpenSceneMode mode = openAdditiveLoaded ? OpenSceneMode.Additive : OpenSceneMode.AdditiveWithoutLoading;
                EditorSceneManager.OpenScene(scenePath, mode);
            }

            if (GUILayout.Button("Remove Scene From List"))
            {
                scenesToRemove.Add(scenes[index]);
            }

            EditorGUILayout.Space();
        }

        for (int index = 0; index < scenesToRemove.Count; index++)
        {
            scenes.Remove(scenesToRemove[index]);
        }
    }

    private void DisplaySettings()
    {
        EditorGUILayout.LabelField("Settings");

        if (saveOnClose)
        {
            EditorGUILayout.HelpBox("Warning: while autosave will most likely work when switching scenes," +
                " it is suggested to save manually as well", MessageType.Warning);
        }
        saveOnClose = EditorGUILayout.Toggle("Auto Save: ", saveOnClose);

        if (!openAdditiveLoaded)
        {
            EditorGUILayout.HelpBox("Warning: If you open a scene additively, it will open unloaded", MessageType.Warning);
        }
        openAdditiveLoaded = EditorGUILayout.Toggle("Additive Scenes Loaded: ", openAdditiveLoaded);
    }
}