using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

class MyEditorScript {
	static string[] SCENES = FindEnabledEditorScenes();
	
	static string APP_NAME = "Unity5Test";
	static string TARGET_DIR = "/Users/ben/Dropbox/Jenkins/" + APP_NAME + "/"+ GetProjectName();
	
	[MenuItem ("Custom/CI/Build Mac OS X")]
	static void PerformMacOSXBuild ()
	{
		string target_dir = APP_NAME + ".app";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneOSXIntel64,BuildOptions.None);
	}

	[MenuItem ("Custom/CI/Build Web Player")]
	static void PerformWebPlayerBuild ()
	{
		string target_dir = APP_NAME + ".unity3d";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.WebPlayerStreamed,BuildOptions.None);
	}

	[MenuItem ("Custom/CI/Build Win64")]
	static void PerformWindows64Build ()
	{
		string target_dir = APP_NAME + ".exe";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows64,BuildOptions.None);
	}

	[MenuItem ("Custom/CI/Build Win32")]
	static void PerformWindows32Build ()
	{
		string target_dir = APP_NAME + ".exe";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows,BuildOptions.None);
	}

	[MenuItem ("Custom/CI/Build Android")]
	static void PerformAndroidBuild ()
	{
		string target_dir = APP_NAME + ".apk";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.Android,BuildOptions.None);
	}

	private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	
	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}
	
	public static string GetProjectName()
	{
		string[] s = Application.dataPath.Split('/');
		string projectName = s[s.Length - 2];
		Debug.Log("project = " + projectName);
		return projectName;
	}
}