using System.Collections.Generic;
using Core.Environment;

namespace Core.Factory
{
    public class ThirdFactoryWay : IFactoryWay
    {
        public List<Stack<IPickable>> DropZones { get; private set; }
        public PickableBoxType BoxType { get; private set; }
        public FactoryBox Box { get; private set; }
        public int MaxFactoryCapacity { get; private set; }

        public bool IsReadyToCreateBox => DropZones[0].Count > 0 && DropZones[1].Count > 0;

        public ThirdFactoryWay(List<Stack<IPickable>> dropZones, FactoryBox box, 
            PickableBoxType boxType, int maxFactoryCapacity)
        {
            DropZones = dropZones;
            Box = box;
            BoxType = boxType;
            MaxFactoryCapacity = maxFactoryCapacity;
        }
    }
}