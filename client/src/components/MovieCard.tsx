import { Link, useLocation } from "react-router-dom";
import { SearchResultItem } from "../type";

export const MovieCard = ({ movie }: { movie: SearchResultItem }) => {
  const { search } = useLocation();
  const query = new URLSearchParams(search);
  const queryStr = query.get("query") || "";
  const page = query.get("page") || 1;
  
  return (
    <div className="bg-white rounded-lg shadow-md">
      <div className="group relative h-full">
        <div className="absolute inset-0 bg-black bg-opacity-50 rounded-lg h-full hidden group-hover:block content-center">
          <Link to={`/movies/${movie.imdbId}?page=${page}&query=${queryStr}`} className="bg-red-600 text-white py-2 px-4 rounded-md self-center"> View</Link>
          <div className="absolute bottom-0 rounded-b-lg bg-slate-200 min-h-[75px] w-full overflow-hidden">
            <h2 className="text-lg font-bold text-gray-800 p-4 pb-0">
              {movie.title}
            </h2>
            <p className="text-sm text-gray-600">
              {movie.year} - {movie.type.toLocaleUpperCase()}
            </p>
          </div>
        </div>
        <img
          src={movie.poster}
          alt={movie.title}
          className="w-full h-full object-cover rounded-lg"
        />
      </div>
    </div>
  );
};
