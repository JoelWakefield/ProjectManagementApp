import { useLoaderData } from "react-router-dom";

export async function loader({ params }) {
  const response = await fetch(`user/${params.userId}`);
  const data = await response.json();
  console.log(data);
  return { data };
}

export default function User() {
  const { user } = useLoaderData();

  const content = user
  ? <>{user.name}</>
  : <>Loading...</>;

  return (
    {content}
  );
}