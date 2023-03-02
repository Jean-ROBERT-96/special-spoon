CREATE TABLE Client(
        Id         Int PRIMARY KEY Auto_increment  NOT NULL ,
        Nom        Varchar (50) NOT NULL ,
        Prenom     Varchar (50) NOT NULL ,
        Telephone  Varchar (15) NOT NULL ,
        Adresse    Varchar (200) NOT NULL ,
        Complement Varchar (200) NOT NULL ,
        CodePostal Int NOT NULL ,
        Ville      Varchar (50) NOT NULL
);

CREATE TABLE Vendor(
        Id         Int PRIMARY KEY Auto_increment  NOT NULL ,
        Societe    Varchar (50) NOT NULL ,
        Telephone  Varchar (15) NOT NULL ,
        Adresse    Varchar (200) NOT NULL ,
        Complement Varchar (200) NOT NULL ,
        CodePostal Int NOT NULL ,
        Ville      Varchar (50) NOT NULL ,
        Mail       Varchar (50) NOT NULL
);

CREATE TABLE Family(
        Id          Int PRIMARY KEY Auto_increment  NOT NULL ,
        Type        Varchar (30) NOT NULL ,
        Description Varchar (30) NOT NULL
);

CREATE TABLE Article(
        Id          Int PRIMARY KEY Auto_increment  NOT NULL ,
        Nom         Varchar (50) NOT NULL ,
        Image       Varchar (255) NOT NULL ,
        Annee       Varchar (4) NOT NULL ,
        Description Varchar (255) NOT NULL ,
        Prix        Float NOT NULL ,
        Stock       Bool NOT NULL ,
        Quantite    Int NOT NULL ,
        Id_Family   Int NOT NULL,
		FOREIGN KEY (Id_Family) REFERENCES Family(Id)
);

CREATE TABLE Facture(
        Id        Int PRIMARY KEY Auto_increment  NOT NULL ,
        Somme     Float NOT NULL ,
        Date      Varchar (10) NOT NULL ,
        Id_Client Int NOT NULL,
		FOREIGN KEY (Id_Client) REFERENCES Client(Id)
);

CREATE TABLE User(
        Id        Int PRIMARY KEY Auto_increment  NOT NULL ,
        Mail      Varchar (50) NOT NULL ,
        Password  Varchar (50) NOT NULL ,
        Id_Client Int NOT NULL,
		FOREIGN KEY (Id_Client) REFERENCES Client(Id)
);

CREATE TABLE Ajouter(
        Id       Int NOT NULL ,
        Id_User  Int NOT NULL ,
        Quantite Int NOT NULL
	,CONSTRAINT Ajouter_PK PRIMARY KEY (Id,Id_User)

	,CONSTRAINT Ajouter_Article_FK FOREIGN KEY (Id) REFERENCES Article(Id)
	,CONSTRAINT Ajouter_User0_FK FOREIGN KEY (Id_User) REFERENCES User(Id)
);

CREATE TABLE Contient(
        Id         Int NOT NULL ,
        Id_Facture Int NOT NULL ,
        Quantite   Int NOT NULL
	,CONSTRAINT Contient_PK PRIMARY KEY (Id,Id_Facture)

	,CONSTRAINT Contient_Article_FK FOREIGN KEY (Id) REFERENCES Article(Id)
	,CONSTRAINT Contient_Facture0_FK FOREIGN KEY (Id_Facture) REFERENCES Facture(Id)
);

CREATE TABLE Fournir(
        Id         Int NOT NULL ,
        Id_Article Int NOT NULL ,
        Quantite   Int NOT NULL ,
        Date       Varchar (10) NOT NULL
	,CONSTRAINT Fournir_PK PRIMARY KEY (Id,Id_Article)

	,CONSTRAINT Fournir_Vendor_FK FOREIGN KEY (Id) REFERENCES Vendor(Id)
	,CONSTRAINT Fournir_Article0_FK FOREIGN KEY (Id_Article) REFERENCES Article(Id)
);
