import sqlite3

DB_CONNECTION='logs.db'

"""
    Set up the datastore.
"""
def migrate():
    with sqlite3.connect(DB_CONNECTION) as conn:
        cursor = conn.cursor()
        cursor.execute('''
            CREATE TABLE IF NOT EXISTS logs (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT,
                datetime DATETIME,
                type TEXT,
                message TEXT
            )
        ''')
        conn.commit()

"""
    Write a log to the DB.
"""
def write_log(name: str, type: str, message: str):
    with sqlite3.connect(DB_CONNECTION) as conn:
        cursor = conn.cursor()
        cursor.execute('''
            INSERT INTO logs (name, datetime, type, message)
            VALUES (?, datetime('now'), ?, ?)
        ''', (name.lower(), type, message))
        conn.commit()

"""
    Read the logs under a specific name (agent)
"""
def read_log(name: str, last_n=10):
    with sqlite3.connect(DB_CONNECTION) as conn:
        cursor = conn.cursor()
        cursor.execute('''
            SELECT datetime, type, message FROM logs 
            WHERE name = ? 
            ORDER BY datetime DESC
            LIMIT ?
        ''', (name.lower(), last_n))
        
        return reversed(cursor.fetchall())

"""
    Read the most recent logs across all agents.
"""
def read_logs(last_n=200):
    with sqlite3.connect(DB_CONNECTION) as conn:
        cursor = conn.cursor()
        cursor.execute('''
            SELECT datetime, name, type, message FROM logs
            ORDER BY datetime DESC
            LIMIT ?
        ''', (last_n,))

        return reversed(cursor.fetchall())