CREATE DATABASE IF NOT EXISTS tododb;

GRANT ALL PRIVILEGES ON tododb.* TO 'user';

FLUSH PRIVILEGES;

USE tododb;

CREATE TABLE tododb.ToDo (Id int NOT NULL AUTO_INCREMENT, Title varchar(100) NOT NULL, ExpiryDate datetime(6) NOT NULL, Description varchar(1000) NOT NULL, CompletePercentage int NOT NULL, PRIMARY KEY (Id)) 
ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(1, '2025-12-12 10:00:00', "Another Todo task", "Do something", 50);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(2, '2025-12-13 10:00:00', "Second Todo task", "Do something 2", 40);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(3, '2025-12-14 20:00:00', "Third Todo task", "Do something 3", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(4, '2025-12-15 20:00:00', "Another Todo task 4", "Do something 4", 100);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(5, '2025-12-15 20:00:00', "Another Todo task 5", "Do something 5", 10);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(6, '2025-12-16 20:00:00', "Another Todo task 6", "Do something 6", 5);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(7, '2025-12-16 20:00:00', "Another Todo task 7", "Do something 7", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(8, '2025-12-16 10:00:00', "Another Todo task 8", "Do something 8", 5);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(9, '2025-12-15 10:00:00', "Another Todo task 9", "Do something 9", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(10, '2025-12-29 10:00:00', "Another Todo task 10" , "Do something 10", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(11, '2025-12-20 15:00:00', "Another Todo task 11" , "Do something 11", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(12, '2025-12-22 20:00:00', "Another Todo task 12" , "Do something 12", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(13, '2025-12-29 10:00:00', "Another Todo task 13" , "Do something 13", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(14, '2025-12-20 10:00:00', "Another Todo task 14" , "Do something 14", 25);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(15, '2025-12-15 12:00:00', "Another Todo task 15" , "Do something 15", 50);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(16, '2025-12-25 10:00:00', "Another Todo task 16" , "Do something 16", 0);
INSERT INTO tododb.ToDo(Id, ExpiryDate, Title, Description, CompletePercentage) VALUES(17, '2026-01-10 10:00:00', "Another Todo task 17" , "Do something 17", 100);