import { Link, Outlet, useLoaderData } from "react-router-dom";

export default function Users() {
  const users = useLoaderData();
  
  return (
    <>
      <h2>
        Users
      </h2>

      <ul id="user-list">
        {users.map(user => 
          <li key={user.id}>
            <Link to={`${user.id}`}>
              {user.name} - {user.projectRoles}
            </Link>
          </li>
        )}
      </ul>

      <div>
        <Outlet />
      </div>
    </>
  )
}