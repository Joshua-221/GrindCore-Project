const express = require('express');
const router = express.Router();
const ctrl = require('../controllers/routineController');

router.get('/routines', ctrl.list);
router.post('/routines', ctrl.create);
router.get('/routines/:id', ctrl.get);
router.put('/routines/:id', ctrl.update);
router.delete('/routines/:id', ctrl.del);

module.exports = router;
