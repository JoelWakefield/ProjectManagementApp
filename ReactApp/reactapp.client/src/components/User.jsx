import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

export default function User() {
  const [user, setUser] = useState();
  const { userId } = useParams();
  const navigate = useNavigate();
  
  useEffect(() => {
    userLoader(userId);
  }, [userId]);

  const handleClick = () => {
    navigate(`/users/${userId}/edit`);
  };

  const content = user
  ? <div>
      <h2>{user.name}</h2>
      <h3>Project Roles</h3>
      <p>{user.projectRoles}</p>
      <button onClick={handleClick}>Edit</button>
    </div>
  : <>Loading...</>;
  
  return (
    <>
      {content}
    </>
  );

  async function userLoader(userId) {
    const response = await fetch(`/user/${userId}`);
    const data = await response.json();
    setUser(data);
  }  
}