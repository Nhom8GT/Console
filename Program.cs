using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Do_an_CTDL
{
    internal class Program
    {
        // Định nghĩa lớp Hosokhachhang để lưu trữ thông tin khách hàng
        public class Hosokhachhang
        {
            public string Madatphong { get; set; }
            public string Tenkhachhang { get; set; }
            public string Email { get; set; }
            public string GioiTinh { get; set; }
            public string sodienthoai { get; set; }
            public DateTime Ngaydatphong { get; set; }
            public string loaiphong { get; set; }
            public int luongphong { get; set; }
            public int luongnguoi { get; set; }
            public string trangthaiphong { get; set; }
        }
        // Định nghĩa lớp Node để tạo nút trong cây nhị phân
        public class Node
        {
            public Node LeftNode { get; set; }
            public Node RightNode { get; set; }
            public Hosokhachhang Data { get; set; }
        }
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
                    Console.WriteLine("{0,5}{1,10}{2,10}{3,25}{4,25}{5,25}{6,5}{7,5}{8,20}{9,15}",
                    parent.Data.Madatphong, parent.Data.Tenkhachhang,
                    parent.Data.GioiTinh, parent.Data.Email,
                    parent.Data.Ngaydatphong.ToString("d", viVn),
                    parent.Data.loaiphong, parent.Data.luongphong, parent.Data.luongnguoi,
                    parent.Data.trangthaiphong, parent.Data.sodienthoai);
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
                    if (parent.Data.Tenkhachhang == Tenkhachhang)
                        Console.WriteLine("{0,5}{1,10}{2,10}{3,25}{4,25}{5,25}{6,5}{7,5}{8,20}{9,15}",
                        parent.Data.Madatphong, parent.Data.Tenkhachhang,
                        parent.Data.GioiTinh,parent.Data.Email, 
                        parent.Data.Ngaydatphong.ToString("d", viVn),
                        parent.Data.loaiphong, parent.Data.luongphong, parent.Data.luongnguoi,
                        parent.Data.trangthaiphong, parent.Data.sodienthoai);
                    findname(parent.RightNode, Tenkhachhang);
                }
            }
            // Phương thức finddate để tìm kiếm khách hàng theo khoảng thời gian đặt phòng
            public void finddate(Node parent, DateTime date1, DateTime date2)
            {
                CultureInfo viVn = new CultureInfo("vi-VN");
                if (parent != null)
                {
                    finddate(parent.LeftNode, date1, date2);
                    if (parent.Data.Ngaydatphong <= date2 && parent.Data.Ngaydatphong >=
                    date1)
                        Console.WriteLine("{0,5}{1,10}{2,10}{3,25}{4,25}{5,25}{6,5}{7,5}{8,20}{9,15}",
                        parent.Data.Madatphong, parent.Data.Tenkhachhang,
                        parent.Data.GioiTinh, parent.Data.Email,
                        parent.Data.Ngaydatphong.ToString("d", viVn),
                        parent.Data.loaiphong, parent.Data.luongphong, parent.Data.luongnguoi,
                        parent.Data.trangthaiphong, parent.Data.sodienthoai);
                    finddate(parent.RightNode, date1, date2);
                }
            }
            // Phương thức TaoHoSo để tạo hồ sơ khách hàng và chèn vào cây nhị phân
            public void TaoHoSo(ref BinarySearchTree binaryTree)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
                Hosokhachhang h = new Hosokhachhang();

                Console.WriteLine("Nhập mã đặt phòng (ID_Booking): ");
                h.Madatphong = Console.ReadLine();

                Console.WriteLine("Nhập tên khách hàng: ");
                h.Tenkhachhang = Console.ReadLine().ToLower();

                do
                {
                    Console.WriteLine("Nhập giới tính khách hàng(male/female): ");
                    h.GioiTinh = Console.ReadLine().ToLower();
                }
                while (h.GioiTinh != "male" && h.GioiTinh != "female");

                Console.WriteLine("Nhập email khách hàng");
                h.Email = Console.ReadLine();

                bool isValidDate = false;
                DateTime ngayDatPhong;

                do
                {
                    Console.WriteLine("Nhập ngày đặt phòng (MM/dd/yyyy): ");
                    string dateString = Console.ReadLine();

                    isValidDate = DateTime.TryParseExact(dateString, "MM/dd/yyyy", null, DateTimeStyles.None, out ngayDatPhong);

                    if (!isValidDate)
                    {
                        Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng nhập lại.");
                    }
                } while (!isValidDate);
                h.Ngaydatphong = ngayDatPhong;

                do
                {
                    Console.WriteLine("Loại phòng đã được đặt:(Standard/Deluxe):  ");
                    h.loaiphong = Console.ReadLine().ToLower();
                }
                while (h.loaiphong != "standard" && h.loaiphong != "deluxe" );

                Console.WriteLine("Nhập số lượng phòng đã đặt: ");
                h.luongphong = int.Parse(Console.ReadLine());

                Console.WriteLine("Nhập số lượng người sẽ cư trú");
                h.luongnguoi = int.Parse(Console.ReadLine());

                do
                {
                    Console.WriteLine("Nhập trạng thái hiện tại của phòng(thanh toan/chưa thanh toan): ");
                    h.trangthaiphong = Console.ReadLine().ToLower();
                }
                while (h.trangthaiphong != "thanh toan" && h.trangthaiphong != "chua thanh toan");

                Console.WriteLine("Nhập số điện thoại khách hàng ");
                h.sodienthoai = (Console.ReadLine());
                Console.WriteLine("=================================================");
                binaryTree.Insert(h);
            }
        }
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.Clear();
            BinarySearchTree binaryTree = new BinarySearchTree();
            int n =0;
            do
            {
                Console.WriteLine("Nhập số lượng hồ sơ cần thêm: ");
                string input = Console.ReadLine();
                Console.WriteLine("===========================================");
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Vui lòng nhập một giá trị.");
                    continue;
                }

                if (!int.TryParse(input, out n))
                {
                    Console.WriteLine("Giá trị không hợp lệ. Vui lòng nhập một số nguyên.");
                }
            } while (n <= 0);

            // Sử dụng giá trị n (số lượng hồ sơ) ở đây

            for (int i = 1; i <= n; i++)
            {
                binaryTree.TaoHoSo(ref binaryTree);
            }
            Console.WriteLine("danh sách các hồ sơ:");
            binaryTree.TraverseInOrder(binaryTree.Root);
            Console.WriteLine("=================================================");
            Console.WriteLine("nhập tên để tìm kiếm: ");
            string name = Console.ReadLine().ToLower();
            binaryTree.findname(binaryTree.Root, name);
            Console.WriteLine("nhập khoảng thời gian đặt phòng để tìm kiếm");
            Console.Write("từ: ");
            DateTime date1 = DateTime.Parse(Console.ReadLine());
            Console.Write("đến: ");
            DateTime date2 = DateTime.Parse(Console.ReadLine());
            binaryTree.finddate(binaryTree.Root, date1, date2);
            Console.ReadLine();
        }
    }
}

