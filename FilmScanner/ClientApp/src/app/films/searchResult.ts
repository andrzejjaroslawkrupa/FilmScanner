export interface Search {
    title: string;
    year: string;
    imdbID: string;
    type: string;
    poster: string;
}

export interface SearchResult {
    searches: Search[];
    totalResults: string;
    response: string;
}
