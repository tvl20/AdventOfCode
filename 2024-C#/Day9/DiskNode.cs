using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    // input 123
    // node 1; id 0 size 1
    // node 2; id -1 size 2
    // node 3; id 1 size 3

    public class DiskNode
    {
        public long ID { get; private set; }
        public int Size { get; private set; }

        public DiskNode(long iD, int size)
        {
            ID = iD;
            Size = size;
        }

        public void ResizeTo(int newSize)
        {
            Size = newSize;
        }

        public List<long> ToLongs()
        {
            return Enumerable.Repeat(ID, 1).ToList();
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Size; i++)
            {
                str += ID > -1 ? ID.ToString() : ".";
            }
            return str;
        }
    }
}
