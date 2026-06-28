const db = require('../db');

exports.listByRoutine = (req,res)=>{
  db.all('SELECT * FROM exercises WHERE routine_id = ?', [req.params.routineId], (err,rows)=> err? res.status(500).json({error:err.message}) : res.json(rows));
}

exports.create = (req,res)=>{
  const { name, notes } = req.body;
  db.run('INSERT INTO exercises (routine_id, name, notes) VALUES (?,?,?)', [req.params.routineId, name, notes], function(err){
    if(err) return res.status(500).json({error:err.message});
    db.get('SELECT * FROM exercises WHERE id = ?', [this.lastID], (e,row)=> res.status(201).json(row));
  });
}

exports.del = (req,res)=>{
  db.run('DELETE FROM exercises WHERE id=?', [req.params.id], function(err){
    if(err) return res.status(500).json({error:err.message});
    res.status(204).end();
  });
}

exports.get = (req,res)=>{
  db.get('SELECT * FROM exercises WHERE id = ?', [req.params.id], (err,row)=> err? res.status(500).json({error:err.message}) : res.json(row));
}
