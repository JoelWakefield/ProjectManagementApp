import { useRouteError } from "react-router-dom";

export default function ErrorPage() {
  const error = useRouteError();

  return (
    <div id="error-page">
      <h1>Uh, oh...</h1>
      <p>Something exepected happened.</p>
      <p>
        <i>{error.statusText || error.message}</i>
      </p>
    </div>
  );
}
