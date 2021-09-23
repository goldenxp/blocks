using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Mover
{

	public static Player instance;
	public static Player Get() { return instance; }
	Vector3 direction = Vector3.zero;
	
	public InputActionReference actionMove;

	void Awake()
	{
		instance = this;
	}
	
	void OnEnable()
	{
		actionMove.action.Enable();
	}
	
	void OnDisable()
	{
		actionMove.action.Disable();
	}

	void Update ()
	{	
		if (CanInput())
			CheckInput();
	}

	public bool CanInput()
	{
		return !Game.isMoving && !Game.Get().holdingUndo;
	}

	public void CheckInput()
	{
		Vector2 axes = actionMove.action.ReadValue<Vector2>();
		float hor = axes.x; 
		float ver = axes.y;

		if (hor == 0 && ver == 0)
			return;

		if (hor != 0 && ver != 0)
		{
			if (direction == Game.Get().MoveLeft || direction == Game.Get().MoveRight)
				hor = 0;
			else
				ver = 0;
		}
		
		if (hor == 1)
			direction = Game.Get().MoveRight;
		else if (hor == -1)
			direction = Game.Get().MoveLeft;
		else if (ver == -1)
			direction = Game.Get().MoveDown;
		else if (ver == 1)
			direction = Game.Get().MoveUp;

		if (CanMove(direction))
		{
			MoveIt(direction);
			Game.Get().MoveStart(direction);
		} else
			Game.moversToMove.Clear();
	}
}
