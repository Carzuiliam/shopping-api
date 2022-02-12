--================== TABLE DATA FEEDING ===================

INSERT INTO TB_BRAND
	(BRN_CODE, BRN_NAME)
VALUES 
	('00000', '(unknown)'),
	('AB123', 'Carzuilha Inc.')

INSERT INTO TB_DEPARTMENT 
	(DPR_NAME)
VALUES 
	('(unknown)'),
	('Electronics')

INSERT INTO TB_USER 
	(USR_USERNAME, USR_NAME)
VALUES
	('carzuiliam', 'Carlos Carvalho'),
	('guest', 'Guest')

INSERT INTO TB_COUPON
	(CPN_CODE, CPN_DESCRIPTION, CPN_DISCOUNT)
VALUES 
	('CUPOMMANHOSO', '5% discount', 0.05),
	('LOJAINTEGRADA', '10% discount', 0.1)
	
INSERT INTO TB_PRODUCT 
	(PRD_CODE, PRD_NAME, PRD_PRICE, PRD_STOCK, BRN_ID, DPR_ID)
VALUES 
	('PRD---', 'Mysterious Product', 49.90, 10, 1, 1),
	('PRD000', 'Something Beautiful', 79.90, 10, 2, 1),
	('PRDAAA', 'Unknown Thing', 139.90, 10, 10, 1),
	('PRD100', 'Marvelous Token', 429.90, 10, 2, 2),
	('PRDXXX', 'Strange Object', 699.90, 10, 1, 2),
	('PRD999', 'Bizarre Device', 1000.00, 10, 2, 2)

--=========================================================