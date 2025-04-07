
CREATE DATABASE FamilyTreeDB;

CREATE TABLE Persons (
    Person_Id INT PRIMARY KEY,
    Personal_Name VARCHAR(50),
    Family_Name VARCHAR(50),
    Gender VARCHAR(10) CHECK (Gender IN ('זכר', 'נקבה')),
    Father_Id INT,
    Mother_Id INT,
    Spouse_Id INT
);

CREATE TABLE Family (
    Person_Id INT,
    Relative_Id INT,
    Connection_Type VARCHAR(20)
);

INSERT INTO Persons (Person_Id, Personal_Name, Family_Name, Gender, Father_Id, Mother_Id, Spouse_Id) VALUES
(1, 'אברהם', 'בן תרח', 'זכר', NULL, NULL, 2),
(2, 'שרה', 'בת הרן', 'נקבה', NULL, NULL, 1),

(3, 'יצחק', 'בן אברהם', 'זכר', 1, 2, 6),
(4, 'ישמעאל', 'בן אברהם', 'זכר', 1, NULL, NULL), 

(6, 'רבקה', 'בת בתואל', 'נקבה', NULL, NULL, 3),

(7, 'יעקב', 'בן יצחק', 'זכר', 3, 6, 10),
(8, 'עשיו', 'בן יצחק', 'זכר', 3, 6, NULL),

(10, 'רחל', 'בת לבן', 'נקבה', NULL, NULL, 7),

(11, 'ראובן', 'בן יעקב', 'זכר', 7, 10, NULL),
(12, 'שמעון', 'בן יעקב', 'זכר', 7, 10, NULL);

select *
from [dbo].[Persons]

SELECT * FROM Family

--תרגיל‌ 1  הקמת עץ משפחה======================👨🏻👨🏻‍🦰👨🏻‍🦱👨🏻‍🦳👨🏻‍🦲👱🏻‍👴🏻
INSERT INTO Family (Person_Id, Relative_Id, Connection_Type)
SELECT Person_Id, Father_Id, 'אב'
FROM Persons
WHERE Father_Id IS NOT NULL;

INSERT INTO Family(Person_Id, Relative_Id, Connection_Type)
SELECT Person_Id, Mother_Id, 'אם'
FROM Persons
WHERE Mother_Id IS NOT NULL;

INSERT INTO Family(Person_Id, Relative_Id, Connection_Type)
SELECT Person_Id, Spouse_Id,
       CASE WHEN Gender = 'זכר' THEN 'בת זוג' ELSE 'בן זוג' END
FROM Persons
WHERE Spouse_Id IS NOT NULL;

INSERT INTO Family(Person_Id, Relative_Id, Connection_Type)
SELECT P.Father_Id, P.Person_Id,
       CASE WHEN P.Gender = 'זכר' THEN 'בן' ELSE 'בת' END
FROM Persons P
WHERE P.Father_Id IS NOT NULL;

INSERT INTO Family (Person_Id, Relative_Id, Connection_Type)
SELECT P.Mother_Id, P.Person_Id,
       CASE WHEN P.Gender = 'זכר' THEN 'בן' ELSE 'בת' END
FROM Persons P
WHERE P.Mother_Id IS NOT NULL;

INSERT INTO Family (Person_Id, Relative_Id, Connection_Type)
SELECT P1.Person_Id, P2.Person_Id,
       CASE WHEN P2.Gender = 'זכר' THEN 'אח' ELSE 'אחות' END
FROM Persons P1
JOIN Persons P2
  ON P1.Father_Id = P2.Father_Id
 AND P1.Mother_Id = P2.Mother_Id
 AND P1.Person_Id <> P2.Person_Id;


 --כאן אני מריצה לראות את הטבלה שלי עם שם וקוד
 SELECT 
    CAST(F.Person_Id AS VARCHAR) + ' ~ ' + P1.Personal_Name AS Person,
	F.Connection_Type,
    CAST(F.Relative_Id AS VARCHAR) + ' ~ ' + P2.Personal_Name AS Relative
FROM Family F
JOIN Persons P1 ON F.Person_Id = P1.Person_Id
JOIN Persons P2 ON F.Relative_Id = P2.Person_Id
ORDER BY F.Person_Id, F.Connection_Type;


--תרגי‌ל 2  השלמת בני / ב‌נות ז‌וג======================👨🏻👨🏻‍🦰👨🏻‍🦱👨🏻‍🦳👨🏻‍🦲👱🏻‍👴🏻
INSERT INTO Persons (Person_Id, Personal_Name, Family_Name, Gender, Father_Id, Mother_Id, Spouse_Id)
VALUES (13, 'זלפה', 'בת לבן', 'נקבה', NULL, NULL, 11); 


INSERT INTO Family (Person_Id, Relative_Id, Connection_Type)
SELECT 
    P2.Person_Id,         
    P1.Person_Id,         
    CASE 
        WHEN P2.Gender = 'זכר' THEN 'בת זוג'
        ELSE 'בן זוג'
    END
FROM Persons P1, Persons P2
WHERE P1.Spouse_Id = P2.Person_Id   
  AND P2.Person_Id NOT IN (        
      SELECT Person_Id
      FROM Family
      WHERE Relative_Id = P1.Person_Id
        AND Connection_Type IN ('בן זוג', 'בת זוג')
  );

  DELETE FROM Family;

