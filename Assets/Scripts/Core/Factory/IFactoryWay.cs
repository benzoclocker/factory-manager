using System.Collections.Generic;
using Core.Environment;

namespace Core.Factory
{
    public interface IFactoryWay
    {
        List<Stack<IPickable>> DropZones { get; }
        bool IsReadyToCreateBox { get; }
    }

    public class ThirdFactoryWay : IFactoryWay
    {
        public List<Stack<IPickable>> DropZones { get; private set; }

        public bool IsReadyToCreateBox => DropZones[0].Count > 0 && DropZones[1].Count > 0;

        public ThirdFactoryWay(List<Stack<IPickable>> dropZones)
        {
            DropZones = dropZones;
        }
    }
}