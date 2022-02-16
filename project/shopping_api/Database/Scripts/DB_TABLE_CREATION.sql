--=========================================================
--				TABLE CREATION SCRIPT 					  
--=========================================================

-----------------------------------------------------------
--						Brand
-----------------------------------------------------------

CREATE TABLE TB_BRAND (
	BRN_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	BRN_CODE TEXT(30) NOT NULL,
	BRN_NAME TEXT(30) NOT NULL
)

-----------------------------------------------------------
--						Coupon
-----------------------------------------------------------

CREATE TABLE TB_COUPON (
	CPN_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	CPN_CODE TEXT(10) NOT NULL,
	CPN_DESCRIPTION TEXT(30) NOT NULL,
	CPN_DISCOUNT DECIMAL(10, 2) NOT NULL DEFAULT 0
)

-----------------------------------------------------------
--						Department
-----------------------------------------------------------

CREATE TABLE TB_DEPARTMENT (
	DPR_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	DPR_NAME TEXT(50) NOT NULL
)

-----------------------------------------------------------
--						User
-----------------------------------------------------------

CREATE TABLE TB_USER (
	USR_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	USR_USERNAME TEXT(20) NOT NULL,
	USR_NAME TEXT(30) NOT NULL
)

-----------------------------------------------------------
--						Product
-----------------------------------------------------------

CREATE TABLE TB_PRODUCT (
	PRD_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	PRD_CODE TEXT(30) NOT NULL,
	PRD_NAME TEXT(200) NOT NULL,
	PRD_PRICE DECIMAL(10, 2) NOT NULL DEFAULT 0,
	PRD_STOCK INTEGER NOT NULL DEFAULT 0,
	BRN_ID INTEGER NOT NULL,
	DPR_ID INTEGER NOT NULL,
	FOREIGN KEY(BRN_ID) REFERENCES TB_BRAND(BRN_ID),
	FOREIGN KEY(DPR_ID) REFERENCES TB_DEPARTMENT (DPR_ID)
)

-----------------------------------------------------------
--						ProductCart
-----------------------------------------------------------

CREATE TABLE TB_CART (
	CRT_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	CRT_SUBTOTAL DECIMAL(10, 2) NOT NULL DEFAULT 0,
	CRT_DISCOUNT DECIMAL(10, 2) NOT NULL DEFAULT 0,
	CRT_SHIPPING DECIMAL(10, 2) NOT NULL DEFAULT 0,
	CRT_TOTAL DECIMAL(10, 2) NOT NULL DEFAULT 0,
	CRT_CREATED_AT DATETIME DEFAULT (DATETIME('NOW', 'LOCALTIME')),
	USR_ID INTEGER NOT NULL,
	CPN_ID INTEGER NULL,
	FOREIGN KEY(USR_ID) REFERENCES TB_USER(USR_ID),
	FOREIGN KEY(CPN_ID) REFERENCES TB_COUPON(CPN_ID)
)

-----------------------------------------------------------
--						Brand
-----------------------------------------------------------

CREATE TABLE TB_PRODUCT_CART (
	PRC_ID INTEGER PRIMARY KEY AUTOINCREMENT,
	PRC_PRICE DECIMAL(10, 2) NOT NULL DEFAULT 0,
	PRC_QUANTITY INTEGER NOT NULL DEFAULT 1,
	PRC_TOTAL DECIMAL(10, 2) NOT NULL DEFAULT 0,
	PRC_ADDED_AT DATETIME DEFAULT (DATETIME('NOW', 'LOCALTIME')),
	CRT_ID INTEGER NOT NULL,
	PRD_ID INTEGER NOT NULL,
	FOREIGN KEY(CRT_ID) REFERENCES TB_CART(CRT_ID),
	FOREIGN KEY(PRD_ID) REFERENCES TB_PRODUCT(PRD_ID)
)

--=========================================================