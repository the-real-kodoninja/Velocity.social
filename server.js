const express = require('express');
const app = express();
const port = 5280;

app.use(express.json());

// Add CORS middleware
app.use((req, res, next) => {
  res.header('Access-Control-Allow-Origin', '*'); 
  res.header('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept');
  res.header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS');
  next();
});

let users = [];

app.get('/api/users', (req, res) => {
  res.json({ $values: users });
});

app.post('/api/users', (req, res) => {
  const newUser = {
    id: users.length + 1,
    username: req.body.username,
    email: req.body.email,
    createdAt: new Date().toISOString(),
    gameProfiles: { $values: [] }
  };
  users.push(newUser);
  res.status(201).json(newUser);
});

app.listen(port, () => {
  console.log(`Mock backend running on port ${port}`);
});