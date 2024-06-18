CREATE TABLE Account
(
    Username VARCHAR2(50) NOT NULL,
    Pass VARCHAR2(50),
    Email varchar2(100),
    IsAdminID varchar2(8),
    Displayname NVarchar2(50),
    CONSTRAINT pk_tk PRIMARY KEY (Username)
);

CREATE TABLE UserDetails
(
    Username VARCHAR2(50) NOT NULL PRIMARY KEY,
    Address NVARCHAR2(100),
    CMND VARCHAR2(12),
    PhoneNumber VARCHAR2(11)
);

CREATE TABLE IsAdmin
(
    IsAdminID varchar2(8) NOT NULL,
    NameA NVarchar2(100),
    CONSTRAINT pk_Quyen PRIMARY KEY (IsAdminID)
);

CREATE TABLE Categories
(
    CategoryID varchar2(50) NOT NULL,
    Name NVarchar2(100),
    CONSTRAINT pk_cat PRIMARY KEY (CategoryID)
);

CREATE TABLE Vendors
(
    VendorId varchar2(50) NOT NULL,
    Name NVarchar2(50),
    CONSTRAINT pk_Vendors PRIMARY KEY (VendorId)
);

CREATE TABLE Orders
(
    OrderId varchar2(50) NOT NULL,
    CustomerID varchar2(20),
    Address NVarchar2(100),
    OrderTotal number(10,2),
    OrderPlaced date,
    Username VARCHAR2(50),
    CONSTRAINT pk_Orders PRIMARY KEY (OrderId)
);

CREATE TABLE Customer
(
    CustomerID varchar2(20) PRIMARY KEY NOT NULL,
    FirtName NVarchar2(20),
    LastName NVarchar2(50),
    PhoneNumber varchar2(50),
    Email varchar2(50),
    GenderID INT
);

CREATE TABLE Gender
(
    GenderID INT PRIMARY KEY NOT NULL,
    GenderType NVARCHAR2(5)
);

CREATE TABLE OrderDetails
(
    OrderDetailId varchar2(50) NOT NULL,
    OrderId varchar2(50),
    ProductId varchar2(50),
    Amount number(10),
    Price number(10,2),
    CONSTRAINT pk_OrderDetails PRIMARY KEY (OrderDetailId)
);

CREATE TABLE Products
(
    ProductId varchar2(50) NOT NULL,
    Name NVarchar2(100),
    Description NVarchar2(1000),
    Price number(10,2),
    Quantity number,
    guarantee varchar2(50),
    CategoryId varchar2(50),
    VendorId varchar2(50),
    DiscountPercent number(5,2),
    CONSTRAINT pk_Products PRIMARY KEY (ProductId)
);

ALTER TABLE Products MODIFY guarantee number;

ALTER TABLE Products MODIFY Price number(10,2);

ALTER TABLE Account ADD CONSTRAINT fk_tk_quyen FOREIGN KEY(IsAdminID) REFERENCES IsAdmin(IsAdminID);

ALTER TABLE OrderDetails ADD CONSTRAINT fk_OrderDetails_Orders FOREIGN KEY(OrderId) REFERENCES Orders(OrderId);

ALTER TABLE OrderDetails ADD CONSTRAINT fk_OrderDetails_Products FOREIGN KEY(ProductId) REFERENCES Products(ProductId);

ALTER TABLE Products ADD CONSTRAINT fk_Products_Categories FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId);

ALTER TABLE Products ADD CONSTRAINT fk_Products_Vendors FOREIGN KEY(VendorId) REFERENCES Vendors(VendorId);

ALTER TABLE UserDetails ADD CONSTRAINT fk_Username_UserDetail FOREIGN KEY(Username) REFERENCES Account(Username);

ALTER TABLE Orders ADD CONSTRAINT fk_Order_userName FOREIGN KEY(Username) REFERENCES Account(Username);

ALTER TABLE Orders ADD CONSTRAINT fk_Order_Customer FOREIGN KEY(CustomerID) REFERENCES Customer(CustomerID);

ALTER TABLE Customer ADD CONSTRAINT fk_Customer_Gender FOREIGN KEY(GenderID) REFERENCES Gender(GenderID);

ALTER TABLE Gender ADD CONSTRAINT fk_Account_Gender FOREIGN KEY(GenderID) REFERENCES Gender(GenderID);

-- Ràng bu?c 
ALTER TABLE Products ADD CONSTRAINT CHK_QUANTITY CHECK (QUANTITY>=0);

-- INSERT INTO ISADMIN 
INSERT INTO IsAdmin VALUES ('ad', 'Admin');
INSERT INTO IsAdmin VALUES ('sl', 'Sale');

-- INSERT INTO ACCOUNT 
INSERT INTO Account VALUES ('anhminh', 'nguyenhai1231', 'anhmin123@gmail.com', 'sl', N'Anh Minh');
INSERT INTO Account VALUES ('vananh123', 'vananh33', 'vananh33@gmail.com', 'sl', N'Tr?n Th? Vân Anh');
INSERT INTO Account VALUES ('khnam123', 'vananh33', 'vananh33@gmail.com', 'ad', N'Nam');
select * from Account
-- INSERT INTO GENDER 
INSERT INTO Gender VALUES (1, N'NAM');
INSERT INTO Gender VALUES (2, N'N?');
commit
-- INSERT INTO CUSTOMER 
INSERT INTO Customer VALUES ('KH01', N'Anh', N'Nguy?n H?i', '0903926730', N'haianh34@gmail.com', 1);
INSERT INTO Customer VALUES ('KH02', N'Hà', N'Tr?n Vi?t', '0945146730', N'tranvietha4@gmail.com', 2);
INSERT INTO Customer VALUES ('KH03', N'H?ng', N'Phan Thanh', '0332922730', N'phanhang67@gmail.com', 2);
INSERT INTO Customer VALUES ('KH04', N'Bi?u', N'H?a Quang', '0953931730', N'quangbieu32@gmail.com', 1);
INSERT INTO Customer VALUES ('KH05',N'Thanh',N'Nguy?n Th?','0931986730',N'thithanh23@gmail.com',2);
INSERT INTO Customer VALUES ('KH06',N'Nhi',N'Phùng Ái','0943926734',N'nhiai@gmail.com',2);
INSERT INTO Customer VALUES ('KH07',N'Tú',N'Bùi Anh','0343826735',N'nhuta4@gmail.com',1);
INSERT INTO Customer VALUES ('KH08',N'Anh',N'Lâm Quang','0343912430',N'nhuta4@gmail.com',1);
INSERT INTO Customer VALUES ('KH09',N'Anh',N'Tr?n Th? Vân','0321312833',N'vananh1410@gmail.com',2);
INSERT INTO Customer VALUES ('KH10',N'Nh?t',N'Ph?m Minh','0908926760',N'nhut1205@gmail.com',2);
INSERT INTO Customer VALUES ('KH011',N'Hoà',N'Ngô B?u','0921862343','buuhoa09@gmail.com',1);
INSERT INTO Customer VALUES ('KH012',N'Khoa',N'?oàn Vi?t','0921868745','vietkhoa021@gmail.com',1);
INSERT INTO Customer VALUES ('KH013',N'Hân',N'Quách Gia','0922862343','giahan29@gmail.com',2);
INSERT INTO Customer VALUES ('KH014',N'Tr??ng',N'T? Quang','0361862343','quangtruong293@gmail.com',1);
INSERT INTO Customer VALUES ('KH015',N'Nhi',N'H? Vân','0921332343','vannhi512@gmail.com',2);
INSERT INTO Customer VALUES ('KH016',N'Vinh',N'Nguy?n T?n','0921862377','tanvinh728@gmail.com',1);
INSERT INTO Customer VALUES ('KH017',N'Hi?u',N'Nguy?n ??ng','0921861188','danghieu83@gmail.com',1);
INSERT INTO Customer VALUES ('KH018',N'D?ng',N'Lê Công Minh','0945662343','minhdung23@gmail.com',1);
INSERT INTO Customer VALUES ('KH019',N'Uyên',N'Bùi M?','0921878123','myuyen23@gmail.com',2);
INSERT INTO Customer VALUES ('KH020',N'Th?ng',N'Nguy?n Bá ??i','0927762343','daithang22@gmail.com',1);
INSERT INTO Customer VALUES ('KH021', N'??t', N'Nguy?n Thành', '0325835947', 'nguyenthanhdai0102@gmail.com', 1);
INSERT INTO Customer VALUES ('KH022', N'Thiên', N'Nguy?n Cao', '0323755564', 'nguyencaothien0912@gmail.com', 1);
INSERT INTO Customer VALUES ('KH023', N'Hà', N'Nguy?n Thu', '0345678936', 'nguyenthuha1204@gmail.com', 2);
INSERT INTO Customer VALUES ('KH024', N'Lan', N'H? Th?', '0363595784', 'hothilan0912@gmail.com', 2);
INSERT INTO Customer VALUES ('KH025', N'Trung', N'Tr?n V?n', '0395838913', 'tranvantrung1407@gmail.com', 1);
INSERT INTO Customer VALUES ('KH026', N'Tiên', N'Lý Th?', '0872689364', 'lythitien0911@gmail.com', 2);
INSERT INTO Customer VALUES ('KH027', N'Quyên', N'Tr?n Ng?c', '0892456856', 'tranngocquyen0910@gmail.com', 2);
INSERT INTO Customer VALUES ('KH028', N'D?ng', N'La Chí', '0374680975', 'lachidung2309@gmail.com', 1);
INSERT INTO Customer VALUES ('KH029', N'Long', N'Nguy?n Lê', '03240863574', 'nguyenlelong2212@gmail.com', 1);
INSERT INTO Customer VALUES ('KH030', N'Xuân', N'Nguy?n Ki?u', '0385082596', 'nguyenkieuxuan0909@gmail.com', 2);

-- INSERT CATEGORIES
INSERT INTO Categories VALUES ('CAT01',N'??ng H? C?');
INSERT INTO Categories VALUES ('CAT02',N'??ng H? Th?ch Anh');
INSERT INTO Categories VALUES ('CAT03',N'??ng H? Thông Minh');
INSERT INTO Categories VALUES ('CAT04',N'??ng H? Solar');
INSERT INTO Categories VALUES ('CAT05',N'??ng H? ?i?n T?');
INSERT INTO Categories VALUES ('CAT06',N'??ng H? Lai');
INSERT INTO Categories VALUES ('CAT07',N'??ng H? Quân ??i');
INSERT INTO Categories VALUES ('CAT08', N'??ng H? Th? Thao');
INSERT INTO Categories VALUES ('CAT09', N'??ng H? Tr? Em');
INSERT INTO Categories VALUES ('CAT10', N'??ng H? Dây Kim Lo?i');
INSERT INTO Categories VALUES ('CAT11', N'??ng H? Dây Cao Su');
INSERT INTO Categories VALUES ('CAT12', N'??ng H? Dây Da');
INSERT INTO Categories VALUES ('CAT13', N'??ng H? Phong Cách Vintage');
INSERT INTO Categories VALUES ('CAT14', N'??ng H? Nam');
INSERT INTO Categories VALUES ('CAT15', N'??ng H? N?');
-- INSERT VENDORS
INSERT INTO Vendors VALUES	('VEN01',N'Th? Gi?i Di ??ng');
INSERT INTO Vendors VALUES ('VEN02',N'Tân Tân Watch');
INSERT INTO Vendors VALUES ('VEN03',N'VinaWatch');
INSERT INTO Vendors VALUES ('VEN04',N'JPWatch');
INSERT INTO Vendors VALUES ('VEN05',N'Duy Anh Watch');
INSERT INTO Vendors VALUES ('VEN06',N'ZenWatch');
INSERT INTO Vendors VALUES ('VEN07',N'??ng H? Th?nh Phát');
INSERT INTO Vendors VALUES ('VEN08',N'Sokolov');
INSERT INTO Vendors VALUES ('VEN09',N'Orient');
INSERT INTO Vendors VALUES ('VEN10',N'Citizen');
INSERT INTO Vendors VALUES ('VEN11',N'Daniel Wellington (DW)');
INSERT INTO Vendors VALUES ('VEN12',N'Doxa');
INSERT INTO Vendors VALUES ('VEN13',N'Tissot');
INSERT INTO Vendors VALUES ('VEN14',N'Longines');
INSERT INTO Vendors VALUES ('VEN15',N'Seiko');
INSERT INTO Vendors VALUES ('VEN16',N'Fossil');

-- INSERT PRODUCTS
INSERT INTO Products VALUES	('PRO01',N'??ng h? thông minh Microwear GT4 Max',N'S? h?u thi?t k? th?i th??ng, sang tr?ng. Cùng v?i các thu?t toán thông minh giúp vi?c ho?t ??ng hi?u qu?, cung c?p s?c m?nh nâng cao kh? n?ng tính toán…',950000,10,2,'CAT03','VEN04',20);
INSERT INTO Products VALUES ('PRO02',N'??ng H? Thông Minh G-Max Watch 5 Pro',N'??ng h? thông minh G-Max Watch 5 Pro s? h?u thi?t k? ??ng c?p tuy?t ??p, b?n b? và nhi?u tính n?ng ?u vi?t s? là s? l?a ch?n hàng ??u cho phái m?nh',1050000,5,2,'CAT03','VEN03',40);
INSERT INTO Products VALUES ('PRO04',N'??ng H? Thông Minh DT8 Max',N'??ng h? thông minh G-Max Watch 5 Pro s? h?u thi?t k? ??ng c?p tuy?t ??p, b?n b? và nhi?u tính n?ng ?u vi?t s? là s? l?a ch?n hàng ??u cho phái m?nh',799000,20,2,'CAT03','VEN06',30);
INSERT INTO Products VALUES ('PRO05',N'Casio W-218H-3AVDF',N'M?u Casio W-218H-3AVDF ki?u dáng n?n m?t s? ?i?n t? hi?n th? ?a ch?c n?ng ??y ti?n ích, v?i phiên b?n thi?t k? t?ng th? b?ng nh?a cùng kh? n?ng ch?u n??c 5 ATM t?o nên v? cá tính ??y m?nh m?.',777000,4,1,'CAT07','VEN05',40);
INSERT INTO Products VALUES ('PRO06',N'Seiko 5 Field Sports Style SRPH29K1',N'M?u Seiko SRPH29K1 phiên b?n dây v?i tone màu xanh qu?n ??i, chi ti?t kim ch? cùng các c?c ch?m tròn nh? ???c ph? d? quang trên n?n m?t s? ?en kích th??c 39.4mm.',7100000,2,2,'CAT07','VEN05',30);
INSERT INTO Products VALUES ('PRO07',N'Seiko 5 Field Sports Style SRPD83K1',N'M?u Seiko SRPD83K1 v? ngoài tr? trung cá tính v?i m?u dây v?i ?en ?i kèm v?i c?c ch?m tròn ?? ph? d? quang n?i b?t trên m?t s? ?en size 42,5mm.',8060000,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO08',N'Citizen Tsuyosa NJ0154-80H',N'M?u Citizen NJ0154-80H phiên b?n m?t kính ch?t li?u kính Sapphire v?i kích th??c nam tính 40mm, k?t h?p cùng m?u dây ?eo kim lo?i dây vàng demi phong cách th?i trang sang tr?ng.',12600000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO09',N'Casio LTP-1183Q-9ADF',N'??ng h? th?i trang dây da Casio LTP-1183Q-9ADF dành cho n? v?i thi?t k? sang tr?ng, m?t n?n vàng, v?i 3 kim ch? m? vàng và 1 l?ch ngày.',1036000,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO10',N'Saga 53766-GPZMGGE-2',N'M?u Saga 53766-GPZMGGE-2 dây da tr?ng phiên b?n da tr?n tr? trung, m?t s? tr?ng, ???c kh?m xà c? v?i hi?u ?ng b?t m?t.',6720000,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO11',N'Doxa GrandeMetre D154RWH',N'M?u Doxa D154RWH phiên b?n gi?i h?n 1000 chi?c trên toàn th? gi?i, s? k?t h?p cách ?i?u gi?a các v?ch s? cùng ch? s? la mã dày d?n.',1447700,1,3,'CAT04','VEN03',10);
INSERT INTO Products VALUES ('PRO12',N'Tissot Lovely Square T058.109.11.036.00',N'M?u Tissot T058.109.11.036.00 phiên b?n sang tr?ng 12 viên kim c??ng t??ng ?ng v?i 12 múi gi? ?ính trên n?n m?t s? vuông v?i kích th??c nh? 20mm.',10330000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO13',N'Fossil ME3099 ',N'M?u ??ng h? Fossil ME3099 v?i v? ngoài ???c thi?t k? theo phong cách gi?n d? nh?ng ch?a ??ng v? ??c ?áo v?i n?n m?t s? chân kính l? ra 1 ph?n tr?i nghi?m c?a b? máy c? ??m ch?t nam tính.',6880000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO14',N'Calvin Klein K0K28120',N'M?u Calvin Klein K0K28120 thi?t k? 3 núm v?n ?i?u ch?nh các ch?c n?ng ?o th?i gian Chronograph hi?n th? trên m?t s? size 39mm.',10420000 ,1,1,'CAT05','VEN01',10);
INSERT INTO Products VALUES ('PRO15',N'Rado Coupole R22862154',N'M?u Rado R22862154 m?t s? ?en size 31mm thi?t k? h?a ti?t tr?i tia nh? phong cách th?i trang tr? trung cùng v?i thi?t k? m?ng trên chi ti?t kim ch? cùng c?c v?ch s?.',34500000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO16',N'Casio EFR-526L-2AVUDF',N'??ng h? Casio EFR-526L-2AVUDF v?i thi?t k? dành cho nam theo phong cách th?i trang, kim ch? và v?ch s? to rõ n?i b?t trên n?n s? xanh th? thao, ô l?ch ngày v? trí 3 gi?.',3356000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO17',N'Daniel Wellington MOP Quadro Special Edition',N'M?u Daniel Wellington DW00100509 phiên b?n m?t s? ch? nh?t ???c ph?i tone màu h?ng xà c? th?i trang tr? trung k?t h?p cùng m?u máy pin ???c thi?t k? siêu m?ng ch? 6mm.',4533000  ,1,1,'CAT15','VEN03',10);
INSERT INTO Products VALUES ('PRO18',N'Daniel Wellington Quadro Studio DW00100518',N'M?u Daniel Wellington DW00100518 phiên b?n m?t s? vuông ?en kích th??c 22mm th?i trang tr? trung k?t h?p cùng m?u dây ?eo kim lo?i ???c ph?i tone màu vàng h?ng.',5195000 ,1,1,'CAT15','VEN03',10);
INSERT INTO Products VALUES ('PRO19',N'Casio F-91W-1HDG',N'M?u Casio F-91W-1HDG phiên b?n m?t s? vuông ?i?n t? phong cách hoài c?, thi?t k? ?a ch?c n?ng k?t h?p cùng m?u dây v? cao su kh? n?ng ch?u va ??p.',492000 ,1,1,'CAT14','VEN03',10);
INSERT INTO Products VALUES ('PRO20',N'Casio W-218H-3AVDF',N'M?u Casio W-218H-3AVDF ki?u dáng n?n m?t s? ?i?n t? hi?n th? ?a ch?c n?ng ??y ti?n ích, v?i phiên b?n thi?t k? t?ng th? b?ng nh?a cùng kh? n?ng ch?u n??c 5 ATM t?o nên v? cá tính ??y m?nh m?.',777000 ,1,N'1 N?m','CAT14','VEN03',10);
INSERT INTO Products VALUES ('PRO21',N'Casio HDC-700-3A3VDF',N'M?u Casio HDC-700-3A3VDF phiên b?n cá tính v?i thi?t k? dây v? nh?a ch?u va ??p, m?t s? ?i?n t? hi?n th? ?a ch?c n?ng n?i b?t v?i kh? n?ng ch?ng n??c lên ??n 10atm.',1633000 ,1,1,'CAT11','VEN03',10);
commit
