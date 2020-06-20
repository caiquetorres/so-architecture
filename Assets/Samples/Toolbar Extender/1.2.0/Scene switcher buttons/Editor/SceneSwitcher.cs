using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
	static class ToolbarStyles
	{
		public static readonly GUIStyle commandButtonStyleLeft;
		public static readonly GUIStyle commandButtonStyleMiddle;
		public static readonly GUIStyle commandButtonStyleRight;

		static ToolbarStyles()
		{
			commandButtonStyleLeft = new GUIStyle("MiniButtonLeft")
			{
				fontSize = 15,
				fixedHeight = 21,
				fixedWidth = 30,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
			};
			
			commandButtonStyleMiddle = new GUIStyle("MiniButtonMid")
			{
				fontSize = 15,
				fixedHeight = 21,
				fixedWidth = 30,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
			};
			
			commandButtonStyleRight = new GUIStyle("MiniButtonRight")
			{
				fontSize = 15,
				fixedHeight = 21,
				fixedWidth = 30,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
			};
		}
	}

	[InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		static SceneSwitchLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("1", "Start Scene 1"), ToolbarStyles.commandButtonStyleLeft))
			{
				SceneHelper.StartScene("ToolbarExtenderExampleScene1");
			}

			if(GUILayout.Button(new GUIContent("2", "Start Scene 2"), ToolbarStyles.commandButtonStyleMiddle))
			{
				SceneHelper.StartScene("ToolbarExtenderExampleScene2");
			}
			
			if(GUILayout.Button(new GUIContent("3", "Start Scene 2"), ToolbarStyles.commandButtonStyleRight))
			{
				SceneHelper.StartScene("ToolbarExtenderExampleScene2");
			}
		}
	}

	static class SceneHelper
	{
		static string sceneToOpen;

		public static void StartScene(string sceneName)
		{
			if(EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			sceneToOpen = sceneName;
			EditorApplication.update += OnUpdate;
		}

		static void OnUpdate()
		{
			if (sceneToOpen == null ||
			    EditorApplication.isPlaying || EditorApplication.isPaused ||
			    EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			EditorApplication.update -= OnUpdate;

			if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				// need to get scene via search because the path to the scene
				// file contains the package version so it'll change over time
				string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
				if (guids.Length == 0)
				{
					Debug.LogWarning("Couldn't find scene file");
				}
				else
				{
					string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
					EditorSceneManager.OpenScene(scenePath);
					EditorApplication.isPlaying = true;
				}
			}
			sceneToOpen = null;
		}
	}
}