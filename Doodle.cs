using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Doodle), true)]
public class DoodleEditor : Editor
{
	private void OnEnable()
	{
		EditorApplication.update += EditorUpdate;
	}

	private void OnDisable()
	{
		EditorApplication.update -= EditorUpdate;
	}

	private void EditorUpdate()
	{
		if (!Application.isPlaying)
			EditorApplication.QueuePlayerLoopUpdate();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Space();
		if (GUILayout.Button("Restart", EditorStyles.miniButton))
		{
			if (target != null && target is Doodle sketch)
			{
				sketch.Cleanup();
				sketch.Setup();
			}
		}
	}
}
#endif

[ExecuteAlways]
public abstract class Doodle : MonoBehaviour
{
	private void OnEnable()
	{
		Camera.onPostRender += PostRenderCallback;
		Setup();
	}

	private void OnDisable()
	{
		Camera.onPostRender -= PostRenderCallback;
		Cleanup();
	}

	private void PostRenderCallback(Camera c) => Render();

	private void Update() => Step();

	public virtual void Setup() {}
	public virtual void Cleanup() {}
	public virtual void Step() {}
	public virtual void Render() {}
}