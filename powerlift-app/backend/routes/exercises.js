const express = require('express');
const router = express.Router();
const ctrl = require('../controllers/exerciseController');

router.get('/routines/:routineId/exercises', ctrl.listByRoutine);
router.post('/routines/:routineId/exercises', ctrl.create);
router.get('/exercises/:id', ctrl.get);
router.delete('/exercises/:id', ctrl.del);

module.exports = router;
