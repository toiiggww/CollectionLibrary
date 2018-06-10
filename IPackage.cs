using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEArts.Etc.CollectionLibrary
{
    public interface IPackage<T>
    {
        T FromBuffer(byte[] buffer);
        Enum PackageType { get; set; }
        byte[] ToBytes();
    }
}
