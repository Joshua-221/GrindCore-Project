const express = require('express');
const router = express.Router();
const ctrl = require('../controllers/setController');

router.get('/exercises/:exerciseId/sets', ctrl.listByExercise);
router.post('/exercises/:exerciseId/sets', ctrl.create);
router.delete('/sets/:id', ctrl.del);

module.exports = router;
