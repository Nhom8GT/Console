using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Do_an_CTDL
{
    // Định nghĩa lớp BinarySearchTree để thực hiện các thao tác trên cây nhị phân
    public class BinarySearchTree
    {
        public Node Root { get; set; }
        // Phương thức Insert để chèn một giá trị vào cây nhị phân
        public bool Insert(Hosokhachhang value)
        {
            Node before = null, after = this.Root;
            while (after != null)
            {
                before = after;
                // So sánh giá trị ngày đặt phòng để xác định vị trí chèn
                if (value.Ngaydatphong <= after.Data.Ngaydatphong)
                    after = after.LeftNode;
                else if (value.Ngaydatphong > after.Data.Ngaydatphong)
                    after = after.RightNode;
                else
                    return false;
            }
            Node newNode = new Node();
            newNode.Data = value;
            if (this.Root == null)
                this.Root = newNode;
            else
            {
                if (value.Ngaydatphong <= before.Data.Ngaydatphong)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }
            return true;
        }
        // Phương thức TraverseInOrder để duyệt cây theo thứ tự trung tố và in ra thông tin
        public void TraverseInOrder(Node parent)
        {
            CultureInfo viVn = new CultureInfo("vi-VN");
            if (parent != null)
            {
                TraverseInOrder(parent.LeftNode);
                Console.WriteLine("{0,5}{1,20}{2,10}{3,25}{4,25}{5,25}{6,20}{7,20}{8,20}{9,15}{10,20}{11,20}",
                parent.Data.Madatphong, parent.Data.Tenkhachhang,
                parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai,
                parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                parent.Data.Ngaynhanphong.ToString("d", viVn),
                parent.Data.Ngaytraphong.ToString("d", viVn),
                parent.Data.luongphong, parent.Data.luongnguoi,
                parent.Data.trangthaiphong);
                TraverseInOrder(parent.RightNode);
            }
        }
        // Phương thức findname để tìm kiếm khách hàng theo tên
        public void findname(Node parent, string Tenkhachhang)
        {
            CultureInfo viVn = new CultureInfo("vi-VN");
            if (parent != null)
            {
                findname(parent.LeftNode, Tenkhachhang);
                if (parent.Data.Tenkhachhang.ToLower().Contains(Tenkhachhang.ToLower()))
                    Console.WriteLine("{0,5}{1,20}{2,10}{3,25}{4,25}{5,25}{6,20}{7,20}{8,20}{9,15}{10,20}{11,20}",
                parent.Data.Madatphong, parent.Data.Tenkhachhang,
                parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai,
                parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                parent.Data.Ngaynhanphong.ToString("d", viVn),
                parent.Data.Ngaytraphong.ToString("d", viVn),
                parent.Data.luongphong, parent.Data.luongnguoi,
                parent.Data.trangthaiphong);
                findname(parent.RightNode, Tenkhachhang);
            }
        }
        // Phương thức finddate để tìm kiếm khách hàng theo khoảng thời gian nhận phòng
        public void FindDate(Node parent, DateTime date1, DateTime date2)
        {
            CultureInfo viVn = new CultureInfo("vi-VN");
            if (parent != null)
            {
                FindDate(parent.LeftNode, date1, date2);
                if (parent.Data.Ngaynhanphong >= date1 && parent.Data.Ngaynhanphong <= date2)
                    Console.WriteLine("{0,5}{1,20}{2,10}{3,25}{4,25}{5,25}{6,20}{7,20}{8,20}{9,15}{10,20}{11,20}",
                        parent.Data.Madatphong, parent.Data.Tenkhachhang,
                        parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai,
                        parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                        parent.Data.Ngaynhanphong.ToString("d", viVn),
                        parent.Data.Ngaytraphong.ToString("d", viVn),
                        parent.Data.luongphong, parent.Data.luongnguoi,
                        parent.Data.trangthaiphong);
                FindDate(parent.RightNode, date1, date2);
            }
        }
        // Phương thức TaoHoSo để tạo hồ sơ khách hàng và chèn vào cây nhị phân
        public void TaoHoSo(ref BinarySearchTree binaryTree)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Hosokhachhang h = new Hosokhachhang();

            Console.WriteLine("================================================");


            // ghi thông tin còn lại của khách hàng
            Console.WriteLine("Nhập tên khách hàng: ");
            h.Tenkhachhang = Console.ReadLine().ToLower();

            Console.WriteLine("Nhập mã đặt phòng: ");
            h.Madatphong = Console.ReadLine();

            do
            {
                Console.WriteLine("Nhập giới tính khách hàng(male/female): ");
                h.GioiTinh = Console.ReadLine().ToLower();
            }
            while (h.GioiTinh != "male" && h.GioiTinh != "female");

            Console.WriteLine("Nhập email khách hàng");
            h.Email = Console.ReadLine();

            Console.WriteLine("Nhập số điện thoại khách hàng ");
            h.sodienthoai = (Console.ReadLine());

            do
            {
                Console.WriteLine("Loại phòng đã được đặt:(Standard/Deluxe):  ");
                h.loaiphong = Console.ReadLine().ToLower();
            }
            while (h.loaiphong != "standard" && h.loaiphong != "deluxe");

            h.Ngaydatphong = DateTime.Now;

            Console.WriteLine("Nhập ngày nhận phòng");
            h.Ngaynhanphong = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Nhập ngày trả phòng");
            h.Ngaytraphong = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Nhập số lượng phòng đã đặt: ");
            h.luongphong = int.Parse(Console.ReadLine());

            Console.WriteLine("Nhập số lượng người sẽ cư trú");
            h.luongnguoi = int.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("Nhập trạng thái hiện tại của phòng(da thanh toan/chưa thanh toan): ");
                h.trangthaiphong = Console.ReadLine().ToLower();
            }
            while (h.trangthaiphong != "da thanh toan" && h.trangthaiphong != "chua thanh toan");

            Console.WriteLine("=================================================");
            binaryTree.Insert(h);
        }
    }
}
