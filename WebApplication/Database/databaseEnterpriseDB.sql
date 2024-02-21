IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Enterprise')
BEGIN
    CREATE DATABASE Enterprise;
    PRINT 'Base de datos creada con éxito.';
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe.';
END