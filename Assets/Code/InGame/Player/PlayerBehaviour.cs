
public abstract class PlayerBehaviour
{
    public PlayerBase PlayerBase { get; set; }

    public PlayerBehaviour(PlayerBase PlayerBase)
	{
        this.PlayerBase = PlayerBase;
    }

    public virtual void Update() { }    
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
}
