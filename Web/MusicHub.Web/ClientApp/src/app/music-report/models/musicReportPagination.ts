import ReportMusic from "./musicReport";

export interface ReportMusicPagination {
  reportMusics: Array<ReportMusic>,
  numberOfPages: number,
  currentPage: number,
}
