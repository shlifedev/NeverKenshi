namespace Kenshi
{
    public interface IBuildingConstructController
    {        
        BuildingConstructManager.BuildingState State { get; set; }
        BuildingConstructManager Instance { get; } 
        
    }
}