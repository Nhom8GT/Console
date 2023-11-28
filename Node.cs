using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Do_an_CTDL
{
    // Định nghĩa lớp Node để tạo nút trong cây nhị phân
    public class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Hosokhachhang Data { get; set; }
    }
}
