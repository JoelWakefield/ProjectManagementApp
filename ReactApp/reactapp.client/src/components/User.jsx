import { useLoaderData, useNavigate } from 'react-router-dom' 

export default function User() {
  const user = useLoaderData();
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/users/${user.id}/edit`);
  };
  
  return (
    <>
      <div>
        <h2>{user.name}</h2>
        <h3>Project Roles</h3>
        <p>{user.projectRoles}</p>
        <button onClick={handleClick}>Edit</button>
      </div>
    </>
  );
}