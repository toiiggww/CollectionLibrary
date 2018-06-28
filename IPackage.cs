using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEArts.Etc.CollectionLibrary
{
    public interface IPackage
    {
        void FromBuffer(byte[] buffer);
        int PackageType { get; }
        byte[] ToBytes();
    }
}
