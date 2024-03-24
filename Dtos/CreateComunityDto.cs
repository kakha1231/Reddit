using Reddit.Models;

namespace Reddit.Dtos;

public class CreateComunityDto
{
    
    public string Name { set; get; }
    
    public string Description { set; get; }
    
    public int OwnerId { set; get; }
    
}