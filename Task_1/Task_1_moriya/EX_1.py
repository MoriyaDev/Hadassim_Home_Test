#=================סעיף 1
from typing import final

import pandas as pd
from collections import Counter

file_path = input("הכנסי את שם הקובץ (logs.txt.xlsx): ")
n = int(input("הכנסי מספר (N): "))
t = int(input("הכנסי מספר (t): "))

full_path = fr"C:\Users\User\Desktop\Hadassim Home Test\Task_1\{file_path}"
log = pd.read_excel(full_path, header=None)
log['error_code'] = log[0].str.extract(r'Error: (\w+_\d+)')

#1==קטע קוד לפיצול קובץ ה-logs.txt לחלקים קטנים יותר
rows_log = len(log)
row_chunks = rows_log //t
#chunks=נתח
for i in range(t):
    start = i * row_chunks
    end = (i + 1) * row_chunks if i < n - 1 else rows_log
    chunk = log[start:end]

    filename = f"log_part_{i + 1}.xlsx"
    chunk.to_excel(filename, index=False, header=False)

    print(f"שמרתי את {filename} עם שורות {start} עד {end - 1}")

#2==השכיחות של כל קוד שגיאה
def count_errors(file):
    my_file = pd.read_excel(file, header=None)
    error_codes = my_file[0].str.extract(r'Error: (\w+_\d+)')[0]
    return Counter(error_codes)

file_names = [f"log_part_{i + 1}.xlsx" for i in range(n)]


total_counter = Counter()

for file in file_names:
    print(f"\nשגיאות בקובץ: {file}")
    file_counter = count_errors(file)
    for code, count in file_counter.items():
        print(f"{code}: {count}")
    total_counter += file_counter



#3==ספירות השכיחות מכל החלקים
print("ספירה של כל קודי השגיאות:")
for code, count in total_counter.items():
    print(f"{code}: {count}")

#4==N קודי השגיאה השכיחים ביותר מהספירות הממוזגות
top_n = total_counter.most_common(n)
print("================================")
print(f"==  {n} השגיאות השכיחות ביותר:  ==")
for i, (code, count) in enumerate(top_n, start=1):
    print(f"==     {i}. {code}: {count}   ==")
print("================================")

#5==N 5.	נתחי את סיבוכיות הזמן והמקום של הפתרון שלך
"""
זמן הריצה שלי הוא דינמי לפי מספר השורות שלי M
M=שורות
U=קודי שגיאה
K=מספר קבצים
N=שחיכים

1. פיצול ושמירה
עוברים על מיליון שורות ושומרים בקבצים.
זמן ריצה: O(M)
(כל שורה נכתבת פעם אחת)

2. קריאת כל הקבצים
טוענים את כל הקבצים ומחפשים קודי שגיאה.

זמן ריצה: O(M)
(כל שורה נקראת ונבדקת פעם אחת)

3. מיזוג הספירות
מחברים את הספירות מכל קובץ.

זמן ריצה: O(k * U)
אבל k קטן (~5 קבצים), U קטן (5~ קודים) כלומר O(1)

4. מיון למציאת השכיחים
מוצאים את N הקודים הכי נפוצים.

זמן ריצה: O(U log n)
אבל U ו־n קטנים ⇒ בפועל O(1)
"""