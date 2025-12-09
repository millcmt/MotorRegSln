Implementation Instructions
GitHub repository URL: https://github.com/millcmt/MotorRegSln 

Database Creation and Seeding Scripts:
INSERT INTO Users (FullName, Username, Password, Role)
VALUES 
('System Administrator', 'admin', 'admin123', 'Admin'),
('John Brown', 'john', 'password123', 'User');

CREATE DATABASE MotorRegDB;
GO
USE MotorRegDB;
GO
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(150) NOT NULL,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'User'
);
CREATE TABLE Vehicles (
    VehicleId INT IDENTITY(1,1) PRIMARY KEY,
    PlateNumber NVARCHAR(20) NOT NULL UNIQUE,
    ChassisNumber NVARCHAR(50) NOT NULL UNIQUE,
    Make NVARCHAR(100) NOT NULL,
    Model NVARCHAR(100) NOT NULL,
    Year INT NOT NULL,
    OwnerUserId INT NULL,

    CONSTRAINT FK_Vehicles_Users 
        FOREIGN KEY (OwnerUserId) REFERENCES Users(UserId)
);
CREATE TABLE Registrations (
    RegistrationId INT IDENTITY(1,1) PRIMARY KEY,
    VehicleId INT NOT NULL,
    StartDate DATE NOT NULL,
    ExpiryDate DATE NOT NULL,
    DurationMonths INT NOT NULL,
    CreatedByUserId INT NOT NULL,

    CONSTRAINT FK_Registrations_Vehicles 
        FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId),

    CONSTRAINT FK_Registrations_Users 
        FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId)
);
CREATE TABLE InsuranceRecords (
    InsuranceId INT IDENTITY(1,1) PRIMARY KEY,
    VehicleId INT NOT NULL,
    ValidUntil DATE NOT NULL,
    LastChecked DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Insurance_Vehicles 
        FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId)
);
CREATE TABLE FitnessRecords (
    FitnessId INT IDENTITY(1,1) PRIMARY KEY,
    VehicleId INT NOT NULL,
    ValidUntil DATE NOT NULL,
    LastChecked DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Fitness_Vehicles 
        FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId)
);

INSERT INTO Users (FullName, Username, Password, Role)
VALUES 
('System Administrator', 'admin', 'admin123', 'Admin'),
('John Brown', 'john', 'password123', 'User');
INSERT INTO Vehicles (PlateNumber, ChassisNumber, Make, Model, Year, OwnerUserId)
VALUES
('JAM-1234', 'CHASSIS-1001', 'Toyota', 'Corolla', 2018, 2),
('JAM-5678', 'CHASSIS-2002', 'Honda', 'Civic', 2020, NULL),
('JAM-9101', 'CHASSIS-3003', 'Nissan', 'Tiida', 2017, NULL);
INSERT INTO Registrations (VehicleId, StartDate, ExpiryDate, DurationMonths, CreatedByUserId)
VALUES
(1, '2024-01-01', '2025-01-01', 12, 2),
(1, '2023-01-01', '2024-01-01', 12, 2);
INSERT INTO InsuranceRecords (VehicleId, ValidUntil)
VALUES
(1, '2025-12-31'),
(2, '2024-06-30'),
(3, '2023-01-15');
INSERT INTO FitnessRecords (VehicleId, ValidUntil)
VALUES
(1, '2025-11-30'),
(2, '2024-04-30'),
(3, '2022-12-10');


Username and Password
Admin Information
Username: admin
Password: admin123
User Information
		Username: john
Password: password123
