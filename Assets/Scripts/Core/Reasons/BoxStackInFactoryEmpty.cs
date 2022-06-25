namespace Core.Reasons
{
    public class BoxStackInFactoryEmpty : IReason
    {
        public string AlertReason => "No boxes to build new.";
    }
}