import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Root from './routes/Root.jsx';
import Home from './routes/Home.jsx'
import ProjectRoles from './routes/ProjectRoles';
import Users from './routes/Users';
import User from './components/User.jsx';
import EditUser from './components/EditUser.jsx';
import usersLoader, { userLoader } from './components/userService.js';
import ErrorPage from './error-page';
import './index.css'

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        index: true,
        element: <Home />,
        errorElement: <ErrorPage />,
      },
      {
        path: "roles",
        element: <ProjectRoles />,
        errorElement: <ErrorPage />,
      },
      {
        path: "users",
        element: <Users />,
        loader: usersLoader,
        errorElement: <ErrorPage />,
      },
      {
        path: "users/:userId",
        element: <User />,
        loader: userLoader,
        errorElement: <ErrorPage />,
      },
      {
        path: "users/:userId/edit",
        element: <EditUser />,
        loader: userLoader,
        errorElement: <ErrorPage />,
      },
    ]
  },
]);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
