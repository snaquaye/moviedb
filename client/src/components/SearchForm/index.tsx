import { Form, useLoaderData } from "react-router-dom";
import { SearchResult } from "../../type";

export function SearchForm() {
  const {query} = useLoaderData() as {data: SearchResult, query: string}
  
  return (
    <div className="">
      <Form id="search-form" role="search" className="flex space-x-3">
        <div className="relative w-full">
          <input
            type="text"
            name="query"
            placeholder="Search movies"
            defaultValue={query}
            autoComplete="off"
            className="bg-white border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full pl-10 p-2.5"
          />
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="absolute top-1/2 left-3 transform -translate-y-1/2 w-6 h-6 text-gray-400"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
            />
          </svg>
        </div>
        <button type="submit" className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Search</button>
      </Form>
    </div>
  );
}
