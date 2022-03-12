export default function getPage<T>(collection: T[], page: number, itemsPerPage: number): T[]{
    let startIndex = (page - 1) * itemsPerPage;
    let endIndex = startIndex + itemsPerPage;
    return collection.slice(startIndex, endIndex);
}