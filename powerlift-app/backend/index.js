const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');
const path = require('path');

require('./db'); // ensure DB initialized

const routines = require('./routes/routines');
const exercises = require('./routes/exercises');
const sets = require('./routes/sets');

const app = express();
app.use(cors());
app.use(bodyParser.json());

app.use('/api', routines);
app.use('/api', exercises);
app.use('/api', sets);

const PORT = process.env.PORT || 4000;
app.listen(PORT, ()=> console.log(`Backend listening on http://localhost:${PORT}`));
