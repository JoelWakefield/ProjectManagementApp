import { useEffect, useState } from "react"

export default function Users() {
  const [users, setUsers] = useState();

  useEffect(() => {
    populateUserData();
  }, []);

  return (
    <>
      <h2>
        Users
      </h2>

      <ul>
        {users.map(user => 
          <li key={user.id}>
            {user.name}
          </li>
        )}
      </ul>
    </>
  )

  async function populateUserData() {
    const response = await fetch('users');
    const data = await response.json();
    setUsers(data);
  }
}