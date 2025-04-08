import pandas as pd
from collections import Counter
import os

# === קלט מהמשתמש ===
file_name = input("הכנסי את שם הקובץ (לדוגמה: time_series.xlsx או time_series.parquet): ")
#"C:\Users\User\Desktop\Hadassim Home Test\Task_1\time_series.parquet"
full_path = fr"C:\Users\User\Desktop\Hadassim Home Test\Task_1\{file_name}"

# === קריאה דינמית של הקובץ בהתאם לסיומת ===
if file_name.endswith('.xlsx'):
    file = pd.read_excel(full_path)
    value_col = 'value'
elif file_name.endswith('.parquet'):
    file = pd.read_parquet(full_path)
    value_col = 'mean_value'
else:
    raise ValueError("סוג קובץ לא נתמך – השתמשי בקובץ .xlsx או .parquet")


# ======סעיף א#
# – תאריך עובר לתאריך תקין ===
file['timestamp'] = pd.to_datetime(file['timestamp'], errors='coerce')

# הסרת שורות עם תאריכים או ערכים חסרים
file = file.dropna(subset=['timestamp', value_col])

# המרת ערכים בעמודת value למספרים, שגויים ייהפכו ל-NaN
file[value_col] = pd.to_numeric(file[value_col], errors='coerce')

# בדיקת כפילויות
file = file.dropna(subset=[value_col])

duplicated_count = file.duplicated().sum()
file = file.drop_duplicates()
print(f"🔁 נמצאו והוסרו {duplicated_count} שורות כפולות.")

# === סעיף ב.1 – ממוצע לפי שעה ===
file['Hour'] = file['timestamp'].dt.strftime('%Y-%m-%d %H:00')
avg_by_hour = file.groupby('Hour')[value_col].mean()
print("\n📊 ממוצע ערכים לפי שעה:")
print(avg_by_hour)

# === סעיף ב.2 – פיצול לפי ימים ושמירה לקבצים ===
file['date'] = file['timestamp'].dt.date
unique_dates = file['date'].unique()

folder_path = r"C:\Users\User\Desktop\Hadassim Home Test\Task_1\split_files"
os.makedirs(folder_path, exist_ok=True)

for day in unique_dates:
    daily_data = file[file['date'] == day]
    daily_file_name = fr"{folder_path}\day_{day}.xlsx"
    daily_data.to_excel(daily_file_name, index=False)

# === ממוצע לפי שעה לכל יום ואיחוד התוצאות ===
all_averages = []

for fname in os.listdir(folder_path):
    if fname.endswith('.xlsx'):
        daily_file = pd.read_excel(os.path.join(folder_path, fname))
        daily_file['timestamp'] = pd.to_datetime(daily_file['timestamp'])
        daily_file[value_col] = pd.to_numeric(daily_file[value_col], errors='coerce')
        daily_file = daily_file.dropna(subset=['timestamp', value_col])

        daily_file['Hour'] = daily_file['timestamp'].dt.strftime('%Y-%m-%d %H:00')
        avg_by_hour = daily_file.groupby('Hour')[value_col].mean().reset_index()

        all_averages.append(avg_by_hour)

final_result = pd.concat(all_averages)
final_result = final_result.sort_values('Hour')

# שמירה לקובץ סופי
output_path = r"C:\Users\User\Desktop\Hadassim Home Test\Task_1\final_result.csv"
final_result.to_csv(output_path, index=False)

print("✅ הקובץ הסופי נוצר בהצלחה בשם final_result.csv")


# סעיף ב תרגיל 3
"""
אם הנתונים לא מגיעים בקובץ אחד אלא זורמים כל הזמן
שאי אפשר לחכות שכל המידע יגיע ורק אז לחשב את הממוצע.
במקום זה, צריך לחשב את הממוצעים תוך כדי שהמידע נכנס.

לפי ההיגיון שלי, הייתי שומרת בכל רגע
 את כמות הערכים שהגיעו בכל שעה ואת הסכום הכולל שלהם.
וכשיגיע עוד ערך חדש – 
נבדוק לאיזו שעה הוא שייך,
 נוסיף את הערך הזה לסכום של אותה שעה,
ונעלה את הספירה באחד.
ככה, נוכל לחשב ממוצע פשוט של סכום חלקי כמות,
 בלי לשמור את כל הנתונים עצמם – 
 רק את מה שצריך בשביל הממוצע.

ככה גם נוכל להציג ממוצעים שמתעדכנים בזמן אמת בלי להעמיס על הזיכרון.
"""

# סעיף ב תרגיל 4
"""
 תיעוד – תמיכה ב Parquet

1. תמיכה בסוגים  שונים:
בהתאם לדרישות התרגיל, עודכן הקוד כך שהוא מזהה את סוג הקובץ לפי הסיומת:
- אם הסיומת היא `.xlsx` → הקובץ נקרא באמצעות `pd.read_excel`
 והעמודה הרלוונטית היא `'value'`
- אם הסיומת היא `.parquet` → הקובץ נקרא באמצעות `pd.read_parquet`
והעמודה הרלוונטית היא `'mean_value'`

הקוד מתאים את עצמו אוטומטית כך שאין צורך לשנות ידנית את שם העמודה או את שיטת הקריאה.

2. יתרונות פורמט Parquet:
Parquet הוא פורמט עמודות (columnar format) 
דחוס ויעיל, שנועד לאחסון וניתוח של כמויות מידע גדולות.

- דחיסת מידע – קבצים קטנים יותר בהשוואה ל־Excel או CSV
- מהירות קריאה וכתיבה – קריאה ישירה רק של העמודות הדרושות
- יעילות בזיכרון – חוסך בזיכרון בעת טעינת קבצים גדולים

חסרון:
- לא ניתן לצפייה ישירה כמו Excel – דורש טעינה בעזרת קוד או כלים תומכים

3. סיכום:
----------
הקוד עודכן כך שהוא תומך בשני הפורמטים בצורה חלקה, תוך שמירה על גמישות, קריאות וניקיון קוד.
 המשתמשת לא צריכה לדעת מראש את מבנה הקובץ – הכל מטופל אוטומטית.

"""
