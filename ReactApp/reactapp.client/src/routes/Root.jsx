import { NavLink, Outlet } from "react-router-dom";

function Root() {
	return (
		<div>
			<header>
				<nav>
					<NavLink to="/">Home</NavLink>
					<NavLink to="users">Users</NavLink>
					<NavLink to="roles">ProjectRoles</NavLink>
				</nav>
			</header>

			<main>
				<Outlet />
			</main>
		</div>
	);
}

export default Root;