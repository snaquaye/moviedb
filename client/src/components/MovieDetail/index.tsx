import { useLoaderData, useNavigate } from "react-router-dom";
import { Movie } from "../../type";
import { MovieInfoList } from "../MovieList";
import { Ratings } from "../Rating";

export function MovieDetail() {
  const movie = useLoaderData() as Movie;

  const navigate = useNavigate();
  const goBack = () => navigate(-1);

  return (
    <div
      className="fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div className="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
        <div
          className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>
        <span
          className="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
        >
          &#8203;
        </span>
        <div className="inline-block align-bottom bg-white rounded-lg px-4 pt-5 pb-4 text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full sm:p-6 min-w-[60%]">
          <div className="flex px-4 space-x-3">
            <div className="flex-1 space-y-3">
              <MovieInfoList list={movie.genre} title="Genre" />
              <MovieInfoList list={movie.director} title="Directors" />
              <MovieInfoList list={movie.actors} title="Cast" />
            </div>
            <div className="px-4 space-y-5">
              <h1 className="text-3xl font-bold">{movie.title}</h1>
              <p className="text-lg">
                {movie.runtime}, {movie.released}
              </p>
              <p className="text-sm">{movie.plot}</p>

              <Ratings movie={movie} />
            </div>
            <div className="min-w-[300px] px-4">
              <img
                src={movie.poster}
                alt={movie.title}
                className="w-full h-full object-cover rounded-lg"
              />
            </div>
          </div>
          <div className="flex justify-end pt-3 pr-8">
            <button
              onClick={goBack}
              className="bg-red-500 text-white px-4 py-2 rounded-lg text-right hover:bg-red-600"
            >
              Go Back
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
