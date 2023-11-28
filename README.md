# SAE-S5-Back

TODO : Write quick doc

BDD : bde

CREATE TABLE Membres(
id_membre INT NOT NULL AUTO_INCREMENT,
first_name VARCHAR(40),
last_name VARCHAR(40),
mail_upjv VARCHAR(60),
discord_username VARCHAR(40),
birth_date DATE,
pp_image_url VARCHAR(100),
description VARCHAR(200),
password VARCHAR(50),
statut VARCHAR(40),
PRIMARY KEY(id_membre)
);

CREATE TABLE Produits(
id_produit INT NOT NULL AUTO_INCREMENT,
nom VARCHAR(40),
description VARCHAR(150),
image_url VARCHAR(100),
prix_adherent DOUBLE,
prix_non_adherent DOUBLE,
stock INT,
categorie VARCHAR(40),
prix_fournisseur DOUBLE,
PRIMARY KEY(id_produit)
);

CREATE TABLE Events(
id_event INT NOT NULL AUTO_INCREMENT,
nom VARCHAR(60),
date_debut DATETIME,
date_fin DATETIME,
short_description VARCHAR(140),
long_description VARCHAR(200),
auteur VARCHAR(50),
nb_places_dispo INT,
nb_place_restantes INT,
image_url VARCHAR(140),
date_creation DATE,
lieu VARCHAR(40),
PRIMARY KEY(id_event)
);

CREATE TABLE Event_inscription(
id_event_inscription INT NOT NULL AUTO_INCREMENT,
statut_paiement BOOLEAN,
id_membre INT NOT NULL,
id_event INT NOT NULL,
PRIMARY KEY(id_event_inscription),
FOREIGN KEY(id_membre) REFERENCES Membres(id_membre),
FOREIGN KEY(id_event) REFERENCES Events(id_event)
);

CREATE TABLE Actualités(
id_actu INT NOT NULL AUTO_INCREMENT,
nom VARCHAR(60),
short_description VARCHAR(100),
long_description VARCHAR(200),
auteur VARCHAR(50),
image_url VARCHAR(140),
date_creation DATE,
PRIMARY KEY(id_actu)
);

CREATE TABLE Paiement_reussi(
id_paiement INT NOT NULL AUTO_INCREMENT,
json_du_paiement MEDIUMTEXT,
PRIMARY KEY(id_paiement)
);

CREATE TABLE contenu(
id_content INT NOT NULL AUTO_INCREMENT,
name_space VARCHAR(255),
content MEDIUMTEXT,
PRIMARY KEY(id_content),
UNIQUE(name_space)
);