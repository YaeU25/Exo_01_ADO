CREATE DATABASE exo_01_ADO;

USE exo_01_ADO;

CREATE TABLE Livre (
    id INT AUTO_INCREMENT,
    titre VARCHAR(100),
    auteur VARCHAR(100),
    anneePublication int,
    isbn VARCHAR(255),
    PRIMARY KEY (id)
);

Select * From Livre;