import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import App from './App.jsx'
import ProjectRoles from './routes/ProjectRoles';
import Users from './routes/users';
import './index.css'
import User, { loader as userLoader } from './components/User.jsx';
import ErrorPage from './error-page';

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/roles",
    element: <ProjectRoles />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/users",
    element: <Users />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/users/:userId",
        element: <User />,
        loader: userLoader,
        errorElement: <ErrorPage />,
      },
    ],
  },
]);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
