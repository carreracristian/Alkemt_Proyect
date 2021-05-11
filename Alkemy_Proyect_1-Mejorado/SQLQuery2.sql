ALTER TABLE Inscriptions NOCHECK CONSTRAINT Id_student;

select * from Inscriptions

insert into Inscriptions(Id_student,Id_subject) values('7','1')


ALTER TABLE Inscriptions DROP CONSTRAINT Id_subject

ALTER TABLE ADD FOREIGN KEY (Id_subject) REFERENCES Subject(Id)

ALTER TABLE Inscriptions DROP Id_student
use Alkemy_Proyect

ALTER TABLE Users
ADD
FOREIGN KEY (Id_rol) REFERENCES Roles(Id);

ALTER TABLE Inscriptions
DROP CONSTRAINT FK__Inscripti__Id_su__48CFD27E

SELECT *
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_NAME = 'Users'

delete from Inscriptions

CREATE TABLE Roles
(
Id int IDENTITY(1,1) PRIMARY KEY,
      Name varchar(50),
);

alter table Rol drop Id;

