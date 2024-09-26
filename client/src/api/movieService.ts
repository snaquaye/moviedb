import { Params } from "react-router-dom";
import { Movie } from "../type";

export async function searchMovies({ request }: { request: Request }) {
  const url = new URL(request.url);
  const query = url.searchParams.get("query") || "";
  const page = url.searchParams.get("page") || 1;

  const [data, searchHistories] = await Promise.all([
    await fetch(
      `${import.meta.env.VITE_BASE_URL}/movies?query=${query}&page=${page}`
    ).then(res => res.json()),
    await fetch(
      `${import.meta.env.VITE_BASE_URL}/search-histories?page=1&limit=5`
    ).then(res => res.json())
  ]);

  return { data, searchHistories, query };
}

export async function getMovie({ params }: { params: Params<string> }) {
  const response = await fetch(
    `${import.meta.env.VITE_BASE_URL}/movies/${params.movieId}`
  );

  const data: Movie = await response.json();
  return data;
}
