using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Do_an_CTDL
{
    // Định nghĩa lớp Hosokhachhang để lưu trữ thông tin khách hàng
    public class Hosokhachhang
    {
        public string Madatphong { get; set; }
        public string Tenkhachhang { get; set; }
        public string GioiTinh { get; set; }
        public string sodienthoai { get; set; }
        public DateTime Ngaydatphong { get; set; }
        public string loaiphong { get; set; }
        public string trangthaiphong { get; set; }
    }
}
