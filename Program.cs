using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                    parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai ,
                    parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                    parent.Data.luongphong, parent.Data.luongnguoi,
                    parent.Data.trangthaiphong) ;
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
                    parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai ,
                    parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                    parent.Data.luongphong, parent.Data.luongnguoi,
                    parent.Data.trangthaiphong) ;
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
                    parent.Data.GioiTinh, parent.Data.Email, parent.Data.sodienthoai ,
                    parent.Data.loaiphong, parent.Data.Ngaydatphong.ToString("d", viVn),
                    parent.Data.luongphong, parent.Data.luongnguoi,
                    parent.Data.trangthaiphong) ;
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
                string filePath = @"C:\\Users\\dell\\Downloads\\Thông-tin-khách-hàng1 (2).tsv";
                int lastBookingId = ReadLastBookingIdFromFile(filePath);

                // Tăng giá trị mã đặt phòng cuối cùng lên 1
                int newBookingId = lastBookingId + 1;

                // Gán mã đặt phòng mới cho trường tương ứng trong đối tượng Hosokhachhang
                h.Madatphong = newBookingId.ToString();

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

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Tạo một phiên bản của BinarySearchTree
            BinarySearchTree binaryTree = new BinarySearchTree();

            string filePath = @"C:\Users\dell\Downloads\Thông-tin-khách-hàng1 (2).tsv";

            // Đọc từng dòng tệp TSV
            string[] lines = File.ReadAllLines(filePath);
          
            // Giả sử dữ liệu bắt đầu từ dòng thứ hai (không bao gồm tiêu đề)
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = line.Split('\t');

                // Đọc dữ liệu từ tệp TSV
                Hosokhachhang hosokhachhang = new Hosokhachhang
                {

                    Madatphong = fields[0],
                    Tenkhachhang = fields[1],
                    GioiTinh = fields[2],
                    Email = fields[3],
                    sodienthoai = fields[4],
                    loaiphong = fields[5],
                    Ngaydatphong = DateTime.MinValue,
                    luongphong = int.Parse(fields[7]),
                    luongnguoi = int.Parse(fields[8]),
                    trangthaiphong = fields[9]
                };
                string ngayDatPhongString = fields[6];
                DateTime ngayDatPhong;

                if (DateTime.TryParseExact(ngayDatPhongString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayDatPhong))
                {
                    hosokhachhang.Ngaydatphong = ngayDatPhong;
                }
                else
                {
                    // Xử lý khi ngày tháng không hợp lệ
                }
                // Chèn dữ liệu vào cây tìm kiếm nhị phân
                binaryTree.Insert(hosokhachhang);
            }
            string answer;
            do
            {
                // Nhắc người dùng tạo hồ sơ mới
                Console.WriteLine("Bạn có muốn tạo thêm hồ sơ không? (c/k)");
                answer = Console.ReadLine();

                if (answer.ToLower() == "c")
                {
                    // Gọi phương thức TaoHoSo để tạo hồ sơ mới
                    binaryTree.TaoHoSo(ref binaryTree);
                }
            } while (answer.ToLower() == "c");


            // Lưu dữ liệu từ cây tìm kiếm vào một danh sách
            List<Hosokhachhang> hosoList = new List<Hosokhachhang>();
            SaveDataToList(binaryTree.Root, hosoList);

            // Lưu danh sách hồ sơ vào tệp tin
            
            SaveDataToFile(filePath, hosoList);

            Console.WriteLine("Dữ liệu đã được lưu vào tệp tin.");


            // Nhắc người dùng nhập tên để tìm kiếm
            Console.WriteLine("Nhập tên để tìm kiếm:");
            string searchName = Console.ReadLine();

            // Gọi phương thức findname để tìm kiếm hồ sơ theo tên
            binaryTree.findname(binaryTree.Root, searchName);

            // Nhắc người dùng nhập ngày bắt đầu và ngày kết thúc
            Console.WriteLine("Nhập ngày bắt đầu (MM/dd/yyyy): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Nhập ngày kết thúc (MM/dd/yyyy): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            // Gọi phương thức finddate để tìm kiếm hồ sơ trong phạm vi ngày đã chỉ định
            binaryTree.finddate(binaryTree.Root, startDate, endDate);

            // Đợi người dùng nhập trước khi đóng cửa sổ bảng điều khiển
            Console.ReadLine();
        }
        // Phương thức để lưu dữ liệu từ cây tìm kiếm vào danh sách
        public static void SaveDataToList(Node parent, List<Hosokhachhang> hosoList)
        {
            if (parent != null)
            {
                SaveDataToList(parent.LeftNode, hosoList);
                hosoList.Add(parent.Data);
                SaveDataToList(parent.RightNode, hosoList);
            }
        }

        // Phương thức để lưu danh sách hồ sơ vào tệp tin
        public static void SaveDataToFile(string filePath, List<Hosokhachhang> hosoList)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                CultureInfo viVn = new CultureInfo("vi-VN");

                foreach (Hosokhachhang hoso in hosoList)
                {
                    writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                        hoso.Madatphong, hoso.Tenkhachhang, hoso.GioiTinh, hoso.Email,
                        hoso.sodienthoai, hoso.loaiphong,
                        hoso.Ngaydatphong.ToString("MM/dd/yyyy", viVn),
                        hoso.luongphong, hoso.luongnguoi, hoso.trangthaiphong);
                }
            }
        }
    }
}
