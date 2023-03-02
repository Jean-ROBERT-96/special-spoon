CREATE TABLE headquarters
(
	id_headquarters INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	name VARCHAR(30) NOT NULL
);

CREATE TABLE services
(
	id_services INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	name VARCHAR(30) NOT NULL
);

CREATE TABLE salaryman
(
	id_salary INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	image_link TEXT NOT NULL,
	firstName VARCHAR(40) NOT NULL,
	lastName VARCHAR(40) NOT NULL,
	gender CHAR(1) NOT NULL,
	phone VARCHAR(15),
	mobile_phone VARCHAR(15) NOT NULL,
	mail VARCHAR(50) NOT NULL,
	id_headquarters INT NOT NULL,
	id_services INT NOT NULL,
	FOREIGN KEY (id_headquarters) REFERENCES headquarters(id_headquarters),
	FOREIGN KEY (id_services) REFERENCES services(id_services)
);

INSERT INTO headquarters
(
	name
)
VALUES
(
	'Bordeaux'
),
(
	'Toulouse'
),
(
	'Lyon'
),
(
	'Paris'
);

INSERT INTO services
(
	name
)
VALUES
(
	'Informatique'
),
(
	'Comptabilité'
),
(
	'Vente'
),
(
	'Direction'
);

INSERT INTO salaryman
(
	image_link,
	firstName,
	lastName,
	gender,
	mobile_phone,
	mail,
	id_headquarters,
	id_services
)
VALUES
(
	'https://tiermaker.com/images/templates/overwatch-2-designs-486615/4866151626673279.png',
	'Albi',
	'Dautaj',
	'M',
	'0610101010',
	'dautaj@cesi.fr',
	1,
	3
),
(
	'https://jf-staeulalia.pt/img/other/76/collection-epic-face-background-18.png',
	'Jean',
	'Robert',
	'M',
	'0610101010',
	'robert@cesi.fr',
	1,
	1
),
(
	'https://jf-staeulalia.pt/img/other/76/collection-epic-face-background-18.png',
	'Théo',
	'Normand',
	'M',
	'0610101010',
	'normand@cesi.fr',
	4,
	2
),
(
	'https://jf-staeulalia.pt/img/other/76/collection-epic-face-background-18.png',
	'Alexandre',
	'Clain',
	'M',
	'0610101010',
	'aclain@cesi.fr',
	1,
	4
),
(
	'https://tiermaker.com/images/templates/overwatch-2-designs-486615/4866151626673279.png',
	'Lorem',
	'Ipsum',
	'M',
	'0610101010',
	'dautaj@cesi.fr',
	3,
	3
);

INSERT INTO salaryman(image_link, firstName, lastName, gender, phone, mobile_phone, mail, id_headquarters, id_services) VALUES ('', '', '', '', '', '', , );
UPDATE OR REPLACE salaryman SET image_link = '', firstName = '', lastName = '', gender = '', phone = '', mobile_phone = '', mail = '', id_headquarters = '', id_services = '' WHERE id_salary = '';
CREATE TABLE administrateur
(
	id_admin INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	userid VARCHAR(50) NOT NULL,
	pass VARCHAR(50) NOT NULL
);