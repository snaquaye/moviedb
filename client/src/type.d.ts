export type SearchFormType = {
  searchTerm: string | null | undefined
}

export type Rating = {
  source: string;
  value: string;
}

export type Movie = {
  title: string;
  year: string;
  rated: string;
  released: string;
  runtime: string;
  genre: string;
  director: string;
  writer: string;
  actors: string;
  plot: string;
  language: string;
  country: string;
  awards: string;
  poster: string;
  ratings: Rating[];
  metascore: string;
  imdbRating: string;
  imdbVotes: string;
  imdbId: string;
  type: string;
  dvd: string;
  boxOffice: string;
  production: string;
  website: string;
  response: string;
}

export type SearchResultItem = Pick<Movie, "title" | "year" | "imdbId" | "type" | "poster">;

export type SearchResult = {
  movies: SearchResultItem[],
  totalResults: string
};

export type SearchQueries = {
  data: {
    query: string
  }[];
}

export type Params = {
  params: {
    movieId: string
  }
}

export type SearchHistory = {
  id: string,
  searchTerm: string;
}

export type LoaderSearchResult = {
  data: SearchResult,
  query: string
  searchHistories: { searchTerms: string[] }
}
