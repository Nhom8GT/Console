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
            public DateTime Ngaynhanphong { get; set; }
            public DateTime Ngaytraphong { get; set; }
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

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Tạo một phiên bản của BinarySearchTree
            BinarySearchTree binaryTree = new BinarySearchTree();

            string filePath = @"C:\Users\dell\Downloads\FILE DỮ LIỆU GỐC (6).tsv";

            // Đọc từng dòng tệp TSV
            string[] lines = File.ReadAllLines(filePath);

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
                    Ngaynhanphong = DateTime.MinValue,
                    Ngaytraphong = DateTime.MinValue,
                    luongphong = int.Parse(fields[9]),
                    luongnguoi = int.Parse(fields[10]),
                    trangthaiphong = fields[11]
                };
                string ngayDatPhongString = fields[6];
                DateTime ngayDatPhong;
                //Kiểm tra xem giá trị của ngayDatPhongString có thể được chuyển đổi thành kiểu DateTime theo định dạng "dd/MM/yyyy" hay không. Nếu chuyển đổi thành công, giá trị được gán vào biến ngayDatPhong.
                if (DateTime.TryParseExact(ngayDatPhongString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayDatPhong))
                {
                    hosokhachhang.Ngaydatphong = ngayDatPhong;
                }
                else
                {
                    // Xử lý khi ngày tháng không hợp lệ
                }
                string ngaynhanphongString = fields[7];
                DateTime ngaynhanphong;
                //Kiểm tra xem giá trị của ngaynhanphongString có thể được chuyển đổi thành kiểu DateTime theo định dạng "dd/MM/yyyy" hay không. Nếu chuyển đổi thành công, giá trị được gán vào biến ngaynhanphong.
                if (DateTime.TryParseExact(ngaynhanphongString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaynhanphong))
                {
                    hosokhachhang.Ngaynhanphong = ngaynhanphong;
                }
                else
                {
                    // Xử lý khi ngày tháng không hợp lệ
                }
                // Chèn dữ liệu vào cây tìm kiếm nhị phân
                string ngaytraPhongString = fields[8];
                DateTime ngaytraPhong;
                //Kiểm tra xem giá trị của ngaytraPhongString có thể được chuyển đổi thành kiểu DateTime theo định dạng "dd/MM/yyyy" hay không. Nếu chuyển đổi thành công, giá trị được gán vào biến ngaytraPhong.
                if (DateTime.TryParseExact(ngaytraPhongString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaytraPhong))
                {
                    hosokhachhang.Ngaytraphong = ngaytraPhong;
                }
                else
                {
                    // Xử lý khi ngày tháng không hợp lệ
                }
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
            Console.WriteLine("Nhập ngày bắt đầu (dd/MM/yyyy): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Nhập ngày kết thúc (dd/MM/yyyy): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            // Gọi phương thức finddate để tìm kiếm hồ sơ trong phạm vi ngày đã chỉ định
            binaryTree.FindDate(binaryTree.Root, startDate, endDate);

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
                    writer.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
                        hoso.Madatphong, hoso.Tenkhachhang, hoso.GioiTinh, hoso.Email,
                        hoso.sodienthoai, hoso.loaiphong,
                        hoso.Ngaydatphong.ToString("dd/MM/yyyy", viVn),
                        hoso.Ngaynhanphong.ToString("dd/MM/yyyy", viVn),
                        hoso.Ngaytraphong.ToString("dd/MM/yyyy", viVn),
                        hoso.luongphong, hoso.luongnguoi, hoso.trangthaiphong);
                }
            }
        }
    }
}