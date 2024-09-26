import { convertStringToArray } from "../../utils";

type Params = {
  list: string;
  title: string;
}

export const MovieInfoList = ({ list, title}: Params) => {
  return (
    <>
      <h2 className="text-2xl font-bold text-right">{title}</h2>
      <ul>
        {convertStringToArray(list).map((genre, index) => (
          <li className="text-right text-gray-500 text-bold" key={index}>{genre}</li>
        ))}
      </ul>
    </>
  );
};
