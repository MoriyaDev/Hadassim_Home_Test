# משימה 2 – עץ משפחה ב-SQL 👨‍👩‍👦

במשימה זו יצרתי מסד נתונים עם טבלת אנשים וטבלת קשרים משפחתיים.
כל אדם מחובר לקרוביו בדרגת קרבה ראשונה:
- אבא, אמא
- בן זוג/בת זוג
- בן, בת
- אח, אחות

## 🗂 הקבצים:
- `FamilyTreeEX.sql` – קובץ SQL עם יצירת טבלאות והזנת נתונים
- `DataTables.csv` – תצוגת נתונים לדוגמה מתוך הטבלאות
- תמונת דוגמה של עץ המשפחה

## מבנה מסד הנתונים

### טבלת אנשים (People)
טבלה זו מכילה מידע בסיסי על כל אדם:
- מזהה ייחודי (ID)
- שם פרטי (FirstName)
- שם משפחה (LastName)
- תאריך לידה (BirthDate)
- מגדר (Gender)
- מידע נוסף (AdditionalInfo)

### טבלת קשרים (Relationships)
טבלה זו מתארת את הקשרים המשפחתיים בין אנשים:
- מזהה ייחודי לקשר (RelationshipID)
- מזהה אדם ראשון (Person1ID)
- מזהה אדם שני (Person2ID)
- סוג הקשר (RelationType): אב/אם, בן/בת, אח/אחות, בן זוג/בת זוג

## שאילתות לדוגמה

### שליפת כל הילדים של אדם מסוים
```sql
SELECT p.FirstName, p.LastName
FROM People p
JOIN Relationships r ON p.ID = r.Person2ID
WHERE r.Person1ID = [מזהה_האדם] AND r.RelationType IN ('son', 'daughter');
```

### שליפת ההורים של אדם מסוים
```sql
SELECT p.FirstName, p.LastName
FROM People p
JOIN Relationships r ON p.ID = r.Person1ID
WHERE r.Person2ID = [מזהה_האדם] AND r.RelationType IN ('father', 'mother');
```

### שליפת האחים והאחיות של אדם מסוים
```sql
SELECT p.FirstName, p.LastName
FROM People p
JOIN Relationships r ON p.ID = r.Person2ID
JOIN Relationships r2 ON r2.Person1ID = r.Person1ID
WHERE r2.Person2ID = [מזהה_האדם] 
AND r.Person2ID != [מזהה_האדם]
AND r.RelationType IN ('son', 'daughter')
AND r2.RelationType IN ('son', 'daughter');
```

## הרחבות אפשריות
- הוספת תאריכי פטירה
- הוספת מיקום גיאוגרפי
- הוספת קשרים מורכבים יותר (דוד/דודה, סבא/סבתא)
- יצירת פונקציות SQL לחישוב שושלות ודורות

## התקנה והפעלה
1. הורד את קובץ `FamilyTreeEX.sql`
2. הרץ את הקובץ בשרת SQL שלך
3. השתמש בשאילתות לדוגמה כדי לחקור את עץ המשפחה
