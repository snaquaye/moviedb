import { create } from "zustand";

type PaginationStore = {
  page: number;
  showingFrom: number;
  showingTo: number;
  maximumPage: number;
  numberOfRecords: number;
  initializePagination: (totalRecords: number) => void;
  nextPage: () => void;
  previousPage: () => void;
}

export const usePaginationStore = create<PaginationStore>((set) => {
  return {
    page: 1,
    showingFrom: 1,
    showingTo: 10,
    maximumPage: 0,
    numberOfRecords: 0,
    nextPage: () => set((state) => {
      const page = state.page + 1;
      const showingFrom = (page * 10) - 9;
      const showingTo = page * 10;
      return { page, showingFrom, showingTo }
    }),
    previousPage: () => set((state) => {
      const page = Math.max(state.page - 1, 1);
      const showingFrom = Math.max((page * 10) - 9, 1);
      const showingTo = page * 10;
      return { page, showingFrom, showingTo }
    }),
    initializePagination: (totalRecords: number) => set((state) => {
      const limit = totalRecords < 10 ? totalRecords : 10;
      const maximumPage = Math.ceil(totalRecords / 10);
      const showingFrom = (state.page * 10) - 9;
      const showingTo = state.page * limit;
      return { showingFrom, showingTo, maximumPage, numberOfRecords: totalRecords }
    })
  }
});
