import { Link, Outlet, useLoaderData } from "react-router-dom";

export default function Users() {
  const users = useLoaderData();
  
  return (
    <>
      <h2>
        Users
      </h2>

      {users.map(user => 
        <Link to={`${user.id}`} key={user.id}>
          {user.name} - {user.projectRoles}
        </Link>
      )}

      <div>
        <Outlet />
      </div>
    </>
  )
}