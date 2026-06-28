const db = require('../db');

exports.listByExercise = (req,res)=>{
  db.all('SELECT * FROM sets WHERE exercise_id = ?', [req.params.exerciseId], (err,rows)=> err? res.status(500).json({error:err.message}) : res.json(rows));
}

exports.create = (req,res)=>{
  const { reps, weight, notes } = req.body;
  db.run('INSERT INTO sets (exercise_id, reps, weight, notes) VALUES (?,?,?,?)', [req.params.exerciseId, reps, weight, notes], function(err){
    if(err) return res.status(500).json({error:err.message});
    db.get('SELECT * FROM sets WHERE id = ?', [this.lastID], (e,row)=> res.status(201).json(row));
  });
}

exports.del = (req,res)=>{
  db.run('DELETE FROM sets WHERE id=?', [req.params.id], function(err){
    if(err) return res.status(500).json({error:err.message});
    res.status(204).end();
  });
}
