import { useLoaderData, useNavigate } from 'react-router-dom' 

export default function User() {
  const user = useLoaderData();
  console.log(user);
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/users/${user.id}/edit`);
  };
  
  return (
    <>
      <div>
        <h2>{user.name}</h2>
        <div>
          <h3>Project Roles</h3>
          {user.projectRoles.map(role => 
            <p key={role.name}>
              <span>{role.name} - {role.value.toString()}</span>
            </p>
          )}
        </div>
        <button onClick={handleClick}>Edit</button>
      </div>
    </>
  );
}