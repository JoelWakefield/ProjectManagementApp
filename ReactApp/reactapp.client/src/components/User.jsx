import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

export default function User() {
  const [user, setUser] = useState();
  const { userId } = useParams();
  
  useEffect(() => {
    userLoader(userId);
  }, [userId]);

  const content = user
  ? <div>{user.name}</div>
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