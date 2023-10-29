namespace VSON.Core
{
    public enum ComponentType
    { 
        MissingComponent = -1,
        GenericComponent = 0,
        GrasshopperParam = 1,
        GrasshopperComponent = 2
    }

    public enum DiffState
    { 
        Undefined = -1,
        None = 0,
        Added = 1,
        Removed = 2,
        Modified = 3,
        Moved = 4
    }
}
