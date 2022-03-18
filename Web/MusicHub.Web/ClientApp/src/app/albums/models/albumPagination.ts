import Album from "./album";

export interface AlbumPagination {
  albums: Array<Album>,
  numberOfPages: number,
  currentPage: number,
}
