using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap_Sort
{
    public class Array
    {
        public int[] array;
        public int length,Node1,Node2;
        public Label[] labels;
        public Array(int[] array, int length,int Node1, int Node2)
        {
            this.array = array;
            this.length = length;
            labels= new Label[length];
            this.Node1 = Node1;
            this.Node2 = Node2;
        }
    }
}
