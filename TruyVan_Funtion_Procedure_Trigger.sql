--PH?M MINH NH?T - 2001216012
------------------------------------TRIGGER----------------------------------------------
--1.Trigger c?p nh?t ti?n t?ng h�a ??n theo chi ti?t h�a ??n
CREATE OR REPLACE TRIGGER updateOrderTotal
AFTER INSERT 
ON orderDetails
DECLARE
BEGIN
    -- C?p nh?t OrderTotal cho t?t c? c�c OrderId t??ng ?ng
    FOR order_rec IN (
        SELECT OrderId, SUM(amount * price) AS total 
        FROM orderDetails 
        GROUP BY OrderId
    )
    LOOP
        UPDATE Orders
        SET OrderTotal = order_rec.total
        WHERE OrderId = order_rec.OrderId;
    END LOOP;
EXCEPTION
    WHEN OTHERS THEN
        NULL; -- B? qua l?i ?? tr�nh vi?c trigger b? v� hi?u h�a
END;
/
--2.C?p nh?t l?i ti?n sau khi x�a s?n ph?m kh?i chi ti?t h�a ??n
CREATE OR REPLACE TRIGGER updateOrderTotalBeforeDelete
BEFORE DELETE ON orderDetails
FOR EACH ROW
DECLARE
    v_total_price NUMBER;
BEGIN
    -- T�nh to�n t?ng s? ti?n c?a m?t h�ng b? x�a
    v_total_price := :OLD.amount * :OLD.price;

    -- C?p nh?t t?ng ti?n c?a ??n h�ng t??ng ?ng
    UPDATE Orders
    SET OrderTotal = OrderTotal - v_total_price
    WHERE OrderId = :OLD.OrderId;
END;
------------------------------------FUNTION----------------------------------------------
--1.T?o h�m xem s?n ph?m b�n ch?y nh?t
CREATE OR REPLACE FUNCTION f_mathang_banchaynhat
RETURN NVARCHAR2
IS
    TENMH NVARCHAR2(50);
BEGIN
    BEGIN
        SELECT pro.Name INTO TENMH
        FROM OrderDetails ord
        INNER JOIN Products pro ON pro.ProductId = ord.ProductId
        GROUP BY pro.Name
        ORDER BY SUM(ord.Amount) DESC
        FETCH FIRST 1 ROW ONLY;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            TENMH := 'No Data';
    END;
    
    RETURN TENMH;
END;
/
--Kh?i ch?y c�u l?nh 
DECLARE
    v_result NVARCHAR2(50);
BEGIN
    v_result := f_mathang_banchaynhat();
    DBMS_OUTPUT.PUT_LINE('S?n ph?m b�n ch?y nh?t l�: ' || v_result);
END;
/
------------------------------------PROCEDURE----------------------------------------------
--1.Th? t?c th�m account
CREATE OR REPLACE PROCEDURE InsertAccount(
    p_Username IN VARCHAR2,
    p_Pass IN VARCHAR2,
    p_Email IN VARCHAR2,
    p_IsAdminID IN VARCHAR2,
    p_Displayname IN NVARCHAR2
) AS
    v_count INTEGER;
BEGIN
    -- Validate Username
    IF p_Username IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001, 'Username cannot be null.');
    END IF;

    -- Validate Password
    IF p_Pass IS NULL THEN
        RAISE_APPLICATION_ERROR(-20002, 'Password cannot be null.');
    END IF;

    -- Validate Email
    IF p_Email IS NULL THEN
        RAISE_APPLICATION_ERROR(-20003, 'Email cannot be null.');
    ELSIF NOT REGEXP_LIKE(p_Email, '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$') THEN
        RAISE_APPLICATION_ERROR(-20004, 'Invalid email format.');
    END IF;

    -- Validate IsAdminID
    IF p_IsAdminID IS NOT NULL THEN
        SELECT COUNT(*) INTO v_count FROM IsAdmin WHERE IsAdminID = p_IsAdminID;
        IF v_count = 0 THEN
            RAISE_APPLICATION_ERROR(-20005, 'IsAdminID does not exist.');
        END IF;
    END IF;

    -- Insert Data into Account Table
    INSERT INTO Account (Username, Pass, Email, IsAdminID, Displayname)
    VALUES (p_Username, p_Pass, p_Email, p_IsAdminID, p_Displayname);

    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE_APPLICATION_ERROR(-20006, 'An error occurred during insert: ' || SQLERRM);
END InsertAccount;
/

--2.Th? t?c hi?n th�ng tin nh�n vi�n v?a th�m b�o
CREATE OR REPLACE PROCEDURE DisplayAccountData AS
    CURSOR account_cursor IS
        SELECT Username, Pass, Email, IsAdminID, Displayname FROM Account;
BEGIN
    FOR account_record IN account_cursor LOOP
        DBMS_OUTPUT.PUT_LINE('Username: ' || account_record.Username);
        DBMS_OUTPUT.PUT_LINE('Password: ' || account_record.Pass);
        DBMS_OUTPUT.PUT_LINE('Email: ' || account_record.Email);
        DBMS_OUTPUT.PUT_LINE('IsAdminID: ' || account_record.IsAdminID);
        DBMS_OUTPUT.PUT_LINE('Displayname: ' || account_record.Displayname);
        DBMS_OUTPUT.PUT_LINE('--------------------------');
    END LOOP;
END DisplayAccountData;
/
--Th�m th�ng tin 
BEGIN
    InsertAccount('john_doe', 'securepassword', 'john.doe@gmail.com', 'ad', 'John Doe');
END;
/
------------------------------------SELECT----------------------------------------------
--1.Truy v?n t�nh t?ng doanh b�n h�ng cho t?ng danh m?c s?n ph?m
SELECT 
    P.CategoryId, 
    C.Name AS CategoryName, 
    SUM(OD.Amount * OD.Price) AS TotalSales
FROM 
    OrderDetails OD
JOIN 
    Products P ON OD.ProductId = P.ProductId
JOIN 
    Categories C ON P.CategoryId = C.CategoryId
WHERE 
    OD.OrderId IS NOT NULL
GROUP BY 
    P.CategoryId, C.Name
HAVING 
    SUM(OD.Amount * OD.Price) > 1000
ORDER BY 
    TotalSales DESC;
    
--2.??m s? l??ng ??n h�ng ???c ??t b?i m?i ng??i d�ng
    
SELECT 
    A.Username, 
    A.Displayname, 
    COUNT(O.OrderId) AS NumberOfOrders
FROM 
    Orders O
JOIN 
    Account A ON O.Username = A.Username
WHERE 
    O.OrderPlaced BETWEEN TO_DATE('01-01-2023', 'MM-DD-YYYY') AND TO_DATE('12-31-2024', 'MM-DD-YYYY')
GROUP BY 
    A.Username, A.Displayname
HAVING 
    COUNT(O.OrderId) > 0
ORDER BY 
    NumberOfOrders DESC;


---------------------------------------------------------------------------------------------------------------
--------------------NAM------------
------- proc th�m 1 kh�ch h�ng c� validate d? li?u
CREATE OR REPLACE PROCEDURE themKhachHang(
    p_CustomerID IN Customer.CustomerID%TYPE,
    p_FirtName IN Customer.FirtName%TYPE,
    p_LastName IN Customer.LastName%TYPE,
    p_PhoneNumber IN Customer.PhoneNumber%TYPE,
    p_Email IN Customer.Email%TYPE,
    p_GenderID IN Customer.GenderID%TYPE
) AS
    v_count INT;
BEGIN
    -- Ki?m tra xem c�c tham s? c� h?p l? hay kh�ng
    IF p_CustomerID IS NULL THEN
        RAISE_APPLICATION_ERROR(-20002, 'CustomerID kh�ng ???c ?? tr?ng');
    END IF;

    IF p_FirtName IS NULL THEN
        RAISE_APPLICATION_ERROR(-20003, 'FirtName kh�ng ???c ?? tr?ng');
    END IF;

    IF p_LastName IS NULL THEN
        RAISE_APPLICATION_ERROR(-20004, 'LastName kh�ng ???c ?? tr?ng');
    END IF;

    IF p_PhoneNumber IS NULL THEN
        RAISE_APPLICATION_ERROR(-20005, 'PhoneNumber kh�ng ???c ?? tr?ng');
    ELSE
        IF LENGTH(p_PhoneNumber) != 10 THEN
            RAISE_APPLICATION_ERROR(-20008, 'PhoneNumber ph?i c� ?�ng 10 s?');
        END IF;
    END IF;

    IF p_Email IS NULL THEN
        RAISE_APPLICATION_ERROR(-20006, 'Email kh�ng ???c ?? tr?ng');
    ELSE
        IF NOT REGEXP_LIKE(p_Email, '^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$') THEN
            RAISE_APPLICATION_ERROR(-20009, 'Email kh�ng h?p l?');
        END IF;
    END IF;

    IF p_GenderID IS NULL THEN
        RAISE_APPLICATION_ERROR(-20007, 'GenderID kh�ng ???c ?? tr?ng');
    END IF;

    -- Ki?m tra xem GenderID c� t?n t?i trong b?ng Gender hay kh�ng
    SELECT COUNT(*) INTO v_count FROM Gender WHERE GenderID = p_GenderID;

    IF v_count = 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'GenderID kh�ng t?n t?i trong b?ng Gender');
    END IF;

    -- Th�m d? li?u v�o b?ng Customer
    INSERT INTO Customer(CustomerID, FirtName, LastName, PhoneNumber, Email, GenderID)
    VALUES (p_CustomerID, p_FirtName, p_LastName, p_PhoneNumber, p_Email, p_GenderID);

    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END themKhachHang;
/
BEGIN
    themKhachHang(
        p_CustomerID => 'CUST456',
        p_FirtName => 'Nguyen',
        p_LastName => 'Van A',
        p_PhoneNumber => '0123456789',
        p_Email => 'nguyenvana@example.com',
        p_GenderID => 1
    );
END;
/


-----proc xem danh s�ch kh�ch h�ng 
CREATE OR REPLACE PROCEDURE dsKhachHang AS
    CURSOR c_customers IS
        SELECT * FROM Customer;
    v_customer c_customers%ROWTYPE;
BEGIN
    OPEN c_customers;

    LOOP
        FETCH c_customers INTO v_customer;
        EXIT WHEN c_customers%NOTFOUND;

        DBMS_OUTPUT.PUT_LINE('CustomerID: ' || v_customer.CustomerID);
        DBMS_OUTPUT.PUT_LINE('FirtName: ' || v_customer.FirtName);
        DBMS_OUTPUT.PUT_LINE('LastName: ' || v_customer.LastName);
        DBMS_OUTPUT.PUT_LINE('PhoneNumber: ' || v_customer.PhoneNumber);
        DBMS_OUTPUT.PUT_LINE('Email: ' || v_customer.Email);
        DBMS_OUTPUT.PUT_LINE('GenderID: ' || v_customer.GenderID);
        DBMS_OUTPUT.PUT_LINE('-------------------------');
    END LOOP;

    CLOSE c_customers;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('?� x?y ra l?i: ' || SQLERRM);
END dsKhachHang;
/
SET SERVEROUTPUT ON
BEGIN
    dsKhachHang;
END;
--- c?p nh?t s? l??ng h�ng b�n b?ng s?n ph?m khi th�m x�a s?a s? l??ng b�n b?ng chitiethoadon
CREATE OR REPLACE TRIGGER trg_CapNhapProduct
AFTER INSERT OR UPDATE OR DELETE ON OrderDetails
FOR EACH ROW
BEGIN
    -- Handle INSERT or UPDATE
    IF INSERTING OR UPDATING THEN
        UPDATE Products
        SET quantity = quantity - :NEW.Amount
        WHERE productid = :NEW.productid;
    END IF;

    -- Handle DELETE
    IF DELETING THEN
        UPDATE Products
        SET quantity = quantity + :OLD.Amount
        WHERE productid = :OLD.productid;
    END IF;
END;


--danh s�ch c�c H�a ??n ?� ???c x? l� b?i c�c qu?n tr? vi�n, s?p x?p theo t?ng gi� tr? ??n h�ng t? cao ??n th?p
SELECT a.Username, o.OrderId, o.OrderTotal
FROM Account a
JOIN Orders o ON a.Username = o.Username
WHERE a.IsAdminID = 'ad'
ORDER BY o.OrderTotal DESC;


--danh s�ch c�c nh�n vi�n ?� x? l� nhi?u h?n 5 ??n h�ng v� trung b�nh T?ng ti?n ???c s?p x?p gi?m d?n
SELECT A.Username, AVG(O.OrderTotal) AS AverageOrderTotal
FROM Orders O, Account A
WHERE O.Username = A.Username
GROUP BY A.Username
HAVING COUNT(O.OrderId) > 0
ORDER BY AverageOrderTotal DESC;

--danh s�ch nh?ng nh�n vi�n l� 'Admin' ?� x? l� h?n 1 ??n h�ng v� t?ng s? ti?n s? ??n h�ng thanh to�n l?n h?n 1000 ???c s?p x?p gi?m d?n
SELECT A.Username, SUM(O.OrderTotal) AS TotalOrderValue, COUNT(O.OrderId) AS NumberOfOrders
FROM Orders O, Account A
WHERE O.Username = A.Username AND A.IsAdminID = 'ad'
GROUP BY A.Username
HAVING COUNT(O.OrderId) > 1 AND SUM(O.OrderTotal) > 1000
ORDER BY TotalOrderValue DESC;

-----------------FUNTION------------------------------
--t�nh gi� tr? trung b�nh c?a T?ng Ti?n cho m?t Nh�nVi�n
CREATE OR REPLACE FUNCTION calculate_average_order(p_username IN VARCHAR2)
RETURN NUMBER IS
    v_total NUMBER(10,2);
    v_count NUMBER;
    v_average NUMBER(10,2);
BEGIN
    SELECT SUM(OrderTotal), COUNT(OrderId)
    INTO v_total, v_count
    FROM Orders
    WHERE Username = p_username;

    IF v_count > 0 THEN
        v_average := v_total / v_count;
    ELSE
        v_average := 0;
    END IF;

    RETURN v_average;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN NULL;
END calculate_average_order;

--l?nh th?c thi
SELECT calculate_average_order('vananh123') AS AverageOrderTotal FROM dual;
--ki?m tra ch?c v? c?a Nh�nVi�n
CREATE OR REPLACE FUNCTION check_admin(p_username IN VARCHAR2)
RETURN VARCHAR2 IS
    v_is_admin VARCHAR2(8);
BEGIN
    SELECT IsAdminID
    INTO v_is_admin
    FROM Account
    WHERE Username = p_username;

    IF v_is_admin = 'ad' THEN
        RETURN 'Yes';
    ELSE
        RETURN 'No';
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN 'No';
END check_admin;

SELECT check_admin('vananh123') AS IsAdmin FROM dual;
---============================================================================================== 
----------------------MINH TR�-----------------------
--L?y ra danh s�ch c�c ??n h�ng c� t?ng gi� tr? l?n h?n 50,000 v� s?p x?p theo t?ng gi� tr? gi?m d?n.
SELECT o.OrderId, c.FirtName, c.LastName, o.OrderTotal
FROM Orders o
JOIN Customer c ON o.CustomerID = c.CustomerID
WHERE o.OrderTotal > 50000
ORDER BY o.OrderTotal DESC;
--??m s? l??ng s?n ph?m v� t�nh gi� trung b�nh c?a c�c s?n ph?m theo t?ng danh m?c, ch? l?y ra c�c danh m?c c� s? l??ng s?n ph?m t?n kho l?n h?n 10 v� c� �t nh?t 1 s?n ph?m ?� ???c b�n.
SELECT p.CategoryId, COUNT(*) AS TotalProducts, AVG(p.Price) AS AveragePrice
FROM Products p
JOIN OrderDetails od ON p.ProductId = od.ProductId
WHERE p.Quantity = 0
GROUP BY p.CategoryId
HAVING COUNT(*) > 1
ORDER BY TotalProducts DESC;

--t�nh t?ng s? l??ng s?n ph?m m� m?i kh�ch h�ng ?� mua, ch? l?y ra nh?ng kh�ch h�ng c� t?ng gi� tr? mua h�ng l?n h?n 100,000
SELECT c.CustomerID, c.FirtName, c.LastName, SUM(od.Amount) AS TotalAmount
FROM Customer c
JOIN Orders ods ON c.CustomerID = ods.CustomerID
JOIN OrderDetails od ON ods.OrderId = od.OrderId
GROUP BY c.CustomerID, c.FirtName, c.LastName
HAVING SUM(od.Price * od.Amount) > 1000
ORDER BY TotalAmount DESC;

--vi?t 1 th? t?c ?? th�m (insert) d? li?u v�o m?t b?ng d? li?u v?i c�ctham s? ??u v�o l� c�c tr??ng d? li?u c?n nh?p (c� validate gi� tr? truy?n v�o v� hi?n th? th�ngb�o l?i c� ngh?a) v� 1 th? t?c ?? hi?n th? d? li?u.
CREATE OR REPLACE PROCEDURE AddProduct(
    p_ProductId IN Products.ProductId%TYPE,
    p_Name IN Products.Name%TYPE,
    p_Description IN Products.Description%TYPE,
    p_Price IN Products.Price%TYPE,
    p_Quantity IN Products.Quantity%TYPE,
    p_Guarantee IN Products.guarantee%TYPE,
    p_CategoryId IN Products.CategoryId%TYPE,
    p_VendorId IN Products.VendorId%TYPE,
    p_DiscountPercent IN Products.DiscountPercent%TYPE
) AS
BEGIN
    -- Validate the Price
    IF p_Price <= 0 THEN
        DBMS_OUTPUT.PUT_LINE('Error: The price must be greater than 0.');
        RETURN;
    END IF;

    -- Validate the Quantity
    IF p_Quantity < 0 THEN
        DBMS_OUTPUT.PUT_LINE('Error: The quantity cannot be negative.');
        RETURN;
    END IF;

    -- Validate the Discount Percent
    IF p_DiscountPercent < 0 OR p_DiscountPercent > 100 THEN
        DBMS_OUTPUT.PUT_LINE('Error: The discount percent must be between 0 and 100.');
        RETURN;
    END IF;

    -- Insert the new product
    INSERT INTO Products (ProductId, Name, Description, Price, Quantity, guarantee, CategoryId, VendorId, DiscountPercent)
    VALUES (p_ProductId, p_Name, p_Description, p_Price, p_Quantity, p_Guarantee, p_CategoryId, p_VendorId, p_DiscountPercent);

    -- Output success message
    DBMS_OUTPUT.PUT_LINE('New product added successfully with Product ID: ' || p_ProductId);
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END AddProduct;

--using
BEGIN
    AddProduct('P001', 'Apple', 'AppleWatch Serries 5', 1000, 10, 12, 'CAT03', 'VEN01', 10);
END;

--th? t?c hi?n th? th�ng tin s?n ph?m khi truy?n v�o m� s?n ph?m
CREATE OR REPLACE PROCEDURE ShowProductInfo(
    p_ProductId IN Products.ProductId%TYPE
) AS
    v_Name Products.Name%TYPE;
    v_Description Products.Description%TYPE;
    v_Price Products.Price%TYPE;
    v_Quantity Products.Quantity%TYPE;
    v_Guarantee Products.guarantee%TYPE;
    v_CategoryId Products.CategoryId%TYPE;
    v_VendorId Products.VendorId%TYPE;
    v_DiscountPercent Products.DiscountPercent%TYPE;
BEGIN
    -- Retrieve product information
    SELECT Name, Description, Price, Quantity, guarantee, CategoryId, VendorId, DiscountPercent
    INTO v_Name, v_Description, v_Price, v_Quantity, v_Guarantee, v_CategoryId, v_VendorId, v_DiscountPercent
    FROM Products
    WHERE ProductId = p_ProductId;

    -- Display product information
    DBMS_OUTPUT.PUT_LINE('Product ID: ' || p_ProductId);
    DBMS_OUTPUT.PUT_LINE('Name: ' || v_Name);
    DBMS_OUTPUT.PUT_LINE('Description: ' || v_Description);
    DBMS_OUTPUT.PUT_LINE('Price: ' || v_Price);
    DBMS_OUTPUT.PUT_LINE('Quantity: ' || v_Quantity);
    DBMS_OUTPUT.PUT_LINE('Guarantee: ' || v_Guarantee);
    DBMS_OUTPUT.PUT_LINE('Category ID: ' || v_CategoryId);
    DBMS_OUTPUT.PUT_LINE('Vendor ID: ' || v_VendorId);
    DBMS_OUTPUT.PUT_LINE('Discount Percent: ' || v_DiscountPercent);
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Error: No product found with the given Product ID.');
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END ShowProductInfo;

BEGIN
    ShowProductInfo('P001');
END;
---========TRIGGER
--Update decription khi s? l??ng s?n ph?m 
CREATE OR REPLACE TRIGGER trg_update_description
BEFORE UPDATE OF Quantity ON Products
FOR EACH ROW
BEGIN
    IF :NEW.Quantity = 0 THEN
        :NEW.Description := 'h?t h�ng';
    ELSIF :NEW.Quantity > 0 THEN
        :NEW.Description := 'h�ng m?i v?';
    END IF;
END;
/

CREATE OR REPLACE TRIGGER trg_delete_products_after_vendor
AFTER DELETE ON Vendors
FOR EACH ROW
BEGIN
    DELETE FROM Products WHERE VendorId = :OLD.VendorId;
END;
Commit


--H�m t�nh t?ng s? l??ng s?n ph?m cho m?t nh� cung c?p c? th?:
CREATE OR REPLACE FUNCTION total_products_for_vendor(vendor_id_in VARCHAR2)
RETURN NUMBER IS
  total_products NUMBER := 0;
BEGIN
  -- Ki?m tra tham s? ??u v�o
  IF vendor_id_in IS NULL THEN
    RETURN total_products;
  END IF;

  -- Truy v?n v� t�nh to�n d? li?u
  SELECT SUM(Quantity)
  INTO total_products
  FROM Products
  WHERE VendorId = vendor_id_in;

  RETURN total_products;
END;

--- SELECT
SELECT VendorId, total_products_for_vendor(VendorId) AS TotalProducts
FROM Vendors;

--H�m ki?m tra xem m?t s?n ph?m c� ???c gi?m gi� hay kh�ng:
CREATE OR REPLACE FUNCTION is_product_discounted(product_id_in VARCHAR2)
RETURN VARCHAR2 IS
  discounted VARCHAR2(3) := 'No';
BEGIN
  -- Ki?m tra tham s? ??u v�o
  IF product_id_in IS NULL THEN
    RETURN discounted;
  END IF;

  -- Truy v?n v� ki?m tra t�nh to�n
  FOR rec IN (SELECT DiscountPercent FROM Products WHERE ProductId = product_id_in) LOOP
    IF rec.DiscountPercent > 0 THEN
      discounted := 'Yes';
      EXIT;
    END IF;
  END LOOP;

  RETURN discounted;
END;
-- G?i h�m is_product_discounted
SELECT ProductId, is_product_discounted(ProductId) AS IsDiscounted
FROM Products;
