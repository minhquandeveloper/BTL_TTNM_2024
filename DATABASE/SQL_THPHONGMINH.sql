-- Tạo cơ sở dữ liệu TTNM
CREATE DATABASE TTNM;
USE TTNM;

-- Tạo bảng Chucvu
CREATE TABLE Chucvu (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Tenchucvu NVARCHAR(25)  -- Tên chức vụ
);


-- Tạo bảng Taikhoan
CREATE TABLE Taikhoan (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Tentaikhoan VARCHAR(50),  -- Tên tài khoản
    Matkhau VARCHAR(50),  -- Mật khẩu
    Chucvu_id INT,  -- Khóa ngoại tham chiếu đến id trong bảng Chucvu
    FOREIGN KEY (Chucvu_id) REFERENCES Chucvu(id)
);
--Thủ tục xử lý tài khoản học sinh là Mã học sinh vừa là Tentaikhoan vừa là Matkhau
INSERT INTO Taikhoan (Tentaikhoan, Matkhau, Chucvu_id) 
SELECT Mahocsinh, Mahocsinh, 4 FROM Hocsinh;
-- Tạo bảng Lop
CREATE TABLE Lop (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Tenlop NVARCHAR(10),  -- Tên lớp
    Siso INT  -- Sĩ số của lớp
);

-- Tạo bảng Hocsinh
CREATE TABLE Hocsinh (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Mahocsinh VARCHAR(10),  -- Mã học sinh
    Hovaten NVARCHAR(255),  -- Họ và tên
    Gioitinh NVARCHAR(10),  -- Giới tính
    Ngaysinh DATE,  -- Ngày sinh
    MaLop INT,  -- Khóa ngoại tham chiếu đến id trong bảng Lop
    SDT_Phuhuynh VARCHAR(15),  -- Số điện thoại phụ huynh
    Hocky NVARCHAR(10),  -- Học kỳ
    Namhoc NVARCHAR(10),  -- Năm học
    FOREIGN KEY (MaLop) REFERENCES Lop(id)
);

-- Tạo bảng BGH
CREATE TABLE BGH (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Mataikhoan VARCHAR(10),  -- Mã tài khoản
    Hoten NVARCHAR(50),  -- Họ tên
    Sdt VARCHAR(15)  -- Số điện thoại
);

-- Tạo bảng Monhoc
CREATE TABLE Monhoc (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Tenmon NVARCHAR(10)  -- Tên môn học
);

-- Tạo bảng GVCN
CREATE TABLE GVCN (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Hovaten NVARCHAR(100),  -- Họ và tên
    Sdt VARCHAR(15),  -- Số điện thoại
    Ngaysinh DATE,  -- Ngày sinh
    Gioitinh NVARCHAR(10),  -- Giới tính
    Diachi NVARCHAR(255),  -- Địa chỉ
    Chucvu_id INT,  -- Khóa ngoại tham chiếu đến id trong bảng Chucvu
    Lopchunhiem INT,  -- Khóa ngoại tham chiếu đến id trong bảng Lop
    Monday INT,  -- Khóa ngoại tham chiếu đến id trong bảng Monhoc
    FOREIGN KEY (Chucvu_id) REFERENCES Chucvu(id),
    FOREIGN KEY (Lopchunhiem) REFERENCES Lop(id),
    FOREIGN KEY (Monday) REFERENCES Monhoc(id)
);

-- Tạo bảng GVBM
CREATE TABLE GVBM (
    id INT IDENTITY(1,1) PRIMARY KEY,  -- Khóa chính, tự tăng
    Hovaten NVARCHAR(100),  -- Họ và tên
    Sdt VARCHAR(15),  -- Số điện thoại
    Ngaysinh DATE,  -- Ngày sinh
    Gioitinh NVARCHAR(10),  -- Giới tính
    Chucvu_id INT,  -- Khóa ngoại tham chiếu đến id trong bảng Chucvu
    Diachi NVARCHAR(255),  -- Địa chỉ
    Monday INT,  -- Khóa ngoại tham chiếu đến id trong bảng Monhoc
    FOREIGN KEY (Chucvu_id) REFERENCES Chucvu(id),
    FOREIGN KEY (Monday) REFERENCES Monhoc(id)
);

-- Tạo bảng Hocsinh_Monhoc
CREATE TABLE Hocsinh_Monhoc (
    hocsinh_id INT,  -- Khóa ngoại tham chiếu đến id trong bảng Hocsinh
    monhoc_id INT,  -- Khóa ngoại tham chiếu đến id trong bảng Monhoc
    Diemso INT,  -- Điểm số
    Hocky NVARCHAR(255),  -- Học kỳ
    Namhoc VARCHAR(255),  -- Năm học
    PRIMARY KEY (hocsinh_id, monhoc_id),  -- Khóa chính bao gồm cả hocsinh_id và monhoc_id
    FOREIGN KEY (hocsinh_id) REFERENCES Hocsinh(id),
    FOREIGN KEY (monhoc_id) REFERENCES Monhoc(id)
);

-- Thêm dữ liệu mẫu vào bảng Chucvu
INSERT INTO Chucvu (Tenchucvu) VALUES 
('BGH'), 
('GVCN'), 
('GVBM'),
('HS');
-- Thêm dữ liệu mẫu vào bảng Lop
INSERT INTO Lop (Tenlop, Siso) VALUES 
('12A1', 50), 
('12A2', 50), 
('11A1', 50);

-- Thêm dữ liệu mẫu vào bảng Taikhoan
INSERT INTO Taikhoan (Tentaikhoan, Matkhau, Chucvu_id) VALUES 
('BGH', '123456', 1), 
('GVCN01', '123456', 2), 
('GVBM01', '123456', 3);
SELECT * FROM Taikhoan
-- Thêm dữ liệu mẫu vào bảng Monhoc
INSERT INTO Monhoc (Tenmon) VALUES 
(N'Toán'), 
(N'Lý'), 
(N'Hóa');

-- Thêm dữ liệu mẫu vào bảng Hocsinh
INSERT INTO Hocsinh (Mahocsinh, Hovaten, Gioitinh, Ngaysinh, MaLop, SDT_Phuhuynh, Hocky, Namhoc) VALUES 
('HS01', N'Vũ Minh Quân', N'Nam', '2006-01-01', 1, '0912345678', '1', '2024-2025'), 
('HS02', N'Dương Thị Hồng Nhung', N'Nữ', '2006-02-02', 2, '0912345679', '1', '2024-2025'), 
('HS03', N'Đỗ Huy Hoàng', N'Nam', '2005-03-03', 3, '0912345680', '1', '2024-2025');

-- Thêm dữ liệu mẫu vào bảng BGH
INSERT INTO BGH (Mataikhoan, Hoten, Sdt) VALUES 
('BGH001', N'Trịnh Minh Thụ', '0912345681');

-- Thêm dữ liệu mẫu vào bảng GVCN
INSERT INTO GVCN (Hovaten, Sdt, Ngaysinh, Gioitinh, Diachi, Chucvu_id, Lopchunhiem, Monday) VALUES 
(N'Trần Thị Cẩm Giang', '0912345682', '1980-04-04', 'Nữ', 'Hà Nội', 2, 1, 1);

-- Thêm dữ liệu mẫu vào bảng GVBM
INSERT INTO GVBM (Hovaten, Sdt, Ngaysinh, Gioitinh, Chucvu_id, Diachi, Monday) VALUES 
('Kiều Tuấn Dũng', '0912345683', '1985-05-05', 'Nam', 3, 'Hà Nội', 2);

-- Thêm dữ liệu mẫu vào bảng Hocsinh_Monhoc
INSERT INTO Hocsinh_Monhoc (hocsinh_id, monhoc_id, Diemso, Hocky, Namhoc) VALUES 
(1, 1, 8, '1', '2024-2025'), 
(2, 2, 7, '1', '2024-2025'), 
(3, 3, 9, '1', '2024-2025');

/* a. Quan hệ Một-nhiều giữa Chucvu và Taikhoan:
Bảng Chucvu: Lưu trữ các chức vụ trong hệ thống.
Bảng Taikhoan: Lưu trữ thông tin tài khoản người dùng.
Quan hệ: Mỗi chức vụ trong bảng Chucvu có thể có nhiều tài khoản trong bảng Taikhoan (ví dụ: mỗi chức vụ BGH có thể có nhiều tài khoản tương ứng).
Khóa ngoại: Chucvu_id trong bảng Taikhoan tham chiếu đến id trong bảng Chucvu.
b. Quan hệ Một-nhiều giữa Lop và Hocsinh:
Bảng Lop: Lưu trữ thông tin các lớp học.
Bảng Hocsinh: Lưu trữ thông tin học sinh.
Quan hệ: Mỗi lớp học trong bảng Lop có thể có nhiều học sinh trong bảng Hocsinh.
Khóa ngoại: MaLop trong bảng Hocsinh tham chiếu đến id trong bảng Lop.
c. Quan hệ Một-một giữa Monhoc và GVCN:
Bảng Monhoc: Lưu trữ thông tin các môn học.
Bảng GVCN: Lưu trữ thông tin Giáo viên chủ nhiệm.
Quan hệ: Mỗi Giáo viên chủ nhiệm trong bảng GVCN chỉ chủ nhiệm một lớp và một môn học cụ thể (mỗi môn học có thể được dạy bởi nhiều giáo viên).
Khóa ngoại: Monday trong bảng GVCN tham chiếu đến id trong bảng Monhoc.
d. Quan hệ Một-nhiều giữa Monhoc và GVBM:
Bảng Monhoc: Lưu trữ thông tin các môn học.
Bảng GVBM: Lưu trữ thông tin Giáo viên bộ môn.
Quan hệ: Mỗi Giáo viên bộ môn trong bảng GVBM chỉ dạy một môn học cụ thể (mỗi môn học có thể được dạy bởi nhiều giáo viên).
Khóa ngoại: Monday trong bảng GVBM tham chiếu đến id trong bảng Monhoc.
2. Quan hệ Nhiều-nhiều (N-N)
a. Quan hệ Nhiều-nhiều giữa Hocsinh và Monhoc qua bảng trung gian Hocsinh_Monhoc:
Bảng Hocsinh: Lưu trữ thông tin học sinh.
Bảng Monhoc: Lưu trữ thông tin các môn học.
Bảng Hocsinh_Monhoc: Là bảng trung gian để lưu thông tin về mối quan hệ giữa học sinh và môn học (chẳng hạn như điểm số).
Quan hệ: Mỗi học sinh trong bảng Hocsinh có thể tham gia nhiều môn học khác nhau, và mỗi môn học trong bảng Monhoc có thể có nhiều học sinh tham gia.
Khóa chính và khóa ngoại:
hocsinh_id trong bảng Hocsinh_Monhoc tham chiếu đến id trong bảng Hocsinh.
monhoc_id trong bảng Hocsinh_Monhoc tham chiếu đến id trong bảng Monhoc.
*/

SELECT * FROM GVBM


SELECT TOP 3 
    hsmh.monhoc_id,
    hs.Hovaten,
    hsmh.Diemso,
    hsmh.Hocky,
    hsmh.Namhoc
FROM Hocsinh_monhoc hsmh
INNER JOIN Monhoc m ON hsmh.monhoc_id = m.id
INNER JOIN Hocsinh hs ON hsmh.hocsinh_id = hs.id
ORDER BY hsmh.hocsinh_id, hsmh.monhoc_id