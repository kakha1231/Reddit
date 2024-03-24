namespace Reddit.Models;

public class Community
{
    public int Id { set; get; }
    public virtual User Owner { set; get; }
    public int OwnerId { set; get; }
    public string Name { set; get; }
    public string Description { set; get; }

    public DateTime CreatedAt { set; get; } = DateTime.UtcNow;

    public virtual ICollection<Post>? Posts { set; get; }
    
    public virtual ICollection<User>? User_Subscribers { set; get; }
    
}