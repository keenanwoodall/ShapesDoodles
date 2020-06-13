using UnityEngine;
using Shapes;

public class _1 : Doodle
{
	private class SphereLine
	{
		public float x;
		public float y;

		private PolylinePath path;
		
		public SphereLine(float x, float y, int resolution)
		{
			path = new PolylinePath();
			for (int i = 0; i < resolution; i++)
				path.AddPoint(0, 0);

			this.x = x;
			this.y = y;
		}

		public void Step(float speed, AnimationCurve width, Gradient gradient, float radius, float angleLength, float frequency, float magnitude)
		{
			float angleStep = angleLength / path.Count;
			
			y -= speed * Time.deltaTime * 360f;
			for (int i = 0; i < path.Count; i++)
			{
				var point = path[i];
				
				float y = this.y - angleStep * i;
				float t = 1f - i / (float)(path.Count - 1);

				Vector3 p = Quaternion.Euler(x, y, 0f) * Vector3.forward * radius;
				Vector3 n = Quaternion.Euler(x, y, 0f) * Vector3.up;
				p += n * (Mathf.Pow(t, 3f) * Mathf.Sin(y * frequency) * magnitude * Mathf.Abs(Mathf.Cos(x * Mathf.Deg2Rad)));
				
				point.thickness = width.Evaluate(t);
				point.color = gradient.Evaluate(t);
				point.point = p;
				
				path[i] = point;
			}
		}

		public void Render()
		{
			Draw.Polyline(path, PolylineJoins.Simple);
		}
	}
	
	public int lineCount = 30;
	public int loops = 1;
	public int resolution = 30;
	public float angleLength = 45f;
	public float frequency = 10f;
	public float magnitude = 0.3f;
	public float speed = 20f;
	public float radius = 1f;
	public float thickness = 10;
	public AnimationCurve thicknessCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
	public Gradient gradient;

	private SphereLine[] lines;

	public override void Setup()
	{
		lines = new SphereLine[lineCount];
		for (int i = 0; i < lines.Length; i++)
		{
			float t = i / (float)(lines.Length);
			lines[i] = new SphereLine(Mathf.LerpAngle(-80f, 80f, (t * (float)loops) % 1f), t * 360f - angleLength, resolution);
		}
	}

	public override void Step()
	{
		for (int i = 0; i < lines.Length; i++)
			lines[i].Step(speed, thicknessCurve, gradient, radius, angleLength, frequency / 180, magnitude);
	}

	public override void Render()
	{
		Draw.BlendMode = ShapesBlendMode.Additive;
		Draw.LineThicknessSpace = ThicknessSpace.Meters;
		Draw.LineThickness = thickness;
		for (int i = 0; i < lines.Length; i++)
			lines[i].Render();
	}
}