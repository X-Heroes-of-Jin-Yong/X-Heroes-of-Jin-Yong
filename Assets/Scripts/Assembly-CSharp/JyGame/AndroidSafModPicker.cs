using UnityEngine;

namespace JyGame
{
	public static class AndroidSafModPicker
	{
		public static void Open(string callbackObject, string callbackMethod)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
			using (AndroidJavaClass picker = new AndroidJavaClass("com.xheroes.modimport.ModFilePicker"))
			{
				picker.CallStatic("open", activity, callbackObject, callbackMethod);
			}
#else
			Debug.LogWarning("Android SAF is only available on an Android device.");
#endif
		}
	}
}
