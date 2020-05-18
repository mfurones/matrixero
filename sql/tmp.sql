-- store procedure para listar DB
Sp_databases
Go

-- verifica si esta habilitado el ingreso por usr y pass (valor 0) o autenticacion windows (1)
SELECT SERVERPROPERTY('IsIntegratedSecurityOnly')
GO


-- habilita el usuario sa (por defecto) y modificamos el password
ALTER LOGIN sa ENABLE ;  
GO  
ALTER LOGIN sa WITH PASSWORD = '#matrixero@123' ;  
GO 



-- Crear usuario

CREATE LOGIN matrixeroAdmin WITH PASSWORD = '#matrixero@123'
GO


IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'NewAdminName')
BEGIN
    CREATE USER [NewAdminName] FOR LOGIN [NewAdminName]
    EXEC sp_addrolemember N'db_owner', N'NewAdminName'
END;
GO


USE [master];
GO
CREATE LOGIN matrixeroAdmin 
    WITH PASSWORD    = N'matrixero@123',
    CHECK_POLICY     = OFF,
    CHECK_EXPIRATION = OFF;
GO
EXEC sp_addsrvrolemember 
    @loginame = N'matrixeroAdmin', 
    @rolename = N'sysadmin';


EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2

