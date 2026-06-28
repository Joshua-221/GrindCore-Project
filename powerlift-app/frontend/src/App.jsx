import { useEffect, useState } from 'react'

function Routine({r, onSelect, onDelete}){
  return <div style={{border:'1px solid #ddd', padding:8, marginBottom:8}}>
    <strong>{r.name}</strong>
    <div style={{marginTop:6}}>
      <button onClick={()=>onSelect(r)}>Ver</button>
      <button onClick={()=>onDelete(r)} style={{marginLeft:8}}>Eliminar</button>
    </div>
  </div>
}

function App(){
  const [routines, setRoutines] = useState([])
  const [name, setName] = useState('')
  const [selected, setSelected] = useState(null)
  const [exercises, setExercises] = useState([])
  const [exerciseName, setExerciseName] = useState('')
  const [sets, setSets] = useState({}) // map exerciseId -> sets array

  const api = (path, opts) => fetch('http://localhost:4000'+path, opts).then(r=>{
    if(!r.ok && r.status!==204) throw new Error('Network');
    return r.status===204? null : r.json();
  })

  useEffect(()=>{ loadRoutines() }, [])

  function loadRoutines(){ api('/api/routines').then(setRoutines).catch(console.error) }

  async function addRoutine(e){ e.preventDefault(); if(!name) return; const r = await api('/api/routines', {method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({name, notes:''})}); setRoutines(prev=>[...prev,r]); setName('') }

  async function delRoutine(r){ if(!confirm('Eliminar rutina?')) return; await api('/api/routines/'+r.id, {method:'DELETE'}); setRoutines(prev=>prev.filter(x=>x.id!==r.id)); if(selected && selected.id===r.id) { setSelected(null); setExercises([]); setSets({}) } }

  async function selectRoutine(r){ setSelected(r); const ex = await api('/api/routines/'+r.id+'/exercises'); setExercises(ex); // load sets for each
    const map = {};
    await Promise.all(ex.map(async (e)=>{ map[e.id] = await api('/api/exercises/'+e.id+'/sets').catch(()=>[]) }));
    setSets(map);
  }

  async function addExercise(e){ e.preventDefault(); if(!exerciseName || !selected) return; const ex = await api('/api/routines/'+selected.id+'/exercises', {method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({name:exerciseName, notes:''})}); setExercises(prev=>[...prev, ex]); setExerciseName(''); setSets(prev=> ({...prev, [ex.id]:[]})) }

  async function delExercise(ex){ if(!confirm('Eliminar ejercicio?')) return; await api('/api/exercises/'+ex.id, {method:'DELETE'}); setExercises(prev=>prev.filter(x=>x.id!==ex.id)); const copy = {...sets}; delete copy[ex.id]; setSets(copy);
  }

  async function addSet(e, exerciseId){ e.preventDefault(); const reps = e.target.reps.value || 5; const weight = e.target.weight.value || 0; const s = await api('/api/exercises/'+exerciseId+'/sets', {method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({reps: Number(reps), weight: Number(weight), notes: ''})}); setSets(prev=> ({...prev, [exerciseId]: [...(prev[exerciseId]||[]), s]})); e.target.reset(); }

  async function delSet(setId, exerciseId){ if(!confirm('Eliminar serie?')) return; await api('/api/sets/'+setId, {method:'DELETE'}); setSets(prev=> ({...prev, [exerciseId]: prev[exerciseId].filter(s=>s.id!==setId)})); }

  return (
    <div style={{padding:20,fontFamily:'Arial'}}>
      <h1>Powerlifting — Rutinas</h1>
      <form onSubmit={addRoutine} style={{marginBottom:16}}>
        <input placeholder="Nombre de rutina" value={name} onChange={e=>setName(e.target.value)} />
        <button type="submit">Agregar rutina</button>
      </form>

      <div style={{display:'flex',gap:20}}>
        <div style={{flex:1}}>
          <h2>Lista</h2>
          {routines.map(r=> <Routine key={r.id} r={r} onSelect={selectRoutine} onDelete={delRoutine} />)}
        </div>

        <div style={{flex:2}}>
          {selected ? (
            <div>
              <h2>{selected.name}</h2>
              <form onSubmit={addExercise} style={{marginBottom:12}}>
                <input placeholder="Nombre de ejercicio" value={exerciseName} onChange={e=>setExerciseName(e.target.value)} />
                <button type="submit">Agregar ejercicio</button>
              </form>

              {exercises.map(ex=> (
                <div key={ex.id} style={{border:'1px solid #ccc', padding:8, marginBottom:8}}>
                  <strong>{ex.name}</strong>
                  <button onClick={()=>delExercise(ex)} style={{marginLeft:8}}>Eliminar</button>
                  <div style={{marginTop:8}}>
                    <form onSubmit={(e)=>addSet(e, ex.id)} style={{display:'flex', gap:8, alignItems:'center'}}>
                      <input name="reps" placeholder="reps" style={{width:60}} />
                      <input name="weight" placeholder="kg" style={{width:80}} />
                      <button type="submit">Agregar serie</button>
                    </form>
                    <ul>
                      {(sets[ex.id]||[]).map(s=> <li key={s.id}>Reps: {s.reps} — {s.weight}kg <button onClick={()=>delSet(s.id, ex.id)} style={{marginLeft:8}}>Eliminar</button></li>)}
                    </ul>
                  </div>
                </div>
              ))}
            </div>
          ) : (<div><em>Selecciona una rutina para ver ejercicios</em></div>)}
        </div>
      </div>

    </div>
  )
}

export default App
