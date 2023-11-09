namespace VSON.Core
{
    public enum DiffState
    {
        Unknown = -1,

        NoChange = 0,

        AttributesChanged = 1,
        AddedToDocument = 2,
        RemovedFromDocument = 3,
        CanvasPositionChanged = 4,

        InstanceChanged = 16,
    }

    public interface IDiff
    {
        DiffState DiffState { get; set; }
    }
}
