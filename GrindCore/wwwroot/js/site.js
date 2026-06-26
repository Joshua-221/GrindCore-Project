(function () {
    'use strict';

    const STORAGE_KEY = 'grindcore.workout.state.v1';

    function $(sel, ctx = document) { return ctx.querySelector(sel); }
    function $all(sel, ctx = document) { return Array.from(ctx.querySelectorAll(sel)); }

    function uid() { return crypto.randomUUID ? crypto.randomUUID() : Math.random().toString(36).slice(2, 9); }

    function getInitialState() {
        const node = document.querySelector('[data-workout-app]');
        if (!node) return { routines: [], suggestions: [] };
        try {
            const initial = JSON.parse(node.getAttribute('data-initial-state')) || {};
            const fromServer = (initial.suggestedRoutines || initial.SuggestedRoutines || []).map(r => ({
                id: uid(),
                name: r.name || r.Name || 'Rutina',
                focus: r.focus || r.Focus || '',
                exercises: (r.exercises || r.Exercises || []).map(e => ({
                    id: uid(),
                    name: e.name || e.Name || '',
                    sets: e.sets || e.Sets || 3,
                    reps: e.reps || e.Reps || 5,
                    loadKg: e.loadKg || e.LoadKg || 0
                }))
            }));

            const suggestions = initial.exerciseSuggestions || initial.ExerciseSuggestions || [];

            // merge with localStorage if present
            const stored = localStorage.getItem(STORAGE_KEY);
            if (stored) {
                try {
                    const parsed = JSON.parse(stored);
                    return parsed;
                } catch (e) {
                    console.warn('Invalid stored state, falling back to server state');
                }
            }

            return { routines: fromServer, suggestions };
        } catch (err) {
            console.error('Failed parsing initial state', err);
            return { routines: [], suggestions: [] };
        }
    }

    function saveState(state) {
        localStorage.setItem(STORAGE_KEY, JSON.stringify(state));
    }

    function renderMetrics(state) {
        const totalRoutines = state.routines.length;
        const totalSets = state.routines.reduce((acc, r) => acc + r.exercises.reduce((s, ex) => s + (Number(ex.sets) || 0), 0), 0);
        const totalVolume = state.routines.reduce((acc, r) => acc + r.exercises.reduce((s, ex) => s + ((Number(ex.sets) || 0) * (Number(ex.loadKg) || 0) * (Number(ex.reps) || 0)), 0), 0);

        document.querySelector('[data-total-routines]').textContent = totalRoutines;
        document.querySelector('[data-total-sets]').textContent = totalSets;
        document.querySelector('[data-total-volume]').textContent = `${totalVolume} kg`;
    }

    function populateRoutineSelect(state) {
        const select = document.querySelector('[data-routine-select]');
        if (!select) return;
        select.innerHTML = '';
        state.routines.forEach(r => {
            const opt = document.createElement('option');
            opt.value = r.id;
            opt.textContent = r.name;
            select.appendChild(opt);
        });
    }

    function renderRoutineList(state, filter = 'all') {
        const container = document.querySelector('[data-routine-list]');
        const empty = document.querySelector('[data-empty-state]');
        container.innerHTML = '';

        const filtered = filter === 'all' ? state.routines : state.routines.filter(r => r.focus === filter);

        if (filtered.length === 0) {
            empty.hidden = false;
            return;
        }
        empty.hidden = true;

        filtered.forEach(r => {
            const card = document.createElement('div');
            card.className = 'card mb-3';

            const cardBody = document.createElement('div');
            cardBody.className = 'card-body';

            const header = document.createElement('div');
            header.className = 'd-flex justify-content-between align-items-start';

            const title = document.createElement('div');
            title.innerHTML = `<h3 class="h5">${escapeHtml(r.name)}</h3><small class="text-muted">${escapeHtml(r.focus)}</small>`;

            const actions = document.createElement('div');
            actions.innerHTML = `<button class="btn btn-sm btn-outline-danger" data-remove-routine="${r.id}">Eliminar</button>`;

            header.appendChild(title);
            header.appendChild(actions);
            cardBody.appendChild(header);

            if (r.exercises.length === 0) {
                const p = document.createElement('p');
                p.className = 'mt-2 mb-0 text-muted';
                p.textContent = 'Sin ejercicios';
                cardBody.appendChild(p);
            } else {
                const list = document.createElement('ul');
                list.className = 'list-group list-group-flush mt-2';
                r.exercises.forEach(ex => {
                    const item = document.createElement('li');
                    item.className = 'list-group-item d-flex justify-content-between align-items-center';
                    item.innerHTML = `<div><strong>${escapeHtml(ex.name)}</strong> <small class="text-muted">${ex.sets}x${ex.reps} @ ${ex.loadKg}kg</small></div>
                                      <div><button class="btn btn-sm btn-outline-secondary me-2" data-edit-ex="${ex.id}" data-routine-id="${r.id}">Editar</button>
                                      <button class="btn btn-sm btn-outline-danger" data-remove-ex="${ex.id}" data-routine-id="${r.id}">Quitar</button></div>`;
                    list.appendChild(item);
                });
                cardBody.appendChild(list);
            }

            card.appendChild(cardBody);
            container.appendChild(card);
        });
    }

    function escapeHtml(str) {
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#039;');
    }

    function bindFormHandlers(state) {
        const routineForm = document.querySelector('[data-routine-form]');
        const exerciseForm = document.querySelector('[data-exercise-form]');

        routineForm?.addEventListener('submit', e => {
            e.preventDefault();
            const fd = new FormData(routineForm);
            const name = fd.get('routineName').toString().trim();
            const focus = fd.get('routineFocus').toString();
            if (!name) return;
            const newR = { id: uid(), name, focus, exercises: [] };
            state.routines.push(newR);
            saveState(state);
            populateRoutineSelect(state);
            renderRoutineList(state, getActiveFilter());
            renderMetrics(state);
            routineForm.reset();
        });

        exerciseForm?.addEventListener('submit', e => {
            e.preventDefault();
            const fd = new FormData(exerciseForm);
            const routineId = fd.get('routineId')?.toString();
            const name = fd.get('exerciseName')?.toString().trim();
            const sets = Number(fd.get('sets')) || 3;
            const reps = Number(fd.get('reps')) || 5;
            const loadKg = Number(fd.get('loadKg')) || 0;
            if (!routineId || !name) return;
            const routine = state.routines.find(r => r.id === routineId);
            if (!routine) return alert('Rutina no encontrada');
            routine.exercises.push({ id: uid(), name, sets, reps, loadKg });
            saveState(state);
            renderRoutineList(state, getActiveFilter());
            renderMetrics(state);
            exerciseForm.reset();
        });

        document.querySelector('[data-reset-app]')?.addEventListener('click', () => {
            if (!confirm('Reiniciar la app borrará tus rutinas locales. Continuar?')) return;
            localStorage.removeItem(STORAGE_KEY);
            location.reload();
        });

        document.querySelectorAll('.segmented-control button').forEach(btn => {
            btn.addEventListener('click', (e) => {
                document.querySelectorAll('.segmented-control button').forEach(b => b.classList.remove('is-active'));
                btn.classList.add('is-active');
                const f = btn.getAttribute('data-filter') || 'all';
                renderRoutineList(state, f);
            });
        });

        // Delegate remove/edit buttons
        document.body.addEventListener('click', e => {
            const removeR = e.target.closest('[data-remove-routine]');
            if (removeR) {
                const id = removeR.getAttribute('data-remove-routine');
                const idx = state.routines.findIndex(r => r.id === id);
                if (idx >= 0) {
                    if (confirm('Eliminar rutina?')) {
                        state.routines.splice(idx, 1);
                        saveState(state);
                        populateRoutineSelect(state);
                        renderRoutineList(state, getActiveFilter());
                        renderMetrics(state);
                    }
                }
                return;
            }

            const removeEx = e.target.closest('[data-remove-ex]');
            if (removeEx) {
                const exId = removeEx.getAttribute('data-remove-ex');
                const routineId = removeEx.getAttribute('data-routine-id');
                const r = state.routines.find(rr => rr.id === routineId);
                if (!r) return;
                const ix = r.exercises.findIndex(x => x.id === exId);
                if (ix >= 0) {
                    if (confirm('Quitar ejercicio?')) {
                        r.exercises.splice(ix, 1);
                        saveState(state);
                        renderRoutineList(state, getActiveFilter());
                        renderMetrics(state);
                        populateRoutineSelect(state);
                    }
                }
                return;
            }

            const editEx = e.target.closest('[data-edit-ex]');
            if (editEx) {
                const exId = editEx.getAttribute('data-edit-ex');
                const routineId = editEx.getAttribute('data-routine-id');
                const r = state.routines.find(rr => rr.id === routineId);
                if (!r) return;
                const ex = r.exercises.find(x => x.id === exId);
                if (!ex) return;
                // simple prompt-based edit
                const name = prompt('Nombre del ejercicio', ex.name);
                if (name == null) return;
                const sets = Number(prompt('Series', ex.sets));
                if (Number.isNaN(sets)) return;
                const reps = Number(prompt('Repeticiones', ex.reps));
                if (Number.isNaN(reps)) return;
                const load = Number(prompt('Kg', ex.loadKg));
                if (Number.isNaN(load)) return;
                ex.name = name;
                ex.sets = sets;
                ex.reps = reps;
                ex.loadKg = load;
                saveState(state);
                renderRoutineList(state, getActiveFilter());
                renderMetrics(state);
                populateRoutineSelect(state);
            }
        });
    }

    function getActiveFilter() {
        const btn = document.querySelector('.segmented-control button.is-active');
        return btn ? (btn.getAttribute('data-filter') || 'all') : 'all';
    }

    document.addEventListener('DOMContentLoaded', () => {
        const state = getInitialState();
        // normalize if missing
        state.routines = state.routines || [];
        state.suggestions = state.suggestions || [];

        // ensure suggestions are set in datalist
        const datalist = document.getElementById('exercise-suggestions');
        if (datalist && state.suggestions.length) {
            datalist.innerHTML = '';
            state.suggestions.forEach(s => {
                const o = document.createElement('option');
                o.value = s;
                datalist.appendChild(o);
            });
        }

        populateRoutineSelect(state);
        renderRoutineList(state);
        renderMetrics(state);
        bindFormHandlers(state);
    });

})();
