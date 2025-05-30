using Godot;

public partial class SmallTree : Sprite2D
{
	private bool isShake = false;
	public override void _Ready()
	{
		// SetInstanceShaderParameter("shake_intensity", 0.0);
		(Material as ShaderMaterial).SetShaderParameter("shake_intensity", 0.0);
		DamageEvent.getHurtEvent += OnGetHurtEvent;
	}

	public override void _ExitTree()
	{
		DamageEvent.getHurtEvent -= OnGetHurtEvent;
	}

	private void OnGetHurtEvent(bool isDead)
	{
		if (isDead)
		{

		}
		else
		{
			// 触发震动，并开始衰减
			(Material as ShaderMaterial).SetShaderParameter("shake_intensity", 1.0);
			isShake = true;
		}
	}

	public override void _Process(double delta)
	{
		// 震动强度逐渐衰减
		if (isShake)
		{
			float currentIntensity = (float)(Material as ShaderMaterial).GetShaderParameter("shake_intensity");
			float newIntensity = Mathf.Lerp(currentIntensity, 0.0f, (float)delta * 5.0f);
			(Material as ShaderMaterial).SetShaderParameter("shake_intensity", newIntensity);
			// GD.Print(newIntensity);
			if (newIntensity <= 0.1f)
			{
				isShake = false;
				(Material as ShaderMaterial).SetShaderParameter("shake_intensity", 0.0);
			}
		}
	}
}