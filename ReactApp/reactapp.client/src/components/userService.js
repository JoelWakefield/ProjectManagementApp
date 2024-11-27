export default async function usersLoader() {
  const response = await fetch('/user');
  return response.json();
}

export async function userLoader({ params }) {
  const response = await fetch(`/user/${params.userId}`);
  return response.json();
}

export async function updateUser(user) {
  // Map the roles back into an array
  const formattedData = formatProjectRoles(user);
  console.log(formattedData);

  // Make the PUT request using the fetch API
  await fetch(`/user/${user.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(formattedData)
  });
}

function formatProjectRoles(raw) {
  let roles = [];
  Object.keys(raw)
    .filter(key => key.includes("role"))
    .reduce((_, key) => {
      const newKey = key.replace("role-", "");
      roles = [...roles, { 
        "name": newKey, 
        "value": raw[key] === "on"
      }];
    }, {});
  
  const notRoles = Object.keys(raw)
  .filter(key => !key.includes("role"))
  .reduce((obj, key) => {
    obj[key] = raw[key];
    return obj;
  }, {});

  return { ...notRoles, projectRoles: roles };
}