CREATE DATABASE IF NOT EXISTS Exo2;
USE Exo2;

CREATE TABLE IF NOT EXISTS Clients(
		id INT AUTO_INCREMENT PRIMARY KEY,
        nom VARCHAR(100),
        prenom VARCHAR(100),
        adresse VARCHAR(100),
        code_postal VARCHAR(100),
        ville VARCHAR(100),
        telephone VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS Commandes(
	id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    date_  DATE NOT NULL,
    montant DECIMAL NOT NULL,
    client_id INT NOT NULL,
    CONSTRAINT fk_commande_client FOREIGN KEY (client_id) REFERENCES Clients(id)
);


