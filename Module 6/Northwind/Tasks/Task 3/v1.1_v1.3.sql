﻿IF NOT EXISTS (SELECT * FROM SYS.TABLES 
	WHERE OBJECT_ID = OBJECT_ID(N'dbo.Regions') AND TYPE IN (N'U'))
BEGIN
EXECUTE SP_RENAME '[dbo].[Region]', 'Regions';
END;

IF NOT EXISTS (SELECT * FROM sys.columns  
	WHERE Name = 'DateOfSettingUp' AND OBJECT_ID = OBJECT_ID(N'dbo.Customers'))
BEGIN
ALTER TABLE Customers
	ADD DateOfSettingUp DATE NULL;
END;