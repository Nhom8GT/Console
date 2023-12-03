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
                Console.WriteLine("{0,5}{1,10}{2,10}{3,25}{4,25}{5,25}{6,5}{7,5}{8,20}{9,15}",
                parent.Data.Madatphong, parent.Data.Tenkhachhang,
                parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai,
                parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
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
                    Console.WriteLine("{0,5}{1,10}{2,10}{3,25}{4,25}{5,25}{6,5}{7,5}{8,20}{9,15}",
                    parent.Data.Madatphong, parent.Data.Tenkhachhang,
                    parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai,
                    parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                    parent.Data.luongphong, parent.Data.luongnguoi,
                    parent.Data.trangthaiphong);
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
                if (parent.Data.Ngaydatphong >= date1 && parent.Data.Ngaydatphong <= date2)
                    Console.WriteLine("{0,5}{1,10}{2,10}{3,25}{4,25}{5,25}{6,5}{7,5}{8,20}{9,15}",
                    parent.Data.Madatphong, parent.Data.Tenkhachhang,
                    parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai,
                    parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                    parent.Data.luongphong, parent.Data.luongnguoi,
                    parent.Data.trangthaiphong);
                finddate(parent.RightNode, date1, date2);
            }
        }
        // Phương thức TaoHoSo để tạo hồ sơ khách hàng và chèn vào cây nhị phân
        public void TaoHoSo(ref BinarySearchTree binaryTree)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Hosokhachhang h = new Hosokhachhang();

            // Đọc dữ liệu từ file để lấy mã đặt phòng cuối cùng
            string filePath = @"C:\\Users\\dell\\Downloads\\Thông-tin-khách-hàng1 (5).tsv";
            int lastBookingId = ReadLastBookingIdFromFile(filePath);

            // Tăng giá trị mã đặt phòng cuối cùng lên 1
            int newBookingId = lastBookingId + 1;

            // Gán mã đặt phòng mới cho trường tương ứng trong đối tượng Hosokhachhang
            h.Madatphong = newBookingId.ToString();

            // Các bước khác để nhập thông tin khách hàng
            // ...

            Console.WriteLine("=================================================");
            binaryTree.Insert(h);

            // Ghi mã đặt phòng mới vào file
            WriteLastBookingIdToFile(filePath, newBookingId);



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

            Console.WriteLine("Nhập số điện thoại khách hàng ");
            h.sodienthoai = (Console.ReadLine());

            do
            {
                Console.WriteLine("Loại phòng đã được đặt:(Standard/Deluxe):  ");
                h.loaiphong = Console.ReadLine().ToLower();
            }
            while (h.loaiphong != "standard" && h.loaiphong != "deluxe");

            h.Ngaydatphong = DateTime.Now;

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
        private int ReadLastBookingIdFromFile(string filePath)
        {
            int lastBookingId = 0;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length >= 2)
                        {
                            string bookingIdString = parts[1];
                            if (int.TryParse(bookingIdString, out int bookingId))
                            {
                                lastBookingId = bookingId;
                            }
                        }
                    }
                }
            }

            return lastBookingId;
        }

        private void WriteLastBookingIdToFile(string filePath, int bookingId)
        {
            File.WriteAllText(filePath, bookingId.ToString());
        }
    }
}
