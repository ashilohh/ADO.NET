CREATE DATABASE IF NOT EXISTS exo1;

USE exo1;
CREATE TABLE Livre(
	id INT AUTO_INCREMENT PRIMARY KEY,
    titre VARCHAR(255),
    auteur VARCHAR(255),
    anneePublication INT,
    isbn VARCHAR(255)
);



