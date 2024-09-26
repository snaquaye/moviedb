import { Movie } from "../../type";

type Props = {
  movie: Movie;
}

export function Ratings({movie}: Props) {
  return (
    <>
      <h2 className="text-2xl font-bold"> Rating </h2>
      <dl>
        {movie.ratings.map((rating, index) => (
          <div key={index}>
            <dt className="text-gray-400 bold"> {rating.source} </dt>
            <dd> {rating.value} </dd>
          </div>
        ))}
      </dl>
    </>
  );
}
