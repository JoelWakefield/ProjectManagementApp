import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

export default function EditUser() {
  const [user, setUser] = useState();
  const { userId } = useParams();
  
  useEffect(() => {
    userLoader(userId);
  }, [userId]);

  const content = user
  ? <div>
      <h2>{user.name}</h2>
      <h3>Project Roles</h3>
      <p>{user.projectRoles}</p>
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