const db = require('../db');

exports.list = (req,res)=>{
  db.all('SELECT * FROM routines', (err,rows)=> err? res.status(500).json({error:err.message}) : res.json(rows));
}

exports.create = (req,res)=>{
  const { name, notes } = req.body;
  db.run('INSERT INTO routines (name, notes) VALUES (?,?)', [name, notes], function(err){
    if(err) return res.status(500).json({error: err.message});
    db.get('SELECT * FROM routines WHERE id = ?', [this.lastID], (e,row)=> res.status(201).json(row));
  });
}

exports.get = (req,res)=>{
  db.get('SELECT * FROM routines WHERE id = ?', [req.params.id], (err,row)=> err? res.status(500).json({error:err.message}) : res.json(row));
}

exports.update = (req,res)=>{
  const { name, notes } = req.body;
  db.run('UPDATE routines SET name=?, notes=? WHERE id=?', [name, notes, req.params.id], function(err){
    if(err) return res.status(500).json({error:err.message});
    db.get('SELECT * FROM routines WHERE id = ?', [req.params.id], (e,row)=> res.json(row));
  });
}

exports.del = (req,res)=>{
  db.run('DELETE FROM routines WHERE id=?', [req.params.id], function(err){
    if(err) return res.status(500).json({error:err.message});
    res.status(204).end();
  });
}
