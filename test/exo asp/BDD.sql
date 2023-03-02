--Todolist :
--id
--users
--task
--dates

CREATE TABLE Task
(
	id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	users VARCHAR(50),
	task VARCHAR(200) NOT NULL,
	is_checked BOOLEAN NOT NULL,
	dates VARCHAR(10)
);

INSERT INTO Task
(
	users,
	task,
	is_checked,
	dates
)
VALUES
(
	'',
	'',
	false,
	''
);

UPDATE Task SET users = '', task = '', dates = '' WHERE id = '';

DELETE FROM Task WHERE id = '';
DELETE FROM Task;