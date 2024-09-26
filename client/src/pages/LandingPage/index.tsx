import { useEffect } from "react";
import { Link, Outlet, useLoaderData, useNavigate } from "react-router-dom";
import { Pagination } from "../../components/Pagination";
import { SearchForm } from "../../components/SearchForm";
import { usePaginationStore } from "../../store/paginationStore";
import { LoaderSearchResult } from "../../type";
import { MovieCard } from "../../components/MovieCard";

export const LandingPage = () => {
  const { data, query, searchHistories } = useLoaderData() as LoaderSearchResult;
  const { initializePagination, page } = usePaginationStore();
  const navigate = useNavigate();

  useEffect(() => {
    initializePagination(parseInt(data.totalResults));
  }, [query, data.totalResults, initializePagination]);

  useEffect(() => {
    const url = new URL(window.location.href);
    url.searchParams.set('page', page.toString());
    const query = url.searchParams.toString();
    if (page >= 1) {
      navigate(`?${query}`);
    }
  }, [page, navigate]);

  if (!data.movies.length && query.length > 0) {
    return <p>No results found for {query}</p>;
  }

  const sanitizSearcheHistory = (searchTerm: string) => {
    return searchTerm.trim().toUpperCase();
  };

  return (
    <div className="flex flex-col space-y-4">
      <SearchForm />
      <ul className="flex flex-row space-x-2">
        <li className="text-gray-500 bold">Search History:</li>
        {searchHistories.searchTerms.map((history) => (
          <li key={history} className="text-white text-bold bg-red-500 py-1 px-2 rounded-lg">
            <Link to={`?query=${history}`}>{sanitizSearcheHistory(history)}</Link>
          </li>
        ))}
      </ul>
      <div className="flex justify-end">
        {data.movies.length > 0 && (
          <Pagination
            totalRecords={parseInt(data.totalResults)}
            showSummary={false}
          />
        )}
      </div>
      {data.movies.length > 0 && (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
          {data.movies.map((movie) => (
            <MovieCard key={movie.imdbId} movie={movie} />
          ))}
        </div>
      )}
      {data.movies.length > 0 && (
        <Pagination
          totalRecords={parseInt(data.totalResults)}
          showSummary={true}
        />
      )}
      <Outlet />
    </div>
  );
};
