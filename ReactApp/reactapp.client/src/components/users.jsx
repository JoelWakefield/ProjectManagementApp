import { useEffect, useState } from "react"

export default function Users() {
  const [users, setUsers] = useState();

  useEffect(() => {
    populateUserData();
  }, []);

  const content = users 
  ? <ul id="user-list">
      {users.map(user => 
        <li key={user.id}>
          <span>
            {user.name} - {user.projectRoles}
          </span>
        </li>
      )}
    </ul>
  : <>loading...</> ;

  return (
    <>
      <h2>
        Users
      </h2>

      {content}
    </>
  )

  async function populateUserData() {
    const response = await fetch('user');
    const data = await response.json();
    console.log(data);
    setUsers(data);
  }
}