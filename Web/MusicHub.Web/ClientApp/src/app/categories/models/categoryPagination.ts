import Category from "./category";

export default interface CategoryPagination {
  categories: Array<Category>,
  numberOfPages: number,
  currentPage: number,
}
