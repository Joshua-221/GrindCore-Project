const sqlite3 = require('sqlite3').verbose();
const path = require('path');
const dbFile = path.join(__dirname, 'data.db');
const db = new sqlite3.Database(dbFile);

db.serialize(() => {
  db.run(`PRAGMA foreign_keys = ON`);
  db.run(`CREATE TABLE IF NOT EXISTS routines (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, notes TEXT)`);
  db.run(`CREATE TABLE IF NOT EXISTS exercises (id INTEGER PRIMARY KEY AUTOINCREMENT, routine_id INTEGER, name TEXT NOT NULL, notes TEXT, FOREIGN KEY(routine_id) REFERENCES routines(id) ON DELETE CASCADE)`);
  db.run(`CREATE TABLE IF NOT EXISTS sets (id INTEGER PRIMARY KEY AUTOINCREMENT, exercise_id INTEGER, reps INTEGER, weight REAL, notes TEXT, FOREIGN KEY(exercise_id) REFERENCES exercises(id) ON DELETE CASCADE)`);
});

module.exports = db;
