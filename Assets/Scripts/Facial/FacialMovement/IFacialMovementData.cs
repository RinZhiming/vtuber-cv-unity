namespace Facial.FacialMovement
{
    public interface IFacialMovementData
    {
        float BrowParam { get; set; }
        float EyeParam { get; set; }
        float MouthOpenParam { get; set; }
        float MouthSizeParam { get; set; }
        float BrowLeapT{ get; set; }
        float EyeLeapT { get; set; }
        float MouthLeapT { get; set; }
    }
}