import { createBrowserRouter } from "react-router-dom";
import { LandingPage } from "../pages/LandingPage";
import { ErrorPage } from "../pages/ErrorPage";
import App from "../App";
import { getMovie, searchMovies } from "../api/movieService";
import { MovieDetail } from "../components/MovieDetail";

const routes = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <LandingPage />,
        loader: searchMovies,
        children: [
          {
            path: "/movies/:movieId",
            element: <MovieDetail />,
            loader: getMovie,
          },
        ],
      },
    ],
  },
]);

export default routes;
