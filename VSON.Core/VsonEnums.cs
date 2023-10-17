namespace VSON.Core
{
    public enum VsonComponentType
    { 
        MissingComponent = -1,
        GenericComponent = 0,
        GrasshopperParam = 1,
        GrasshopperComponent = 2
    }

    public enum VsonDiffState
    { 
        Undefined = -1,
        None = 0,
        Added = 1,
        Removed = 2,
        Modified = 3,
        Moved = 4
    }
}
