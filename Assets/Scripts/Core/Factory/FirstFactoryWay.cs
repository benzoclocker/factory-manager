using System.Collections.Generic;
using Core.Environment;

namespace Core.Factory
{
    public class FirstFactoryWay : IFactoryWay
    {
        public List<Stack<IPickable>> DropZones { get; private set; }
        public PickableBoxType BoxType { get; private set; }
        public FactoryBox Box { get; }
        public int MaxFactoryCapacity { get; }
        public bool IsReadyToCreateBox { get; } = true;

        public FirstFactoryWay(FactoryBox box, PickableBoxType boxType, int maxFactoryCapacity)
        {
            Box = box;
            BoxType = boxType;
            MaxFactoryCapacity = maxFactoryCapacity;
        }
    }
}