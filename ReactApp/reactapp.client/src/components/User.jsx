import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

export default function User() {
  const [user, setUser] = useState();
  const { userId } = useParams();
  
  useEffect(() => {
    userLoader(userId);
  }, [userId]);

  console.log(user);
  const content = user
  ? <div>{user.name}</div>
  : <>Loading...</>;
  
  return (
    <>
      {content}
    </>
  );

  async function userLoader(userId) {
    console.log(userId);
    const response = await fetch(`/user/${userId}`);
    console.log(response.url);
    const data = await response.json();
    console.log(data);
    setUser(data);
  }  
}