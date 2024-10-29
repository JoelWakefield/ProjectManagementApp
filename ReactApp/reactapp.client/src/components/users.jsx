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
          {user.name}
        </li>
      )}
    </ul>
  : <>loading...</> ;

  console.log(content);

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
    setUsers(data);
  }
}