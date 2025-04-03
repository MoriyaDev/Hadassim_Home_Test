import pandas as pd

file= pd.read_csv("time_series.csv")

try:
    file['Timestamp']=pd.to_datetime(['Timestamp'])
except Exception as e:
    print("שגיאה באיך שהתאריך כתוב")

original_length = len(file)
file = file.drop_duplicates()
new_length = len(file)
print(f"🧹 הוסרו {original_length - new_length} כפילויות.")

err=0
