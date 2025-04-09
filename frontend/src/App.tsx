import React, { useEffect, useState } from 'react';
import { Container, Typography, List, ListItem, ListItemText, Button, Box, TextField, Stack } from '@mui/material';

interface User {
  id: number;
  username: string;
  email: string;
  createdAt: string;
  gameProfiles: { $values: GameProfile[] };
}

interface GameProfile {
  id: number;
  platform: string;
  gamerTag: string;
}

interface ApiResponse {
  $values: User[];
}

const App: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [newUser, setNewUser] = useState<{ username: string; email: string }>({ username: '', email: '' });
  const apiUrl = 'https://https://velocity-social-backend.onrender.com/api/users';

  const fetchUsers = async () => {
    try {
      const response = await fetch(apiUrl);
      const data: ApiResponse = await response.json();
      setUsers(data.$values || []);
    } catch (error) {
      console.error('Error fetching users:', error);
    }
  };

  const addUser = async () => {
    try {
      const response = await fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newUser),
      });
      if (response.ok) {
        setNewUser({ username: '', email: '' });
        await fetchUsers();
      }
    } catch (error) {
      console.error('Error adding user:', error);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom align="center">
        Velocity Social
      </Typography>
      <Stack spacing={2} sx={{ mb: 4 }}>
        <TextField
          label="Username"
          value={newUser.username}
          onChange={(e) => setNewUser({ ...newUser, username: e.target.value })}
          fullWidth
        />
        <TextField
          label="Email"
          value={newUser.email}
          onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
          fullWidth
        />
        <Button variant="contained" onClick={addUser}>
          Add User
        </Button>
        <Button variant="outlined" onClick={fetchUsers}>
          Refresh
        </Button>
      </Stack>
      <List>
        {users.length === 0 ? (
          <Typography variant="body1" align="center">No users yet!</Typography>
        ) : (
          users.map((user) => (
            <ListItem key={user.id}>
              <Box width="100%">
                <ListItemText
                  primary={user.username}
                  secondary={`${user.email} | Joined: ${new Date(user.createdAt).toLocaleDateString()}`}
                />
                <Typography variant="body2" color="textSecondary">
                  Gaming Profiles:
                </Typography>
                <List dense>
                  {user.gameProfiles.$values.length > 0 ? (
                    user.gameProfiles.$values.map((profile) => (
                      <ListItem key={profile.id}>
                        <ListItemText primary={`${profile.platform}: ${profile.gamerTag}`} />
                      </ListItem>
                    ))
                  ) : (
                    <ListItem>
                      <ListItemText primary="No gaming profiles" />
                    </ListItem>
                  )}
                </List>
              </Box>
            </ListItem>
          ))
        )}
      </List>
    </Container>
  );
};

export default App;