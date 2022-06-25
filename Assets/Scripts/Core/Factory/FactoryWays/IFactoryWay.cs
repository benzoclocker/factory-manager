using System.Collections.Generic;
using Core.Environment;

namespace Core.Factory
{
    public interface IFactoryWay
    {
        List<Stack<IPickable>> DropZones { get; }
        PickableBoxType BoxType { get; }
        FactoryBox Box { get; }
        float CreationDelay { get; }
        int MaxFactoryCapacity { get; }
        bool IsReadyToCreateBox { get; }
    }
}