import { Link, Outlet, useLoaderData } from "react-router-dom";
import "./Users.css";

export default function Users() {
  const users = useLoaderData();
  
  return (
    <>
      <h2>
        Users
      </h2>

      <div id="user-list">
        {users.map(user => 
          <Link to={`${user.id}`} key={user.id}>
            {user.name} - {user.projectRoles}
          </Link>
        )}
      </div>

      <div>
        <Outlet />
      </div>
    </>
  )
}