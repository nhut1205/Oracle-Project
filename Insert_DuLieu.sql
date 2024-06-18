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

-- R�ng bu?c 
ALTER TABLE Products ADD CONSTRAINT CHK_QUANTITY CHECK (QUANTITY>=0);

-- INSERT INTO ISADMIN 
INSERT INTO IsAdmin VALUES ('ad', 'Admin');
INSERT INTO IsAdmin VALUES ('sl', 'Sale');

-- INSERT INTO ACCOUNT 
INSERT INTO Account VALUES ('anhminh', 'nguyenhai1231', 'anhmin123@gmail.com', 'sl', N'Anh Minh');
INSERT INTO Account VALUES ('vananh123', 'vananh33', 'vananh33@gmail.com', 'sl', N'Tr?n Th? V�n Anh');
INSERT INTO Account VALUES ('khnam123', 'vananh33', 'vananh33@gmail.com', 'ad', N'Nam');
select * from Account
-- INSERT INTO GENDER 
INSERT INTO Gender VALUES (1, N'NAM');
INSERT INTO Gender VALUES (2, N'N?');
commit
-- INSERT INTO CUSTOMER 
INSERT INTO Customer VALUES ('KH01', N'Anh', N'Nguy?n H?i', '0903926730', N'haianh34@gmail.com', 1);
INSERT INTO Customer VALUES ('KH02', N'H�', N'Tr?n Vi?t', '0945146730', N'tranvietha4@gmail.com', 2);
INSERT INTO Customer VALUES ('KH03', N'H?ng', N'Phan Thanh', '0332922730', N'phanhang67@gmail.com', 2);
INSERT INTO Customer VALUES ('KH04', N'Bi?u', N'H?a Quang', '0953931730', N'quangbieu32@gmail.com', 1);
INSERT INTO Customer VALUES ('KH05',N'Thanh',N'Nguy?n Th?','0931986730',N'thithanh23@gmail.com',2);
INSERT INTO Customer VALUES ('KH06',N'Nhi',N'Ph�ng �i','0943926734',N'nhiai@gmail.com',2);
INSERT INTO Customer VALUES ('KH07',N'T�',N'B�i Anh','0343826735',N'nhuta4@gmail.com',1);
INSERT INTO Customer VALUES ('KH08',N'Anh',N'L�m Quang','0343912430',N'nhuta4@gmail.com',1);
INSERT INTO Customer VALUES ('KH09',N'Anh',N'Tr?n Th? V�n','0321312833',N'vananh1410@gmail.com',2);
INSERT INTO Customer VALUES ('KH10',N'Nh?t',N'Ph?m Minh','0908926760',N'nhut1205@gmail.com',2);
INSERT INTO Customer VALUES ('KH011',N'Ho�',N'Ng� B?u','0921862343','buuhoa09@gmail.com',1);
INSERT INTO Customer VALUES ('KH012',N'Khoa',N'?o�n Vi?t','0921868745','vietkhoa021@gmail.com',1);
INSERT INTO Customer VALUES ('KH013',N'H�n',N'Qu�ch Gia','0922862343','giahan29@gmail.com',2);
INSERT INTO Customer VALUES ('KH014',N'Tr??ng',N'T? Quang','0361862343','quangtruong293@gmail.com',1);
INSERT INTO Customer VALUES ('KH015',N'Nhi',N'H? V�n','0921332343','vannhi512@gmail.com',2);
INSERT INTO Customer VALUES ('KH016',N'Vinh',N'Nguy?n T?n','0921862377','tanvinh728@gmail.com',1);
INSERT INTO Customer VALUES ('KH017',N'Hi?u',N'Nguy?n ??ng','0921861188','danghieu83@gmail.com',1);
INSERT INTO Customer VALUES ('KH018',N'D?ng',N'L� C�ng Minh','0945662343','minhdung23@gmail.com',1);
INSERT INTO Customer VALUES ('KH019',N'Uy�n',N'B�i M?','0921878123','myuyen23@gmail.com',2);
INSERT INTO Customer VALUES ('KH020',N'Th?ng',N'Nguy?n B� ??i','0927762343','daithang22@gmail.com',1);
INSERT INTO Customer VALUES ('KH021', N'??t', N'Nguy?n Th�nh', '0325835947', 'nguyenthanhdai0102@gmail.com', 1);
INSERT INTO Customer VALUES ('KH022', N'Thi�n', N'Nguy?n Cao', '0323755564', 'nguyencaothien0912@gmail.com', 1);
INSERT INTO Customer VALUES ('KH023', N'H�', N'Nguy?n Thu', '0345678936', 'nguyenthuha1204@gmail.com', 2);
INSERT INTO Customer VALUES ('KH024', N'Lan', N'H? Th?', '0363595784', 'hothilan0912@gmail.com', 2);
INSERT INTO Customer VALUES ('KH025', N'Trung', N'Tr?n V?n', '0395838913', 'tranvantrung1407@gmail.com', 1);
INSERT INTO Customer VALUES ('KH026', N'Ti�n', N'L� Th?', '0872689364', 'lythitien0911@gmail.com', 2);
INSERT INTO Customer VALUES ('KH027', N'Quy�n', N'Tr?n Ng?c', '0892456856', 'tranngocquyen0910@gmail.com', 2);
INSERT INTO Customer VALUES ('KH028', N'D?ng', N'La Ch�', '0374680975', 'lachidung2309@gmail.com', 1);
INSERT INTO Customer VALUES ('KH029', N'Long', N'Nguy?n L�', '03240863574', 'nguyenlelong2212@gmail.com', 1);
INSERT INTO Customer VALUES ('KH030', N'Xu�n', N'Nguy?n Ki?u', '0385082596', 'nguyenkieuxuan0909@gmail.com', 2);

-- INSERT CATEGORIES
INSERT INTO Categories VALUES ('CAT01',N'??ng H? C?');
INSERT INTO Categories VALUES ('CAT02',N'??ng H? Th?ch Anh');
INSERT INTO Categories VALUES ('CAT03',N'??ng H? Th�ng Minh');
INSERT INTO Categories VALUES ('CAT04',N'??ng H? Solar');
INSERT INTO Categories VALUES ('CAT05',N'??ng H? ?i?n T?');
INSERT INTO Categories VALUES ('CAT06',N'??ng H? Lai');
INSERT INTO Categories VALUES ('CAT07',N'??ng H? Qu�n ??i');
INSERT INTO Categories VALUES ('CAT08', N'??ng H? Th? Thao');
INSERT INTO Categories VALUES ('CAT09', N'??ng H? Tr? Em');
INSERT INTO Categories VALUES ('CAT10', N'??ng H? D�y Kim Lo?i');
INSERT INTO Categories VALUES ('CAT11', N'??ng H? D�y Cao Su');
INSERT INTO Categories VALUES ('CAT12', N'??ng H? D�y Da');
INSERT INTO Categories VALUES ('CAT13', N'??ng H? Phong C�ch Vintage');
INSERT INTO Categories VALUES ('CAT14', N'??ng H? Nam');
INSERT INTO Categories VALUES ('CAT15', N'??ng H? N?');
-- INSERT VENDORS
INSERT INTO Vendors VALUES	('VEN01',N'Th? Gi?i Di ??ng');
INSERT INTO Vendors VALUES ('VEN02',N'T�n T�n Watch');
INSERT INTO Vendors VALUES ('VEN03',N'VinaWatch');
INSERT INTO Vendors VALUES ('VEN04',N'JPWatch');
INSERT INTO Vendors VALUES ('VEN05',N'Duy Anh Watch');
INSERT INTO Vendors VALUES ('VEN06',N'ZenWatch');
INSERT INTO Vendors VALUES ('VEN07',N'??ng H? Th?nh Ph�t');
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
INSERT INTO Products VALUES	('PRO01',N'??ng h? th�ng minh Microwear GT4 Max',N'S? h?u thi?t k? th?i th??ng, sang tr?ng. C�ng v?i c�c thu?t to�n th�ng minh gi�p vi?c ho?t ??ng hi?u qu?, cung c?p s?c m?nh n�ng cao kh? n?ng t�nh to�n�',950000,10,2,'CAT03','VEN04',20);
INSERT INTO Products VALUES ('PRO02',N'??ng H? Th�ng Minh G-Max Watch 5 Pro',N'??ng h? th�ng minh G-Max Watch 5 Pro s? h?u thi?t k? ??ng c?p tuy?t ??p, b?n b? v� nhi?u t�nh n?ng ?u vi?t s? l� s? l?a ch?n h�ng ??u cho ph�i m?nh',1050000,5,2,'CAT03','VEN03',40);
INSERT INTO Products VALUES ('PRO04',N'??ng H? Th�ng Minh DT8 Max',N'??ng h? th�ng minh G-Max Watch 5 Pro s? h?u thi?t k? ??ng c?p tuy?t ??p, b?n b? v� nhi?u t�nh n?ng ?u vi?t s? l� s? l?a ch?n h�ng ??u cho ph�i m?nh',799000,20,2,'CAT03','VEN06',30);
INSERT INTO Products VALUES ('PRO05',N'Casio W-218H-3AVDF',N'M?u Casio W-218H-3AVDF ki?u d�ng n?n m?t s? ?i?n t? hi?n th? ?a ch?c n?ng ??y ti?n �ch, v?i phi�n b?n thi?t k? t?ng th? b?ng nh?a c�ng kh? n?ng ch?u n??c 5 ATM t?o n�n v? c� t�nh ??y m?nh m?.',777000,4,1,'CAT07','VEN05',40);
INSERT INTO Products VALUES ('PRO06',N'Seiko 5 Field Sports Style SRPH29K1',N'M?u Seiko SRPH29K1 phi�n b?n d�y v?i tone m�u xanh qu?n ??i, chi ti?t kim ch? c�ng c�c c?c ch?m tr�n nh? ???c ph? d? quang tr�n n?n m?t s? ?en k�ch th??c 39.4mm.',7100000,2,2,'CAT07','VEN05',30);
INSERT INTO Products VALUES ('PRO07',N'Seiko 5 Field Sports Style SRPD83K1',N'M?u Seiko SRPD83K1 v? ngo�i tr? trung c� t�nh v?i m?u d�y v?i ?en ?i k�m v?i c?c ch?m tr�n ?? ph? d? quang n?i b?t tr�n m?t s? ?en size 42,5mm.',8060000,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO08',N'Citizen Tsuyosa NJ0154-80H',N'M?u Citizen NJ0154-80H phi�n b?n m?t k�nh ch?t li?u k�nh Sapphire v?i k�ch th??c nam t�nh 40mm, k?t h?p c�ng m?u d�y ?eo kim lo?i d�y v�ng demi phong c�ch th?i trang sang tr?ng.',12600000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO09',N'Casio LTP-1183Q-9ADF',N'??ng h? th?i trang d�y da Casio LTP-1183Q-9ADF d�nh cho n? v?i thi?t k? sang tr?ng, m?t n?n v�ng, v?i 3 kim ch? m? v�ng v� 1 l?ch ng�y.',1036000,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO10',N'Saga 53766-GPZMGGE-2',N'M?u Saga 53766-GPZMGGE-2 d�y da tr?ng phi�n b?n da tr?n tr? trung, m?t s? tr?ng, ???c kh?m x� c? v?i hi?u ?ng b?t m?t.',6720000,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO11',N'Doxa GrandeMetre D154RWH',N'M?u Doxa D154RWH phi�n b?n gi?i h?n 1000 chi?c tr�n to�n th? gi?i, s? k?t h?p c�ch ?i?u gi?a c�c v?ch s? c�ng ch? s? la m� d�y d?n.',1447700,1,3,'CAT04','VEN03',10);
INSERT INTO Products VALUES ('PRO12',N'Tissot Lovely Square T058.109.11.036.00',N'M?u Tissot T058.109.11.036.00 phi�n b?n sang tr?ng 12 vi�n kim c??ng t??ng ?ng v?i 12 m�i gi? ?�nh tr�n n?n m?t s? vu�ng v?i k�ch th??c nh? 20mm.',10330000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO13',N'Fossil ME3099 ',N'M?u ??ng h? Fossil ME3099 v?i v? ngo�i ???c thi?t k? theo phong c�ch gi?n d? nh?ng ch?a ??ng v? ??c ?�o v?i n?n m?t s? ch�n k�nh l? ra 1 ph?n tr?i nghi?m c?a b? m�y c? ??m ch?t nam t�nh.',6880000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO14',N'Calvin Klein K0K28120',N'M?u Calvin Klein K0K28120 thi?t k? 3 n�m v?n ?i?u ch?nh c�c ch?c n?ng ?o th?i gian Chronograph hi?n th? tr�n m?t s? size 39mm.',10420000 ,1,1,'CAT05','VEN01',10);
INSERT INTO Products VALUES ('PRO15',N'Rado Coupole R22862154',N'M?u Rado R22862154 m?t s? ?en size 31mm thi?t k? h?a ti?t tr?i tia nh? phong c�ch th?i trang tr? trung c�ng v?i thi?t k? m?ng tr�n chi ti?t kim ch? c�ng c?c v?ch s?.',34500000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO16',N'Casio EFR-526L-2AVUDF',N'??ng h? Casio EFR-526L-2AVUDF v?i thi?t k? d�nh cho nam theo phong c�ch th?i trang, kim ch? v� v?ch s? to r� n?i b?t tr�n n?n s? xanh th? thao, � l?ch ng�y v? tr� 3 gi?.',3356000 ,1,1,'CAT07','VEN03',10);
INSERT INTO Products VALUES ('PRO17',N'Daniel Wellington MOP Quadro Special Edition',N'M?u Daniel Wellington DW00100509 phi�n b?n m?t s? ch? nh?t ???c ph?i tone m�u h?ng x� c? th?i trang tr? trung k?t h?p c�ng m?u m�y pin ???c thi?t k? si�u m?ng ch? 6mm.',4533000  ,1,1,'CAT15','VEN03',10);
INSERT INTO Products VALUES ('PRO18',N'Daniel Wellington Quadro Studio DW00100518',N'M?u Daniel Wellington DW00100518 phi�n b?n m?t s? vu�ng ?en k�ch th??c 22mm th?i trang tr? trung k?t h?p c�ng m?u d�y ?eo kim lo?i ???c ph?i tone m�u v�ng h?ng.',5195000 ,1,1,'CAT15','VEN03',10);
INSERT INTO Products VALUES ('PRO19',N'Casio F-91W-1HDG',N'M?u Casio F-91W-1HDG phi�n b?n m?t s? vu�ng ?i?n t? phong c�ch ho�i c?, thi?t k? ?a ch?c n?ng k?t h?p c�ng m?u d�y v? cao su kh? n?ng ch?u va ??p.',492000 ,1,1,'CAT14','VEN03',10);
INSERT INTO Products VALUES ('PRO20',N'Casio W-218H-3AVDF',N'M?u Casio W-218H-3AVDF ki?u d�ng n?n m?t s? ?i?n t? hi?n th? ?a ch?c n?ng ??y ti?n �ch, v?i phi�n b?n thi?t k? t?ng th? b?ng nh?a c�ng kh? n?ng ch?u n??c 5 ATM t?o n�n v? c� t�nh ??y m?nh m?.',777000 ,1,N'1 N?m','CAT14','VEN03',10);
INSERT INTO Products VALUES ('PRO21',N'Casio HDC-700-3A3VDF',N'M?u Casio HDC-700-3A3VDF phi�n b?n c� t�nh v?i thi?t k? d�y v? nh?a ch?u va ??p, m?t s? ?i?n t? hi?n th? ?a ch?c n?ng n?i b?t v?i kh? n?ng ch?ng n??c l�n ??n 10atm.',1633000 ,1,1,'CAT11','VEN03',10);
commit
