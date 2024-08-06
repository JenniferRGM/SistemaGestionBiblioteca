USE BibliotecaBD

-- Crear la tabla Libros
CREATE TABLE Libros (
    ISBN VARCHAR(13) PRIMARY KEY,
    Titulo VARCHAR(100) NOT NULL,
    Autor VARCHAR(100) NOT NULL,
    AñoPublicacion INT NOT NULL,
    NumeroPaginas INT NOT NULL
);

-- Crear la tabla Usuarios
CREATE TABLE Usuarios (
    NumeroSocio INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL
);

-- Crear la tabla Prestamos
CREATE TABLE Prestamos (
    PrestamoID INT PRIMARY KEY IDENTITY,
    ISBN VARCHAR(13) NOT NULL,
    NumeroSocio INT NOT NULL,
    FechaPrestamo DATETIME NOT NULL,
    FechaDevolucion DATETIME,
    FOREIGN KEY (ISBN) REFERENCES Libros(ISBN),
    FOREIGN KEY (NumeroSocio) REFERENCES Usuarios(NumeroSocio)
);
