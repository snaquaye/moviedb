import { usePaginationStore } from "../../store/paginationStore";

type Props = {
  totalRecords: number;
  showSummary: boolean;
}

export function Pagination({ totalRecords, showSummary }: Props) {
  const { nextPage, previousPage, showingFrom, showingTo } = usePaginationStore();

  return (
    <div className="flex justify-between items-center">
      {showSummary && (
        <p>
          Showing {showingFrom} to {showingTo} of {totalRecords}
        </p>
      )}
      {totalRecords > 10 && (
        <div className="flex space-x-2">
          <button
            className="text-black bg-slate-200 px-4 py-2 rounded-md"
            onClick={previousPage}
          >
            Previous
          </button>
          <button
            className="text-black bg-slate-200 px-4 py-2 rounded-md"
            onClick={nextPage}
          >
            Next
          </button>
        </div>
      )}
    </div>
  );
}