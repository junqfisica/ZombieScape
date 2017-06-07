using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[InitializeOnLoad]
public class BundleManager
{
	[PostProcessBuildAttribute(1)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
	{
		Debug.Log("Build v" + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")");
		IncreaseMinor();
	}

	static void IncrementVersion(int majorIncr, int minorIncr)
	{
		string[] lines = PlayerSettings.bundleVersion.Split('.');
		Debug.Log (lines[0]);
		Debug.Log (lines[1]);

		int MajorVersion = int.Parse(lines[0]) + majorIncr;
		int MinorVersion = int.Parse(lines[1]) + minorIncr;

		PlayerSettings.bundleVersion = MajorVersion.ToString ("0") + "." + MinorVersion.ToString ("00");
		PlayerSettings.Android.bundleVersionCode = MajorVersion * 1000 + MinorVersion * 100;// + Build;
	}

	[MenuItem("Build/Increase Minor Version")]
	private static void IncreaseMinor()
	{
		IncrementVersion(0, 1);
	}

	[MenuItem("Build/Increase Major Version")]
	private static void IncreaseMajor()
	{
		IncrementVersion(1, 0);
	}
		
}