using Godot;
using System;

public partial class HurtComponent : Area2D
{
	[Export] private int maxHealth;
	private int curHealth;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnGetHurt;
		curHealth = maxHealth;
	}
	public override void _ExitTree()
	{
		AreaEntered -= OnGetHurt;
	}

	private void OnGetHurt(Node2D body)
	{
		if (body is HitComponent hitComponent)
		{
			curHealth -= hitComponent.Damage;
			DamageEvent.OnGetHurtEvent(curHealth <= 0);
			if (curHealth <= 0)
			{
				curHealth = 0;
				Owner.QueueFree();
			}
		}
	}

}
